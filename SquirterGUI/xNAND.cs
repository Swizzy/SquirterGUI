namespace SquirterGUI {
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Windows.Forms;
    using SquirterGUI.Properties;

    internal class XNAND {
        public readonly bool IsOk;
        public static bool Abort;
        private static uint _config;
        private static XSPI _xspi;

        public XNAND() {
            _xspi = new XSPI();
            IsOk = _xspi.IsOk;
            Abort = false;
        }

        private static bool WaitReady(uint timeout) {
            do {
                if ((_xspi.ReadByte(0x04) & 0x01) == 0)
                    return true;
            }
            while (timeout-- > 0);
            return false;
        }

        private static uint GetStatus() {
            return _xspi.ReadWord(0x04);
        }

        private static void ClearStatus() {
            var buf = _xspi.ReadSync(4, 4);
            _xspi.Write(4, buf);
        }

        public static void SetConfig(uint config) {
            _config = config;
        }

        public static uint FlashDataInit() {
            _xspi.EnterFlashMode();
            var ret = _xspi.ReadSync(0, 4);
            if (ret == null || (ret[0] == ret[1] && ret[0] == ret[2] && ret[0] == ret[3]))
                return 0;
            return BitConverter.ToUInt32(ret, 0);
        }

        public static XNANDSettings GetSettings() {
            var sfc = new XNANDSettings {
                                            MetaType = 0,
                                            PageSz = 0x200,
                                            MetaSz = 0x10
                                        };
            sfc.PageSzPhys = sfc.PageSz + sfc.MetaSz;
            switch ((_config >> 17) & 0x03)
            {
                case 0: // Small block original SFC (pre jasper)
                    sfc.MetaType = 0;
                    sfc.BlocksPerLgBlock = 8;

                    switch ((_config >> 4) & 0x3)
                    {
                        case 0: // Unsupported 8MB?
                            MessageBox.Show(Resources.error_unsupported, Resources.error_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return null;
                        case 1: // 16MB
                            sfc.BlockSz = 0x4000; // 16 KB
                            sfc.SizeBlocks = 0x400;
                            sfc.SizeBytes = sfc.SizeBlocks << 0xE;
                            sfc.SizeUsableFs = 0x3E0;
                            sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                            break;
                        case 2: // 32MB
                            sfc.BlockSz = 0x4000; // 16 KB
                            sfc.SizeBlocks = 0x800;
                            sfc.SizeBytes = sfc.SizeBlocks << 0xE;
                            sfc.SizeUsableFs = 0x7C0;
                            sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                            break;

                        case 3: // 64MB
                            sfc.BlockSz = 0x4000; // 16 KB
                            sfc.SizeBlocks = 0x1000;
                            sfc.SizeBytes = sfc.SizeBlocks << 0xE;
                            sfc.SizeUsableFs = 0xF80;
                            sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                            break;
                    }
                    break;
                case 1: // New SFC/Southbridge: Codename "Panda"?
                case 2: // New SFC/Southbridge: Codename "Panda" v2?
                    switch ((_config >> 4) & 0x3)
                    {
                        case 0:
                            if (((_config >> 17) & 0x03) == 0x01)
                            {
                                // Unsupported
                                MessageBox.Show(Resources.error_unsupported, Resources.error_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return null;
                            }
                            sfc.MetaType = 1;
                            sfc.BlockSz = 0x4000; // 16 KB
                            sfc.SizeBlocks = 0x400;
                            sfc.SizeBytes = sfc.SizeBlocks << 0xE;
                            sfc.BlocksPerLgBlock = 8;
                            sfc.SizeUsableFs = 0x3E0;
                            sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                            break;
                        case 1:
                            if (((_config >> 17) & 0x03) == 0x01)
                            {
                                // Small block 16MB setup
                                sfc.MetaType = 1;
                                sfc.BlockSz = 0x4000; // 16 KB
                                sfc.SizeBlocks = 0x400;
                                sfc.SizeBytes = sfc.SizeBlocks << 0xE;
                                sfc.BlocksPerLgBlock = 8;
                                sfc.SizeUsableFs = 0x3E0;
                                sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                                break;
                            }
                            // Small block 64MB setup
                            sfc.MetaType = 1;
                            sfc.BlockSz = 0x4000; // 16 KB
                            sfc.SizeBlocks = 0x1000;
                            sfc.SizeBytes = sfc.SizeBlocks << 0xE;
                            sfc.BlocksPerLgBlock = 8;
                            sfc.SizeUsableFs = 0xF80;
                            sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                            break;
                        case 2: // Large Block: Current Jasper 256MB and 512MB
                            sfc.MetaType = 2;
                            sfc.BlockSz = 0x20000; // 128KB
                            sfc.SizeBytes = 0x1 << (((int)_config >> 19 & 0x3) + ((int)_config >> 21 & 0xF) + 0x17);
                            sfc.SizeBlocks = sfc.SizeBytes >> 0x11;
                            sfc.BlocksPerLgBlock = 1;
                            sfc.SizeUsableFs = 0x1E0;
                            sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                            break;

                        case 3: // Large Block: Future or unknown hardware
                            sfc.MetaType = 2;
                            sfc.BlockSz = 0x40000; // 256KB
                            sfc.SizeBytes = 0x1 << (((int)_config >> 19 & 0x3) + ((int)_config >> 21 & 0xF) + 0x17);
                            sfc.SizeBlocks = sfc.SizeBytes >> 0x12;
                            sfc.BlocksPerLgBlock = 1;
                            sfc.SizeUsableFs = 0xF0;
                            sfc.AddrConfig = (sfc.SizeUsableFs - 0x04) * sfc.BlockSz;
                            break;
                    }
                    break;

                default:
                    MessageBox.Show(Resources.error_unsupported, Resources.error_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
            }
            sfc.LenConfig = sfc.BlockSz * 0x04; //4 physical blocks
            sfc.PagesInBlock = sfc.BlockSz / sfc.PageSz;
            sfc.BlockSzPhys = sfc.PagesInBlock * sfc.PageSzPhys;
            sfc.SizePages = sfc.SizeBytes / sfc.PageSz;
            sfc.SizeBlocks = sfc.SizeBytes / sfc.BlockSz;
            sfc.SizeBytesPhys = sfc.BlockSzPhys * sfc.SizeBlocks;
            sfc.SizeMB = sfc.SizeBytes >> 20;
            return sfc;
        }

        private static void LogInit(string filename, int start, int last, int mode, ref XNANDSettings nandopts, string process, string modename = "")
        {
            Logger.WriteLine2("=========================================================================");
            Logger.WriteLine2(Main.AppNameAndVersion);
            Logger.WriteLine2(string.Format("Log started: {0:yyyy-MM-dd HH:mm:ss}", DateTime.Now));
            Logger.WriteLine2(string.Format("Started {0} NAND With the following settings:", process));
            Logger.WriteLine2(!string.IsNullOrEmpty(filename)
                ? string.Format("Filename & Path: {1}{0}First block: 0x{2:X}{0}Last Block: 0x{3:X}", Environment.NewLine, filename, start,last)
                : string.Format("First block: 0x{1:X}{0}Last Block: 0x{2:X}", Environment.NewLine, start, last));
            if (mode != 0) {
                Logger.Write2(string.Format("{0} Mode: ", modename));
                switch (mode) {
                    case (int) BwArgs.Modes.Raw:
                        Logger.WriteLine2("RAW");
                        break;
                    case (int) BwArgs.Modes.Glitch:
                        Logger.WriteLine2("Glitch");
                        break;
                }
            }
            Logger.WriteLine2("Nand Information:");
            Logger.WriteLine2(string.Format("config register = 0x{0:X08}", _config));
            Logger.WriteLine2(string.Format("page data size  = 0x{0:X}", nandopts.PageSz));
            Logger.WriteLine2(string.Format("meta size       = 0x{0:X}", nandopts.MetaSz));
            Logger.WriteLine2(string.Format("page size       = 0x{0:X}", nandopts.PageSzPhys));
            Logger.WriteLine2(string.Format("pages per block = 0x{0:X}", nandopts.PagesInBlock));
            Logger.WriteLine2(string.Format("total pages     = 0x{0:X}", nandopts.SizePages));
            Logger.WriteLine2(string.Format("total blocks    = 0x{0:X}", nandopts.SizeBlocks));
            Logger.WriteLine2("=========================================================================");
        }

        public static void Read(string filename, int start, int last, int mode, ref XNANDSettings nandopts)
        {
            var sw = new Stopwatch();
            sw.Start();
            LogInit(filename, start, last, mode, ref nandopts, "dumping", "Read");
            var fi = new FileInfo(filename);
            if (fi.Exists)
                fi.Delete();
            var handle = fi.OpenWrite();
            var currpage = 0;
            for (var block = start; block <= last; block++)
            {
                if (Abort) {
                    Main.SendAbort();
                    sw.Stop();
                    handle.Close();
                    Logger.WriteLine2(string.Format("Dumping aborted after: {0} Minutes and {1} seconds", sw.Elapsed.Minutes, sw.Elapsed.Seconds));
                    break;
                }
                ClearStatus();
                Main.SendStatus(block, last);
                for (var page = 0; page < nandopts.PagesInBlock; page++) {
                    var len = nandopts.PageSzPhys / 4;
                    var pagesleft = 0;
                    while (len > 0) {
                        if (pagesleft == 0) {
                            ReadPageInit((uint)currpage);
                            pagesleft = 0x84;
                        }
                        var readnow = (len < pagesleft) ? len : pagesleft;
                        ReadProc(ref handle, readnow, ref nandopts, mode);
                        pagesleft -= readnow;
                        len -= readnow;
                    }
                    currpage++;
                }
                var status = GetStatus();
                if (status != 0x200)
                    Main.SendError(block, status, "reading");
            }
            sw.Stop();
            handle.Close();
            Logger.WriteLine2(string.Format("Dumping completed after: {0} Minutes and {1} seconds", sw.Elapsed.Minutes, sw.Elapsed.Seconds));
        }

        private static void ReadPageInit(uint page)
        {
            ClearStatus();
            _xspi.WriteWord(0x0C, page << 9);
            _xspi.WriteByte(0x08, 0x03, true);
            if (!WaitReady(0x1000))
                return;
            _xspi.WriteReg(0x0C, true, true);
        }

        private static void ReadProc(ref FileStream handle, int pages, ref XNANDSettings nandopts, int mode) {
            var len = pages;
            while (pages-- > 0) {
                _xspi.WriteReg(0x08);
                _xspi.Read(0x10, 4, 32, false, false);
            }
            var data = _xspi.ReadSendReceive(len*4);
            if (data == null)
                return;
            switch (mode) {
                case (int) BwArgs.Modes.Raw:
                    handle.Write(data, 0, data.Length);
                    break;
                case (int) BwArgs.Modes.Glitch:
                    handle.Write(data, 0, data.Length - nandopts.MetaSz);
                    break;
            }
        }

        public static void Erase(int start, int last, ref XNANDSettings nandopts) {
            var sw = new Stopwatch();
            sw.Start();
            LogInit("", start, last, 0, ref nandopts, "erasing");
            for (var block = start; block <= last; block++)
            {
                if (Abort)
                {
                    Main.SendAbort();
                    sw.Stop();
                    Logger.WriteLine2(string.Format("Erasing aborted after: {0} Minutes and {1} seconds", sw.Elapsed.Minutes, sw.Elapsed.Seconds));
                    break;
                }
                Main.SendStatus(block, last);
                EraseBlock(block);
            }
            sw.Stop();
            Logger.WriteLine2(string.Format("Erasing completed after: {0} Minutes and {1} seconds", sw.Elapsed.Minutes, sw.Elapsed.Seconds));
        }

        private static void EraseBlock(int block) {
            ClearStatus();
            var tmp = _xspi.ReadSync(0, 4);
            tmp[0] |= 0x08;
            _xspi.Write(0, tmp);
            _xspi.WriteWord(0x0C, (uint)(block << 9), false);
            _xspi.WriteByte(0x08, 0xAA);
            _xspi.WriteByte(0x08, 0x55);
            _xspi.WriteByte(0x08, 0x5, true);
            if (WaitReady(0x1000))
                return;
            var status = GetStatus();
            if (status != 0x200)
                Main.SendError(block, status, "erasing");
        }

        public static void Write(string filename, int start, int last, int mode, ref XNANDSettings nandopts)
        {
            var sw = new Stopwatch();
            sw.Start();
            LogInit(filename, start, last, mode, ref nandopts, "writing", "Write");
            var fi = new FileInfo(filename);
            if (fi.Exists)
                fi.Delete();
            var handle = fi.OpenRead();
            var currpage = 0;
            for (var block = start; block <= last; block++)
            {
                if (Abort)
                {
                    Main.SendAbort();
                    sw.Stop();
                    handle.Close();
                    Logger.WriteLine2(string.Format("Writing aborted after: {0} Minutes and {1} seconds", sw.Elapsed.Minutes, sw.Elapsed.Seconds));
                    break;
                }
                ClearStatus();
                Main.SendStatus(block, last);
                for (var page = 0; page < nandopts.PagesInBlock; page++)
                {
                    var pagesleft = 0;
                    if (currpage % nandopts.PagesInBlock == 0)
                    {
                        EraseBlock(block);
                        pagesleft = 0x84;
                        WritePageInit();
                    }
                    var len = nandopts.PageSzPhys / 4;
                    while (len > 0)
                    {
                        //TODO: Add crap to deal with ECC patching
                        //TODO: fix write shit
                        var writenow = (len < pagesleft) ? len : pagesleft;
                        WriteProc(data, writenow);
                        pagesleft -= writenow;
                        len -= writenow;
                    }
                    currpage++;
                }
                var status = GetStatus();
                if (status != 0x200)
                    Main.SendError(block, status, "Writing");
            }
            sw.Stop();
            handle.Close();
            Logger.WriteLine2(string.Format("Writing completed after: {0} Minutes and {1} seconds", sw.Elapsed.Minutes, sw.Elapsed.Seconds));
        }

        private static void WritePageInit()
        {
            _xspi.WriteReg(0x0C, true, true);
        }

        private static void WriteProc(byte[] data, int pages)
        {
            var offset = 0;
            var buf = new byte[4];
            while (pages-- > 0)
            {
                Array.Copy(data, offset, buf, 0, 4);
                _xspi.Write(0x10, buf, false, false);
                _xspi.WriteByte(0x08, 0x01);
                offset += 4;
            }
        }

        private static void WriteExecute(int block) {
            _xspi.WriteWord(0x0C, (uint)(block << 9));
            _xspi.WriteByte(0x08, 0x55);
            _xspi.WriteByte(0x08, 0xAA);
            _xspi.WriteByte(0x08, 0x4, true);
            if (WaitReady(0x1000))
                return;
            var status = GetStatus();
            if (status != 0x200)
                Main.SendError(block, status, "writing");
        }
    }
}