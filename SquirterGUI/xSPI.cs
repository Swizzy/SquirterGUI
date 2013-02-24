namespace SquirterGUI {
    using System;
    using System.Threading;

    internal class xSPI {
        private readonly FtdcspiWrapper _wrapper;
        internal readonly bool IsOk;

        public xSPI() {
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

        internal void Poweron()
        {
            throw new NotImplementedException("Poweron");
        }

        byte[] Read(byte reg, int len) {
            var ret = new byte[len];
            var wbuf = new byte[] { (byte) ((reg << 2) | 1), 0xFF };
            _wrapper.EnableSpiChip();
            _wrapper.AddWriteOutBuffer(0x20, wbuf);
            _wrapper.AddReadOutBuffer(0x20);
            _wrapper.DisableSpiChip();
            _wrapper.SetAnswerFast();
            _wrapper.SendBytesToDevice();
            _wrapper.GetDataFromDevice(4, ref ret, 0);
            return ret;
        }

        byte[] ReadSync(byte reg, int len) {
            var ret = new byte[len];
            var wbuf = new byte[] { (byte)((reg << 2) | 1), 0xFF };
            _wrapper.ClearOutputBuffer();
            _wrapper.EnableSpiChip();
            _wrapper.AddWriteOutBuffer(0x20, wbuf);
            _wrapper.AddReadOutBuffer(0x20);
            _wrapper.DisableSpiChip();
            _wrapper.SetAnswerFast();
            _wrapper.SendBytesToDevice();
            _wrapper.GetDataFromDevice(4, ref ret, 0);
            return ret;
        }

        uint ReadWord(byte reg) {
            var ret = new byte[2];
            var wbuf = new byte[] { (byte)((reg << 2) | 1), 0xFF };
            _wrapper.ClearOutputBuffer();
            _wrapper.EnableSpiChip();
            _wrapper.AddWriteOutBuffer(0x20, wbuf);
            _wrapper.AddReadOutBuffer(0x10);
            _wrapper.DisableSpiChip();
            _wrapper.SetAnswerFast();
            _wrapper.SendBytesToDevice();
            _wrapper.GetDataFromDevice(2, ref ret, 0);
            return ret[0] | ((uint)ret[1]<<8);
        }

        byte ReadByte(byte reg) {
            var ret = new byte[1];
            var wbuf = new byte[] { (byte) ((reg << 2) | 1), 0xFF };
            _wrapper.ClearOutputBuffer();
            _wrapper.EnableSpiChip();
            _wrapper.AddWriteOutBuffer(0x20, wbuf);
            _wrapper.AddReadOutBuffer(0x8);
            _wrapper.DisableSpiChip();
            _wrapper.SetAnswerFast();
            _wrapper.SendBytesToDevice();
            _wrapper.GetDataFromDevice(1, ref ret, 0);
            return ret[0];
        }

        void Write(byte reg, byte[] data)
        {
            if (data.Length != 4)
                return;
            byte[] wbuf = new byte[5];
            wbuf[0] = (byte)((reg << 2) | 2);
            Array.Copy(data, 0, wbuf, 1, data.Length);
            DoWrite(wbuf);
        }

        void WriteSync(byte reg, byte[] data) {
            if (data.Length != 4)
                return;
            byte[] wbuf = new byte[5];
            wbuf[0] = (byte) ((reg << 2) | 2);
            Array.Copy(data, 0, wbuf, 1, data.Length);
            DoWrite(wbuf, true);
        }

        void WriteWord(byte reg, uint data)
        {
            byte[] wbuf = new byte[5];
            wbuf[0] = (byte)((reg << 2) | 2);
            var tmp = BitConverter.GetBytes(data);
            Array.Copy(tmp, 0, wbuf, 1, tmp.Length);
            DoWrite(wbuf, true);
        }

        void WriteByte(byte reg, byte data, bool send = false) {
            byte[] wbuf = new byte[5];
            wbuf[0] = (byte) ((reg << 2) | 2);
            wbuf[1] = data;
            DoWrite(wbuf, false, send);
        }

        void WriteReg(byte reg) {
            byte[] wbuf = new byte[5];
            wbuf[0] = (byte)((reg << 2) | 2);
            DoWrite(wbuf, false, false);
        }

        void DoWrite(byte[] wbuf, bool clear = false, bool send = true)
        {
            if (clear)
                _wrapper.ClearOutputBuffer();
            _wrapper.EnableSpiChip();
            _wrapper.AddWriteOutBuffer(0x28, wbuf);
            _wrapper.DisableSpiChip();
            if (send)
                _wrapper.SendBytesToDevice();
        }

    }
}