namespace SquirterGUI {
    using System;
    using System.Threading;

    internal class XSPI {
        private readonly FtdcspiWrapper _wrapper;
        internal readonly bool IsOk;

        public XSPI() {
            _wrapper = new FtdcspiWrapper();
            IsOk = _wrapper.SpiInit();
        }

        internal void EnterFlashMode() {
            _wrapper.SpiSetGpio(false, true);
            _wrapper.SpiSetCs(true);
            Thread.Sleep(35);
            _wrapper.SpiSetGpio(false, false);
            _wrapper.SpiSetCs(false);
            Thread.Sleep(35);
            _wrapper.SpiSetGpio(true, true);
            Thread.Sleep(35);
        }

        internal void LeaveFlashMode() {
            throw new NotImplementedException("LeaveFlashMode");
        }

        internal void Shutdown() {
            throw new NotImplementedException("Shutdown");
        }

        internal void Poweron() {
            throw new NotImplementedException("Poweron");
        }

        public byte[] Read(byte reg, int len, uint rlen = 0x20, bool clear = false, bool sendReceive = true) {
            var wbuf = new byte[] {
                                      (byte) ((reg << 2) | 1), 0xFF
                                  };
            if (clear)
                _wrapper.ClearOutputBuffer();
            _wrapper.EnableSpiChip();
            _wrapper.AddWriteOutBuffer((uint) (wbuf.Length*8), wbuf);
            _wrapper.AddReadOutBuffer(rlen);
            _wrapper.DisableSpiChip();
            return sendReceive ? ReadSendReceive(len) : null;
        }

        public byte[] ReadSendReceive(int len) {
            var ret = new byte [len];
            _wrapper.SetAnswerFast();
            _wrapper.SendBytesToDevice();
            return _wrapper.GetDataFromDevice((uint)len, ref ret) ? ret : null;
        }

        public byte[] ReadSync(byte reg, int len, bool sendReceive = true) {
            return Read(reg, len, 0x20, true, sendReceive);
        }

        public uint ReadWord(byte reg, bool sendReceive = true) {
            var ret = Read(reg, 1, 0x10, true, sendReceive);
            return ret != null ? ret[0] | ((uint) ret[1] << 8) : 0;
        }

        public byte ReadByte(byte reg, bool sendReceive = true) {
            var ret = Read(reg, 1, 8, true, sendReceive);
            return ret != null ? ret[0] : new byte();
        }

        public void Write(byte reg, byte[] data, bool clear = true, bool send = true)
        {
            if (data == null || data.Length != 4)
                return;
            var wbuf = new byte[5];
            wbuf[0] = (byte) ((reg << 2) | 2);
            Array.Copy(data, 0, wbuf, 1, data.Length);
            if (clear)
                _wrapper.ClearOutputBuffer();
            _wrapper.EnableSpiChip();
            _wrapper.AddWriteOutBuffer((uint)(wbuf.Length * 8), wbuf);
            _wrapper.DisableSpiChip();
            if (send)
                _wrapper.SendBytesToDevice();
        }

        public void WriteWord(byte reg, uint data, bool clear = true) {
            Write(reg, BitConverter.GetBytes(data), clear);
        }

        public void WriteByte(byte reg, byte data, bool send = false) {
            Write(reg, new byte[] { data, 0, 0, 0 }, false, send);
        }

        public void WriteReg(byte reg, bool clear = false, bool send = false) {
            Write(reg, new byte[4], clear, send);
        }
    }
}