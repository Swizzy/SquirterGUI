using System;

namespace SquirterGUI {
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class FtdcspiWrapper {
        #region DLL Crap

        #region Constants

        private const uint FtcSuccess = 0;
        private const uint FtcDeviceInUse = 27;

        private const uint MaxNumDeviceNameChars = 100;
        private const uint MaxNumChannelChars = 5;

        private const uint MaxNumDllVersionChars = 10;

        //private const uint MaxNumErrorMessageChars = 100;
        // To communicate with the 93LC56(2048 word) EEPROM, the maximum frequency the clock can be set is 1MHz
        //private const uint MaxFreq93Lc56ClockDivisor = 29; // equivalent to 1MHz

        //private const bool ADBUS3ChipSelect = false;

        //private const uint ADBUS2DataIn = 0;

        //private const int WriteControlBufferSize = 256;
        //private const int WriteDataBufferSize = 65536;
        //private const int ReadDataBufferSize = 65536;
        //private const int ReadCmdsDataBufferSize = 131071;

        //private const uint SpiewenCmdIndex = 0;
        //private const uint SpiewdsCmdIndex = 1;
        //private const uint SpieralCmdIndex = 2;

        //private const uint MaxSpi93Lc56ChipSizeInWords = 128;

        //private const uint Num93Lc56BCmdContolBits = 11;
        //private const uint Num93Lc56BCmdContolBytes = 2;

        //private const uint Num93Lc56BCmdDataBits = 16;
        //private const uint Num93Lc56BCmdDataBytes = 2;


        //private enum HiSpeedDeviceTypes : uint
        //{
        //    Ft2232HDeviceType = 1,
        //    Ft4232HDeviceType = 2
        //};

        private const int MaxFreqClockDivisor = 1;

        private const int MaxNumBytesUsbWrite = 4096;

        private const byte ChipSelectPin = 0x8;
        
        private const byte SetLowByteDataBitsCmd = 0x80;

        //private const byte SetHighByteDataBitsCmd = 0x82;

        private const byte SendAnswerBackImmediatelyCmd = 0x87;
        
        private const byte ClkDataBytesOutOnNegClkLsbFirstCmd = 0x19;
        private const byte ClkDataBitsOutOnNegClkLsbFirstCmd = 0x1B;
        private const byte ClkDataBytesInOnNegClkLsbFirstCmd = 0x2D;
        private const byte ClkDataBitsInOnNegClkLsbFirstCmd = 0x2F;

        #endregion Constants

        #region DLL Imports

        [DllImport("ftd2xx.dll", EntryPoint = "FT_Write", CallingConvention = CallingConvention.Cdecl)] private static extern uint FT_Write(IntPtr ftHandle, byte[] lpBuffer, uint dwBytesToWrite, ref uint lpBytesWritten);

        [DllImport("ftd2xx.dll", EntryPoint = "FT_Read", CallingConvention = CallingConvention.Cdecl)] private static extern uint FT_Read(IntPtr ftHandle, ref byte[] lpBuffer, uint dwBytesToRead, ref uint lpBytesReturned);

        [DllImport("ftcspi.dll", EntryPoint = "SPI_GetDllVersion",
            CallingConvention = CallingConvention.Cdecl)] private static extern uint GetDllVersion(byte[] pDllVersion, uint buufferSize);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_AddHiSpeedDeviceReadCmd(IntPtr ftHandle,
        //                                 ref FtcInitCondition pReadStartCondition,
        //                                 bool bClockOutControBitsMsbFirst,
        //                                 bool bClockOutControBitsPosEdge,
        //                                 uint numControlBitsToWrite,
        //                                 byte[] pWriteControlBuffer,
        //                                 uint numControlBytesToWrite,
        //                                 bool bClockInDataBitsMsbFirst,
        //                                 bool bClockInDataBitsPosEdge,
        //                                 uint numDataBitsToRead,
        //                                 ref FthHigherOutputPins
        //                                     pHighPinsReadActiveStates);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_ClearDeviceCmdSequence(IntPtr ftHandle);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_Close(IntPtr ftHandle);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_CloseDevice(IntPtr ftHandle,
        //                     ref FtcCloseFinalStatePins
        //                         pCloseFinalStatePinsData);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_ExecuteDeviceCmdSequence(IntPtr ftHandle,
        //                                  byte[] pReadCmdSequenceDataBuffer,
        //                                  out uint pnumDataBytesReturned);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_GetClock(uint clockDivisor, ref uint clockFrequencyHz);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_GetDeviceLatencyTimer(IntPtr ftHandle, ref byte timerValue);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_GetErrorCodeString(string language,
        //                            uint statusCode,
        //                            byte[] pErrorMessage,
        //                            uint bufferSize);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_GetHiSpeedDeviceClock(uint clockDivisor,
                                       ref uint clockFrequencyHz);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_GetHiSpeedDeviceGPIOs(IntPtr ftHandle,
        //                               out FthLowHighPins pHighPinsInputData);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_GetHiSpeedDeviceNameLocIDChannel(uint deviceNameIndex,
                                                  byte[] pDeviceName,
                                                  uint deviceNameBufferSize,
                                                  ref uint locationID,
                                                  byte[] pChannel,
                                                  uint channelBufferSize,
                                                  ref uint hiSpeedDeviceType);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_GetHiSpeedDeviceType(IntPtr ftHandle,
                                      ref uint hiSpeedDeviceType);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_GetNumHiSpeedDevices(ref uint numHiSpeedDevices);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_InitDevice(IntPtr ftHandle, uint clockDivisor);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_OpenHiSpeedDevice(string deviceName,
                                   uint locationID,
                                   string channel,
                                   ref IntPtr pFtHandle);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_ReadHiSpeedDevice(IntPtr ftHandle,
        //                           ref FtcInitCondition pReadStartCondition,
        //                           bool bClockOutControBitsMsbFirst,
        //                           bool bClockOutControBitsPosEdge,
        //                           uint numControlBitsToWrite,
        //                           byte[] pWriteControlBuffer,
        //                           uint numControlBytesToWrite,
        //                           bool bClockInDataBitsMsbFirst,
        //                           bool bClockInDataBitsPosEdge,
        //                           uint numDataBitsToRead,
        //                           byte[] pReadDataBuffer,
        //                           out uint pnumDataBytesReturned,
        //                           ref FthHigherOutputPins
        //                               pHighPinsReadActiveStates);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_SetClock(IntPtr ftHandle,
                          uint clockDivisor,
                          ref uint clockFrequencyHz);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_SetDeviceLatencyTimer(IntPtr ftHandle, byte timerValue);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_SetHiSpeedDeviceGPIOs(IntPtr ftHandle,
                                       ref FtcChipSelectPins
                                           pChipSelectsDisableStates,
                                       ref FthInputOutputPins
                                           pHighInputOutputPinsData);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_SetLoopback(IntPtr ftHandle, bool bLoopBackState);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_TurnOffDivideByFiveClockingHiSpeedDevice(IntPtr ftHandle);

        [DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        uint SPI_TurnOnDivideByFiveClockingHiSpeedDevice(IntPtr ftHandle);

        //[DllImport("ftcspi.dll", CallingConvention = CallingConvention.Cdecl)] private static extern
        //uint SPI_WriteHiSpeedDevice(IntPtr ftHandle,
        //                            ref FtcInitCondition pWriteStartCondition,
        //                            bool bClockOutDataBitsMsbFirst,
        //                            bool bClockOutDataBitsPosEdge,
        //                            uint numControlBitsToWrite,
        //                            byte[] pWriteControlBuffer,
        //                            uint numControlBytesToWrite,
        //                            bool bWriteDataBits,
        //                            uint numDataBitsToWrite,
        //                            byte[] pWriteDataBuffer,
        //                            uint numDataBytesToWrite,
        //                            ref FtcWaitDataWrite
        //                                pWaitDataWriteComplete,
        //                            ref FthHigherOutputPins
        //                                pHighPinsWriteActiveStates);

        #endregion DLL Imports

        #region Structs

        public struct FtcCloseFinalStatePins {
            public bool BtckPinActiveState;
            public bool BtckPinState;
            public bool BtdiPinActiveState;
            public bool BtdiPinState;
            public bool BTmsPinActiveState;
            public bool BTmsPinState;
        }

        public struct FtcChipSelectPins {
            public bool Badbus3ChipSelectPinState;
            public bool Badbus4GPIOL1PinState;
            public bool Badbus5GPIOL2PinState;
            public bool Badbus6GPIOL3PinState;
            public bool Badbus7GPIOL4PinState;
        }

        public struct FtcHigherOutputPins {
            public bool BPin1ActiveState;
            public bool BPin1State;
            public bool BPin2ActiveState;
            public bool BPin2State;
            public bool BPin3ActiveState;
            public bool BPin3State;
            public bool BPin4ActiveState;
            public bool BPin4State;
        }

        public struct FtcInitCondition {
            public bool BChipSelectPinState;
            public bool BClockPinState;
            public bool BDataOutPinState;
            public bool ChipSelectPinValue;
        }

        public struct FtcInputOutputPins {
            public bool BPin1InputOutputState;
            public bool BPin1LowHighState;
            public bool BPin2InputOutputState;
            public bool BPin2LowHighState;
            public bool BPin3InputOutputState;
            public bool BPin3LowHighState;
            public bool BPin4InputOutputState;
            public bool BPin4LowHighState;
        }

        public struct FtcLowHighPins {
            public bool BPin1LowHighState;
            public bool BPin2LowHighState;
            public bool BPin3LowHighState;
            public bool BPin4LowHighState;
        }

        public struct FtcWaitDataWrite {
            public bool BDataWriteCompleteState;
            public bool BWaitDataWriteComplete;
            public uint DataWriteTimeoutmSecs;
            public uint WaitDataWritePin;
        }

        public struct FthHigherOutputPins {
            public bool BPin1ActiveState;
            public bool BPin1State;
            public bool BPin2ActiveState;
            public bool BPin2State;
            public bool BPin3ActiveState;
            public bool BPin3State;
            public bool BPin4ActiveState;
            public bool BPin4State;
            public bool BPin5ActiveState;
            public bool BPin5State;
            public bool BPin6ActiveState;
            public bool BPin6State;
            public bool BPin7ActiveState;
            public bool BPin7State;
            public bool BPin8ActiveState;
            public bool BPin8State;
        }

        public struct FthInputOutputPins {
            public bool BPin1InputOutputState;
            public bool BPin1LowHighState;
            public bool BPin2InputOutputState;
            public bool BPin2LowHighState;
            public bool BPin3InputOutputState;
            public bool BPin3LowHighState;
            public bool BPin4InputOutputState;
            public bool BPin4LowHighState;
            public bool BPin5InputOutputState;
            public bool BPin5LowHighState;
            public bool BPin6InputOutputState;
            public bool BPin6LowHighState;
            public bool BPin7InputOutputState;
            public bool BPin7LowHighState;
            public bool BPin8InputOutputState;
            public bool BPin8LowHighState;
        }

        public struct FthLowHighPins {
            public bool BPin1LowHighState;
            public bool BPin2LowHighState;
            public bool BPin3LowHighState;
            public bool BPin4LowHighState;
            public bool BPin5LowHighState;
            public bool BPin6LowHighState;
            public bool BPin7LowHighState;
            public bool BPin8LowHighState;
        }

        #endregion Structs

        #endregion DLL Crap

        private uint _status;
        readonly byte[] _byOutputBuffer = new byte[65535];
        byte _dwLowPinsValue;
        uint _dwNumBytesToSend; // Index to the output buffer
        uint _dwNumBytesSent; // Count of actual bytes sent - used with FT_Write
        private IntPtr _ftHandle;

        private FtcChipSelectPins _chipSelectsDisableStates;
        private FthInputOutputPins _highInputOutputPins;


        public bool SpiInit() {
            uint dwNumHiSpeedDevices = 0, dwLocationID = 0, dwHiSpeedDeviceType = 0, dwHiSpeedDeviceIndex = 0, dwClockFrequencyHz = 0;
            var szDeviceName = new byte[MaxNumDeviceNameChars];
            var szChannel = new byte[MaxNumChannelChars];
            var channel = "";
            byte timerValue = 0;
            var szDllVersion = new byte[MaxNumDllVersionChars];
            _status = GetDllVersion(szDllVersion, MaxNumDllVersionChars);
            if (_status != FtcSuccess)
                return false;
            //Below code is for debugging version of DLL found...

            //if (_status == FtcSuccess) {
            //    var dllVersion = Encoding.ASCII.GetString(szDllVersion);
            //    dllVersion = dllVersion.Substring(0,
            //                                      dllVersion.IndexOf("\0", StringComparison.Ordinal));
            //    MessageBox.Show(string.Format("DLL Version: {0}", dllVersion));
            //}
            _status = SPI_GetNumHiSpeedDevices(ref dwNumHiSpeedDevices);
            if ((_status == FtcSuccess) && (dwNumHiSpeedDevices > 0)) {
                do {
                    _status = SPI_GetHiSpeedDeviceNameLocIDChannel(dwHiSpeedDeviceIndex, szDeviceName, MaxNumDeviceNameChars, ref dwLocationID, szChannel, MaxNumChannelChars, ref dwHiSpeedDeviceType);
                    dwHiSpeedDeviceIndex = dwHiSpeedDeviceIndex + 1;
                    if (_status != FtcSuccess)
                        continue;
                    channel = Encoding.ASCII.GetString(szChannel);
                    channel = channel.Substring(0, channel.IndexOf("\0", StringComparison.Ordinal));
                    if (channel.Equals("B", StringComparison.CurrentCultureIgnoreCase))
                        break;
                }
                while ((_status == FtcSuccess) && (dwHiSpeedDeviceIndex < dwNumHiSpeedDevices));
                if (_status == FtcSuccess) {
                    if (!channel.Equals("B", StringComparison.CurrentCultureIgnoreCase))
                        _status = FtcDeviceInUse;
                }
                var deviceName = Encoding.ASCII.GetString(szDeviceName);
                deviceName = deviceName.Substring(0, deviceName.IndexOf("\0", StringComparison.Ordinal));
                if (_status == FtcSuccess) {
                    _status = SPI_OpenHiSpeedDevice(deviceName, dwLocationID, channel, ref _ftHandle);
                    if (_status == FtcSuccess) {
                        _status = SPI_GetHiSpeedDeviceType(_ftHandle, ref dwHiSpeedDeviceType);

                        //Below is for debugging device type found...

                        //if (_status == FtcSuccess) {
                        //    var szDeviceDetails = "Type = ";
                        //    switch (dwHiSpeedDeviceType) {
                        //        case (uint)HiSpeedDeviceTypes.Ft4232HDeviceType:
                        //            szDeviceDetails += "FT4232H";
                        //            break;
                        //        case (uint)HiSpeedDeviceTypes.Ft2232HDeviceType:
                        //            szDeviceDetails += "FT2232H";
                        //            break;
                        //    }

                        //    szDeviceDetails += ", Name = ";
                        //    szDeviceDetails += deviceName;

                        //    MessageBox.Show(string.Format("Hi Speed Device:\n{0}", szDeviceDetails));
                        //}
                    }
                }
            }

            if ((_status == FtcSuccess) && (_ftHandle != (IntPtr) 0)) {
                _status = SPI_InitDevice(_ftHandle, MaxFreqClockDivisor); //65536

                if (_status == FtcSuccess) {
                    _status = SPI_GetDeviceLatencyTimer(_ftHandle, ref timerValue);
                    if (_status == FtcSuccess){
                        _status = SPI_SetDeviceLatencyTimer(_ftHandle, 50);
                        if (_status == FtcSuccess){
                            SPI_GetDeviceLatencyTimer(_ftHandle, ref timerValue);
                            SPI_SetDeviceLatencyTimer(_ftHandle, 1);
                            _status = SPI_GetDeviceLatencyTimer(_ftHandle, ref timerValue);
                        }
                    }
                }
                if (_status == FtcSuccess) {
                    _status = SPI_GetHiSpeedDeviceClock(0, ref dwClockFrequencyHz);
                    if (_status == FtcSuccess)
                        _status = SPI_TurnOnDivideByFiveClockingHiSpeedDevice(_ftHandle);
                    if (_status == FtcSuccess) {
                        SPI_GetHiSpeedDeviceClock(0, ref dwClockFrequencyHz);
                        _status = SPI_SetClock(_ftHandle, MaxFreqClockDivisor, ref dwClockFrequencyHz);
                        if (_status == FtcSuccess) {
                            _status = SPI_TurnOffDivideByFiveClockingHiSpeedDevice(_ftHandle);
                            if (_status == FtcSuccess)
                                _status = SPI_SetClock(_ftHandle, MaxFreqClockDivisor,ref dwClockFrequencyHz);
                        }
                    }
                }
            }
            if (_status == FtcSuccess) {
                // Must set the chip select disable states for all the SPI devices connected to a FT2232H hi-speed dual
                // device or FT4332H hi-speed quad device
                _chipSelectsDisableStates.Badbus3ChipSelectPinState = true;
                _chipSelectsDisableStates.Badbus4GPIOL1PinState = false;
                _chipSelectsDisableStates.Badbus5GPIOL2PinState = false;
                _chipSelectsDisableStates.Badbus6GPIOL3PinState = false;
                _chipSelectsDisableStates.Badbus7GPIOL4PinState = false;

                _highInputOutputPins.BPin1InputOutputState = true;
                _highInputOutputPins.BPin1LowHighState = false;
                _highInputOutputPins.BPin2InputOutputState = true;
                _highInputOutputPins.BPin2LowHighState = true;
                _highInputOutputPins.BPin3InputOutputState = false;
                _highInputOutputPins.BPin3LowHighState = false;
                _highInputOutputPins.BPin4InputOutputState = false;
                _highInputOutputPins.BPin4LowHighState = false;
                _highInputOutputPins.BPin5InputOutputState = false;
                _highInputOutputPins.BPin5LowHighState = false;
                _highInputOutputPins.BPin6InputOutputState = false;
                _highInputOutputPins.BPin6LowHighState = false;
                _highInputOutputPins.BPin7InputOutputState = false;
                _highInputOutputPins.BPin7LowHighState = false;
                _highInputOutputPins.BPin8InputOutputState = false;
                _highInputOutputPins.BPin8LowHighState = false;

                _status = SPI_SetHiSpeedDeviceGPIOs(_ftHandle,
                                                    ref _chipSelectsDisableStates,
                                                    ref _highInputOutputPins);
            }
            return (_status == FtcSuccess && dwNumHiSpeedDevices > 0);
        }

        public void SendBytesToDevice()
        {
            _status = FtcSuccess;
            uint numBytesSent = 0;
            uint dwTotalNumBytesSent = 0;

            if (_dwNumBytesToSend > MaxNumBytesUsbWrite) {
                do {
                    uint dwNumDataBytesToSend;
                    if ((dwTotalNumBytesSent + MaxNumBytesUsbWrite) <= _dwNumBytesToSend)
                        dwNumDataBytesToSend = MaxNumBytesUsbWrite;
                    else
                        dwNumDataBytesToSend = (_dwNumBytesToSend - dwTotalNumBytesSent);
                    _status = FT_Write(_ftHandle,
                                       _byOutputBuffer,
                                       dwNumDataBytesToSend,
                                       ref numBytesSent);
                    dwTotalNumBytesSent = dwTotalNumBytesSent + numBytesSent;
                }
                while ((dwTotalNumBytesSent < _dwNumBytesToSend) && (_status == FtcSuccess));
            }
            else
                _status = FT_Write(_ftHandle, _byOutputBuffer, _dwNumBytesToSend, ref numBytesSent);
            _dwNumBytesToSend = 0;
        }

        public void ClearOutputBuffer()
        {
            _dwNumBytesToSend = 0;
        }

        private void AddByteToOutputBuffer(byte dataByte, bool bClearOutputBuffer)
        {
            if(bClearOutputBuffer)
                ClearOutputBuffer();
            _byOutputBuffer[_dwNumBytesToSend++] = dataByte;
        }

        public void SetAnswerFast()
        {
            AddByteToOutputBuffer(SendAnswerBackImmediatelyCmd, false);	
        }

        public void GetDataFromDevice(uint dwNumBytesToRead, ref byte[] readDataBuffer, int offset)
        {
            var buf = new byte[dwNumBytesToRead];
            uint dwNumBytesRead = 0;
            var tryCount = 10;
            do {
                FT_Read(_ftHandle, ref buf, dwNumBytesToRead, ref dwNumBytesRead);
                dwNumBytesToRead -= dwNumBytesRead;
                if (dwNumBytesToRead == 0)
                    Buffer.BlockCopy(buf, 0, readDataBuffer, offset, buf.Length);
                else {
                    Buffer.BlockCopy(buf, 0, readDataBuffer, offset, (int) dwNumBytesRead);
                    offset += (int) dwNumBytesRead;
                }
            } while(dwNumBytesToRead > 0 && tryCount-- > 0);
            if( tryCount <= 0 )
                throw new Exception("ERROR: NO DATA FROM DEVICE"); 
        }

        public void DisableSpiChip()
        {
            AddByteToOutputBuffer(SetLowByteDataBitsCmd, false);
            _dwLowPinsValue = (byte) (_dwLowPinsValue | ChipSelectPin); // set CS to low
            // set SK, DO, CS and GPIOL1-4 as output, set D1 as input
            AddByteToOutputBuffer(_dwLowPinsValue, false);
            AddByteToOutputBuffer(0xFB, false);
        }

        public void EnableSpiChip()
        {
            AddByteToOutputBuffer(SetLowByteDataBitsCmd, false);
            _dwLowPinsValue = (byte) (_dwLowPinsValue & ~ChipSelectPin); // set CS to low
            // set SK, DO, CS and GPIOL1-4 as output, set D1 as input
            AddByteToOutputBuffer(_dwLowPinsValue, false);
            AddByteToOutputBuffer(0xFB, false);
        }

        public void AddWriteOutBuffer(uint dwNumControlBitsToWrite, IList<byte> pWriteControlBuffer)
        {
            var dwControlBufferIndex = 0;

            // kra - 040608, added test for number of control bits to write, because for SPI only, the number of control
            // bits to write can be 0 on some SPI devices, before a read operation is performed
            if (dwNumControlBitsToWrite <= 1)
                return;
            // adjust for bit count of 1 less than no of bits
            var dwModNumControlBitsToWrite = dwNumControlBitsToWrite - 1;
            // Number of control bytes is greater than 0, only if the minimum number of control bits is 8
            var dwNumControlBytes = dwModNumControlBitsToWrite / 8;

            if (dwNumControlBytes > 0)
            {
                // Number of whole bytes
                dwNumControlBytes = (dwNumControlBytes - 1);

                // clk data bytes out
                AddByteToOutputBuffer(ClkDataBytesOutOnNegClkLsbFirstCmd, false);
                AddByteToOutputBuffer((byte) (dwNumControlBytes & 0xFF), false);
                AddByteToOutputBuffer((byte) ((dwNumControlBytes / 256) & 0xFF), false);
                // now add the data bytes to go out
                do
                {
                    AddByteToOutputBuffer(pWriteControlBuffer[dwControlBufferIndex], false);
                    dwControlBufferIndex = (dwControlBufferIndex + 1);
                }
                while (dwControlBufferIndex < (dwNumControlBytes + 1));
            }

            var dwNumRemainingControlBits = (dwModNumControlBitsToWrite % 8);

            // do remaining bits
            if (dwNumRemainingControlBits <= 0)
                return;
            // clk data bits out
            //*lpdwDataWriteBytesCommand = CLK_DATA_BYTES_OUT_ON_NEG_CLK_LSB_FIRST_CMD;
            //*lpdwDataWriteBitsCommand = CLK_DATA_BITS_OUT_ON_NEG_CLK_LSB_FIRST_CMD;
            AddByteToOutputBuffer(ClkDataBitsOutOnNegClkLsbFirstCmd, false);
            AddByteToOutputBuffer((byte) (dwNumRemainingControlBits & 0xFF), false);
            AddByteToOutputBuffer( pWriteControlBuffer[dwControlBufferIndex], false);
        }

        public void AddReadOutBuffer(uint dwNumDataBitsToRead)
        {
            // adjust for bit count of 1 less than no of bits
            var dwModNumBitsToRead = (dwNumDataBitsToRead - 1);

            var dwNumDataBytes = (dwModNumBitsToRead / 8);

            if (dwNumDataBytes > 0)
            {
                // Number of whole bytes
                dwNumDataBytes = (dwNumDataBytes - 1);

                // clk data bytes out
                AddByteToOutputBuffer(ClkDataBytesInOnNegClkLsbFirstCmd, false);
                AddByteToOutputBuffer((byte) (dwNumDataBytes & 0xFF), false);
                AddByteToOutputBuffer((byte) ((dwNumDataBytes / 256) & 0xFF), false);
            }

            // number of remaining bits
            var dwNumRemainingDataBits = (dwModNumBitsToRead % 8);

            if (dwNumRemainingDataBits <= 0)
                return;
            // clk data bits out
            AddByteToOutputBuffer(ClkDataBitsInOnNegClkLsbFirstCmd, false);
            AddByteToOutputBuffer((byte) (dwNumRemainingDataBits & 0xFF), false);
        }

        public void SpiSetCs(bool chipSelect)
        {

            _dwNumBytesToSend = 0; // Index to the output buffer
            _dwNumBytesSent = 0; // Count of actual bytes sent - used with FT_Write
            _byOutputBuffer[_dwNumBytesToSend++] = 0x80;
            _dwLowPinsValue = (byte) (chipSelect ? 0x08 : 0x00);
            _byOutputBuffer[_dwNumBytesToSend++] = _dwLowPinsValue;
            _byOutputBuffer[_dwNumBytesToSend++] = 0x3E; // byDirection
            var ftStatus = FT_Write(_ftHandle, _byOutputBuffer, _dwNumBytesToSend, ref _dwNumBytesSent);
            _dwNumBytesToSend = 0;
        }

        public void SpiSetGpio(bool xxLo, bool ejLo)
        {
            _dwNumBytesToSend = 0; // Index to the output buffer
            _dwNumBytesSent = 0; // Count of actual bytes sent - used with FT_Write
            var dwNumBytesToRead = 0;

            _byOutputBuffer[_dwNumBytesToSend++] = 0x80;
            _dwLowPinsValue = 0;
            if (xxLo)
                _dwLowPinsValue = (byte) (_dwLowPinsValue | 0x10);
            else 
                _dwLowPinsValue = (byte) (_dwLowPinsValue | 0x00);
            if (ejLo)
                _dwLowPinsValue = (byte)(_dwLowPinsValue | 0x20);
            else
                _dwLowPinsValue = (byte)(_dwLowPinsValue | 0x00);
            _byOutputBuffer[_dwNumBytesToSend++] = _dwLowPinsValue; 
            _byOutputBuffer[_dwNumBytesToSend++] = 0x3E; // byDirection
            var ftStatus = FT_Write(_ftHandle, _byOutputBuffer, _dwNumBytesToSend, ref _dwNumBytesSent);
            _dwNumBytesToSend = 0;
        }
    }
}