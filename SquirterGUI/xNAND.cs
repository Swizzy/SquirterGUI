namespace SquirterGUI {
    using System;
    using System.IO;
    using System.Windows.Forms;
    using SquirterGUI.Properties;

    internal class XNAND {
        public readonly bool IsOk;
        private uint _config;
        private static XSPI _xspi;

        public XNAND() {
            _xspi = new XSPI();
            IsOk = _xspi.IsOk;
        }

        private static bool WaitReady(uint timeout) {
            do {
                if ((_xspi.ReadByte(0x04) & 0x01) == 0)
                    return true;
            }
            while (timeout-- > 0);

            return false;
        }

        private uint GetStatus() {
            return _xspi.ReadWord(0x04);
        }

        private static void ClearStatus() {
            //var buf = new byte[] { 0,2,0,0 }; //Is it really needed?
            var buf = _xspi.ReadSync(4, 4);
            _xspi.WriteSync(4, buf);
        }

        public void SetConfig(uint config) {
            _config = config;
        }

        public static uint FlashDataInit() {
            _xspi.EnterFlashMode();
            //_xspi.ReadSync(0, 4);
            var ret = _xspi.ReadSync(0, 4);
            if (ret == null || (ret[0] == ret[1] && ret[0] == ret[2] && ret[0] == ret[3]))
                return 0;
            return BitConverter.ToUInt32(ret, 0);
        }

        public XNANDSettings GetSettings() {
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

        public static void Read(string filename, int start, int last, int mode, ref XNANDSettings nandopts)
        {
            var fi = new FileInfo(filename);
            var handle = fi.OpenWrite();
            for (var block = start; block <= last; block++)
            {
                if (mode == (int)BwArgs.Modes.Raw)
                    ReadRaw(ref handle, ref block, ref nandopts, last);
            }
        }

        private static void ReadRaw(ref FileStream handle, ref int block, ref XNANDSettings nandopts, int last) {
            var len = nandopts.PageSzPhys/4;
            var pagesleft = 0;
            uint status = 0;
            while (len > 0) {
                Main.SendStatus(block, last);
                if (pagesleft == 0) {
                    status |= ReadRawInit((uint)block);
                    block++;
                    pagesleft = 0x84;
                }
                var readnow = (len < pagesleft)? len: pagesleft;
                ReadRawProc(ref handle, ref block, readnow);
                pagesleft-= readnow;
                len-= readnow;
            }
        }

        private static uint ReadRawInit(uint block) {
            ClearStatus();
            _xspi.WriteWord(0x0C, block << 9);
            _xspi.WriteByte(0x08, 0x03, true);
            if (!WaitReady(0x1000))
                return 0x8011;
            _xspi.WriteReg(0x0C, true, true);
            return 0;
        }

        private static void ReadRawProc(ref FileStream handle, ref int block, int pages) {
            var len = pages;
            while (pages-- > 0) {
                _xspi.WriteReg(0x08);
                _xspi.Read(0x10, 4, false);
            }
            var data = _xspi.ReadSendReceive(len*4);
            if (data != null)
                handle.Write(data, 0, data.Length);
        }
    }
}