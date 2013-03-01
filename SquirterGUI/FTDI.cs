/*
** FTD2XX_NET.cs
**
** Copyright © 2009-2012 Future Technology Devices International Limited
**
** C# Source file for .NET wrapper of the Windows FTD2XX.dll API calls.
** Main module
**
** Author: FTDI
** Project: CDM Windows Driver Package
** Module: FTD2XX_NET Managed Wrapper
** Requires: 
** Comments:
**
** History:
**  1.0.0	-	Initial version
**  1.0.12	-	Included support for the FT232H device.
**  1.0.14	-	Included Support for the X-Series of devices.
**
*/

namespace SquirterGUI
{
    using System;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.IO;
    using System.Windows.Forms;
    using SquirterGUI.Properties;

    /// <summary>
    /// Class wrapper for FTD2XX.DLL
    /// </summary>
    public class FTDI
    {
        private static FtStatus _lastStatus = FtStatus.FtOk;
        #region CONSTRUCTOR_DESTRUCTOR
        // constructor
        /// <summary>
        /// Constructor for the FTDI class.
        /// </summary>
        public FTDI()
        {
            // If FTD2XX.DLL is NOT loaded already, load it
            if (_hFtd2Xxdll == IntPtr.Zero)
            {
                // Load our FTD2XX.DLL library
                _hFtd2Xxdll = LoadLibrary(@"FTD2XX.DLL");
                if (_hFtd2Xxdll == IntPtr.Zero)
                {
                    // Failed to load our FTD2XX.DLL library from System32 or the application directory
                    // Try the same directory that this FTD2XX_NET DLL is in
                    //MessageBox.Show(string.Format("Attempting to load FTD2XX.DLL from:\n{0}", Path.GetDirectoryName(GetType().Assembly.Location)));
                    _hFtd2Xxdll = LoadLibrary(@Path.GetDirectoryName(GetType().Assembly.Location) + "\\FTD2XX.DLL");
                }
            }

            // If we have succesfully loaded the library, get the function pointers set up
            if (_hFtd2Xxdll != IntPtr.Zero)
            {
                // Set up our function pointers for use through our exported methods
                //_pFTCreateDeviceInfoList = GetProcAddress(_hFtd2Xxdll, "FT_CreateDeviceInfoList");
                //_pFTGetDeviceInfoDetail = GetProcAddress(_hFtd2Xxdll, "FT_GetDeviceInfoDetail");
                //_pFTOpen = GetProcAddress(_hFtd2Xxdll, "FT_Open");
                //_pFTOpenEx = GetProcAddress(_hFtd2Xxdll, "FT_OpenEx");
                //_pFTClose = GetProcAddress(_hFtd2Xxdll, "FT_Close");
                _pFTRead = GetProcAddress(_hFtd2Xxdll, "FT_Read");
                _pFTWrite = GetProcAddress(_hFtd2Xxdll, "FT_Write");
                //_pFTGetQueueStatus = GetProcAddress(_hFtd2Xxdll, "FT_GetQueueStatus");
                //_pFTGetModemStatus = GetProcAddress(_hFtd2Xxdll, "FT_GetModemStatus");
                //_pFTGetStatus = GetProcAddress(_hFtd2Xxdll, "FT_GetStatus");
                //_pFTSetBaudRate = GetProcAddress(_hFtd2Xxdll, "FT_SetBaudRate");
                //_pFTSetDataCharacteristics = GetProcAddress(_hFtd2Xxdll, "FT_SetDataCharacteristics");
                //_pFTSetFlowControl = GetProcAddress(_hFtd2Xxdll, "FT_SetFlowControl");
                //_pFTSetDtr = GetProcAddress(_hFtd2Xxdll, "FT_SetDtr");
                //_pFTClrDtr = GetProcAddress(_hFtd2Xxdll, "FT_ClrDtr");
                //_pFTSetRts = GetProcAddress(_hFtd2Xxdll, "FT_SetRts");
                //_pFTClrRts = GetProcAddress(_hFtd2Xxdll, "FT_ClrRts");
                //_pFTResetDevice = GetProcAddress(_hFtd2Xxdll, "FT_ResetDevice");
                //_pFTResetPort = GetProcAddress(_hFtd2Xxdll, "FT_ResetPort");
                //_pFTCyclePort = GetProcAddress(_hFtd2Xxdll, "FT_CyclePort");
                //_pFTRescan = GetProcAddress(_hFtd2Xxdll, "FT_Rescan");
                //_pFTReload = GetProcAddress(_hFtd2Xxdll, "FT_Reload");
                //_pFTPurge = GetProcAddress(_hFtd2Xxdll, "FT_Purge");
                //_pFTSetTimeouts = GetProcAddress(_hFtd2Xxdll, "FT_SetTimeouts");
                //_pFTSetBreakOn = GetProcAddress(_hFtd2Xxdll, "FT_SetBreakOn");
                //_pFTSetBreakOff = GetProcAddress(_hFtd2Xxdll, "FT_SetBreakOff");
                //_pFTGetDeviceInfo = GetProcAddress(_hFtd2Xxdll, "FT_GetDeviceInfo");
                //_pFTSetResetPipeRetryCount = GetProcAddress(_hFtd2Xxdll, "FT_SetResetPipeRetryCount");
                //_pFTStopInTask = GetProcAddress(_hFtd2Xxdll, "FT_StopInTask");
                //_pFTRestartInTask = GetProcAddress(_hFtd2Xxdll, "FT_RestartInTask");
                //_pFTGetDriverVersion = GetProcAddress(_hFtd2Xxdll, "FT_GetDriverVersion");
                //_pFTGetLibraryVersion = GetProcAddress(_hFtd2Xxdll, "FT_GetLibraryVersion");
                //_pFTSetDeadmanTimeout = GetProcAddress(_hFtd2Xxdll, "FT_SetDeadmanTimeout");
                //_pFTSetChars = GetProcAddress(_hFtd2Xxdll, "FT_SetChars");
                //_pFTSetEventNotification = GetProcAddress(_hFtd2Xxdll, "FT_SetEventNotification");
                //_pFTGetComPortNumber = GetProcAddress(_hFtd2Xxdll, "FT_GetComPortNumber");
                //_pFTSetLatencyTimer = GetProcAddress(_hFtd2Xxdll, "FT_SetLatencyTimer");
                //_pFTGetLatencyTimer = GetProcAddress(_hFtd2Xxdll, "FT_GetLatencyTimer");
                //_pFTSetBitMode = GetProcAddress(_hFtd2Xxdll, "FT_SetBitMode");
                //_pFTGetBitMode = GetProcAddress(_hFtd2Xxdll, "FT_GetBitMode");
                //_pFTSetUSBParameters = GetProcAddress(_hFtd2Xxdll, "FT_SetUSBParameters");
                //_pFTReadEe = GetProcAddress(_hFtd2Xxdll, "FT_ReadEE");
                //_pFTWriteEe = GetProcAddress(_hFtd2Xxdll, "FT_WriteEE");
                //_pFTEraseEe = GetProcAddress(_hFtd2Xxdll, "FT_EraseEE");
                //_pFTEeUaSize = GetProcAddress(_hFtd2Xxdll, "FT_EE_UASize");
                //_pFTEeUaRead = GetProcAddress(_hFtd2Xxdll, "FT_EE_UARead");
                //_pFTEeUaWrite = GetProcAddress(_hFtd2Xxdll, "FT_EE_UAWrite");
                //_pFTEeRead = GetProcAddress(_hFtd2Xxdll, "FT_EE_Read");
                //_pFTEeProgram = GetProcAddress(_hFtd2Xxdll, "FT_EE_Program");
                //_pFTEepromRead = GetProcAddress(_hFtd2Xxdll, "FT_EEPROM_Read");
                //_pFTEepromProgram = GetProcAddress(_hFtd2Xxdll, "FT_EEPROM_Program");
            }
            else
            {
                // Failed to load our DLL - alert the user
                MessageBox.Show(Resources.ftdi_fail_installed, Resources.error_title, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Destructor for the FTDI class.
        /// </summary>
        ~FTDI()
        {
            // FreeLibrary here - we should only do this if we are completely finished
            FreeLibrary(_hFtd2Xxdll);
            _hFtd2Xxdll = IntPtr.Zero;
        }
        #endregion CONSTRUCTOR_DESTRUCTOR

        #region LOAD_LIBRARIES
        /// <summary>
        /// Built-in Windows API functions to allow us to dynamically load our own DLL.
        /// Will allow us to use old versions of the DLL that do not have all of these functions available.
        /// </summary>
        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);
        #endregion LOAD_LIBRARIES

        #region DELEGATES
        // Definitions for FTD2XX functions
/*
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FtStatus FtCreateDeviceInfoList(ref UInt32 numdevs);
*/
/*
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FtStatus FtGetDeviceInfoDetail(UInt32 index, ref UInt32 flags, ref FtDevice chiptype, ref UInt32 id, ref UInt32 locid, byte[] serialnumber, byte[] description, ref IntPtr ftHandle);
*/

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtOpen(UInt32 index, ref IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtOpenEx(string devstring, UInt32 dwFlags, ref IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtOpenExLoc(UInt32 devloc, UInt32 dwFlags, ref IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtClose(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FtStatus FtRead(IntPtr ftHandle, byte[] lpBuffer, UInt32 dwBytesToRead, ref UInt32 lpdwBytesReturned);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FtStatus FtWrite(IntPtr ftHandle, byte[] lpBuffer, UInt32 dwBytesToWrite, ref UInt32 lpdwBytesWritten);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetQueueStatus(IntPtr ftHandle, ref UInt32 lpdwAmountInRxQueue);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetModemStatus(IntPtr ftHandle, ref UInt32 lpdwModemStatus);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetStatus(IntPtr ftHandle, ref UInt32 lpdwAmountInRxQueue, ref UInt32 lpdwAmountInTxQueue, ref UInt32 lpdwEventStatus);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetBaudRate(IntPtr ftHandle, UInt32 dwBaudRate);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetDataCharacteristics(IntPtr ftHandle, byte uWordLength, byte uStopBits, byte uParity);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetFlowControl(IntPtr ftHandle, UInt16 usFlowControl, byte uXon, byte uXoff);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetDtr(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtClrDtr(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetRts(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtClrRts(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtResetDevice(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtResetPort(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtCyclePort(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtRescan();
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtReload(UInt16 wVID, UInt16 wPID);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtPurge(IntPtr ftHandle, UInt32 dwMask);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetTimeouts(IntPtr ftHandle, UInt32 dwReadTimeout, UInt32 dwWriteTimeout);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetBreakOn(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetBreakOff(IntPtr ftHandle);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetDeviceInfo(IntPtr ftHandle, ref FtDevice pftType, ref UInt32 lpdwID, byte[] pcSerialNumber, byte[] pcDescription, IntPtr pvDummy);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetResetPipeRetryCount(IntPtr ftHandle, UInt32 dwCount);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtStopInTask(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtRestartInTask(IntPtr ftHandle);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetDriverVersion(IntPtr ftHandle, ref UInt32 lpdwDriverVersion);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetLibraryVersion(ref UInt32 lpdwLibraryVersion);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetDeadmanTimeout(IntPtr ftHandle, UInt32 dwDeadmanTimeout);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetChars(IntPtr ftHandle, byte uEventCh, byte uEventChEn, byte uErrorCh, byte uErrorChEn);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetEventNotification(IntPtr ftHandle, UInt32 dwEventMask, SafeHandle hEvent);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetComPortNumber(IntPtr ftHandle, ref Int32 dwComPortNumber);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetLatencyTimer(IntPtr ftHandle, byte ucLatency);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetLatencyTimer(IntPtr ftHandle, ref byte ucLatency);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetBitMode(IntPtr ftHandle, byte ucMask, byte ucMode);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtGetBitMode(IntPtr ftHandle, ref byte ucMode);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtSetUsbParameters(IntPtr ftHandle, UInt32 dwInTransferSize, UInt32 dwOutTransferSize);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtReadEe(IntPtr ftHandle, UInt32 dwWordOffset, ref UInt16 lpwValue);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtWriteEe(IntPtr ftHandle, UInt32 dwWordOffset, UInt16 wValue);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEraseEe(IntPtr ftHandle);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEeUaSize(IntPtr ftHandle, ref UInt32 dwSize);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEeUaRead(IntPtr ftHandle, byte[] pucData, Int32 dwDataLen, ref UInt32 lpdwDataRead);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEeUaWrite(IntPtr ftHandle, byte[] pucData, Int32 dwDataLen);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEeRead(IntPtr ftHandle, FtProgramData pData);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEeProgram(IntPtr ftHandle, FtProgramData pData);

        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEepromRead(IntPtr ftHandle, IntPtr eepromData, UInt32 eepromDataSize, byte[] manufacturer, byte[] manufacturerID, byte[] description, byte[] serialnumber);
        //[UnmanagedFunctionPointer(CallingConvention.StdCall)]
        //private delegate FtStatus FtEepromProgram(IntPtr ftHandle, IntPtr eepromData, UInt32 eepromDataSize, byte[] manufacturer, byte[] manufacturerID, byte[] description, byte[] serialnumber);
        #endregion

        #region CONSTANT_VALUES
        // Constants for FT_STATUS
        /// <summary>
        /// Status values for FTDI devices.
        /// </summary>
        public enum FtStatus
        {
            /// <summary>
            /// Status OK
            /// </summary>
            FtOk = 0,
            /// <summary>
            /// The device handle is invalid
            /// </summary>
            FtInvalidHandle,
            /// <summary>
            /// Device not found
            /// </summary>
            FtDeviceNotFound,
            /// <summary>
            /// Device is not open
            /// </summary>
            FtDeviceNotOpened,
            /// <summary>
            /// IO error
            /// </summary>
            FtIoError,
            /// <summary>
            /// Insufficient resources
            /// </summary>
            FtInsufficientResources,
            /// <summary>
            /// A parameter was invalid
            /// </summary>
            FtInvalidParameter,
            /// <summary>
            /// The requested baud rate is invalid
            /// </summary>
            FtInvalidBaudRate,
            /// <summary>
            /// Device not opened for erase
            /// </summary>
            FtDeviceNotOpenedForErase,
            /// <summary>
            /// Device not poened for write
            /// </summary>
            FtDeviceNotOpenedForWrite,
            /// <summary>
            /// Failed to write to device
            /// </summary>
            FtFailedToWriteDevice,
            /// <summary>
            /// Failed to read the device EEPROM
            /// </summary>
            FtEepromReadFailed,
            /// <summary>
            /// Failed to write the device EEPROM
            /// </summary>
            FtEepromWriteFailed,
            /// <summary>
            /// Failed to erase the device EEPROM
            /// </summary>
            FtEepromEraseFailed,
            /// <summary>
            /// An EEPROM is not fitted to the device
            /// </summary>
            FtEepromNotPresent,
            /// <summary>
            /// Device EEPROM is blank
            /// </summary>
            FtEepromNotProgrammed,
            /// <summary>
            /// Invalid arguments
            /// </summary>
            FtInvalidArgs,
            /// <summary>
            /// An other error has occurred
            /// </summary>
            FtOtherError
        };

        // Constants for other error states internal to this class library
        /// <summary>
        /// Error states not supported by FTD2XX DLL.
        /// </summary>
        //private enum FtError
        //{
        //    FtNoError = 0,
        //    FtIncorrectDevice,
        //    FtInvalidBitmode,
        //    FtBufferSize
        //};

        //// Flags for FT_OpenEx
        //private const UInt32 FtOpenBySerialNumber	= 0x00000001;
        //private const UInt32 FtOpenByDescription		= 0x00000002;
        //private const UInt32 FtOpenByLocation		= 0x00000004;

        // Word Lengths
        /// <summary>
        /// Permitted data bits for FTDI devices
        /// </summary>
        //public class FTDataBits
        //{
        //    /// <summary>
        //    /// 8 data bits
        //    /// </summary>
        //    public const byte FtBits8 = 0x08;
        //    /// <summary>
        //    /// 7 data bits
        //    /// </summary>
        //    public const byte FtBits7 = 0x07;
        //}

        // Stop Bits
        /// <summary>
        /// Permitted stop bits for FTDI devices
        /// </summary>
        //public class FT_STOP_BITS
        //{
        //    /// <summary>
        //    /// 1 stop bit
        //    /// </summary>
        //    public const byte FtStopBits1 = 0x00;
        //    /// <summary>
        //    /// 2 stop bits
        //    /// </summary>
        //    public const byte FtStopBits2 = 0x02;
        //}

        // Parity
        /// <summary>
        /// Permitted parity values for FTDI devices
        /// </summary>
        //public class FTParity
        //{
        //    /// <summary>
        //    /// No parity
        //    /// </summary>
        //    public const byte FtParityNone	= 0x00;
        //    /// <summary>
        //    /// Odd parity
        //    /// </summary>
        //    public const byte FtParityOdd		= 0x01;
        //    /// <summary>
        //    /// Even parity
        //    /// </summary>
        //    public const byte FtParityEven	= 0x02;
        //    /// <summary>
        //    /// Mark parity
        //    /// </summary>
        //    public const byte FtParityMark	= 0x03;
        //    /// <summary>
        //    /// Space parity
        //    /// </summary>
        //    public const byte FtParitySpace	= 0x04;
        //}

        // Flow Control
        /// <summary>
        /// Permitted flow control values for FTDI devices
        /// </summary>
        //public class FTFlowControl
        //{
        //    /// <summary>
        //    /// No flow control
        //    /// </summary>
        //    public const UInt16 FtFlowNone		= 0x0000;
        //    /// <summary>
        //    /// RTS/CTS flow control
        //    /// </summary>
        //    public const UInt16 FtFlowRtsCts		= 0x0100;
        //    /// <summary>
        //    /// DTR/DSR flow control
        //    /// </summary>
        //    public const UInt16 FtFlowDtrDsr		= 0x0200;
        //    /// <summary>
        //    /// Xon/Xoff flow control
        //    /// </summary>
        //    public const UInt16 FtFlowXonXoff	= 0x0400;
        //}

        // Purge Rx and Tx buffers
        /// <summary>
        /// Purge buffer constant definitions
        /// </summary>
        //public class FtPurgeClass
        //{
        //    /// <summary>
        //    /// Purge Rx buffer
        //    /// </summary>
        //    public const byte FtPurgeRx = 0x01;
        //    /// <summary>
        //    /// Purge Tx buffer
        //    /// </summary>
        //    public const byte FtPurgeTx = 0x02;
        //}

        // Modem Status bits
        /// <summary>
        /// Modem status bit definitions
        /// </summary>
        public class FtModemStatus
        {
            /// <summary>
            /// Clear To Send (CTS) modem status
            /// </summary>
            public const byte FtCts	= 0x10;
            /// <summary>
            /// Data Set Ready (DSR) modem status
            /// </summary>
            public const byte FtDsr	= 0x20;
            /// <summary>
            /// Ring Indicator (RI) modem status
            /// </summary>
            public const byte FtRi		= 0x40;
            /// <summary>
            /// Data Carrier Detect (DCD) modem status
            /// </summary>
            public const byte FtDcd	= 0x80;
        }

        // Line Status bits
        /// <summary>
        /// Line status bit definitions
        /// </summary>
        public class FtLineStatus
        {
            /// <summary>
            /// Overrun Error (OE) line status
            /// </summary>
            public const byte FtOe = 0x02;
            /// <summary>
            /// Parity Error (PE) line status
            /// </summary>
            public const byte FtPe = 0x04;
            /// <summary>
            /// Framing Error (FE) line status
            /// </summary>
            public const byte FtFe = 0x08;
            /// <summary>
            /// Break Interrupt (BI) line status
            /// </summary>
            public const byte FtBi = 0x10;
        }

        // Events
        /// <summary>
        /// FTDI device event types that can be monitored
        /// </summary>
        public class FtEvents
        {
            /// <summary>
            /// Event on receive character
            /// </summary>
            public const UInt32 FtEventRxchar			= 0x00000001;
            /// <summary>
            /// Event on modem status change
            /// </summary>
            public const UInt32 FtEventModemStatus	= 0x00000002;
            /// <summary>
            /// Event on line status change
            /// </summary>
            public const UInt32 FtEventLineStatus	= 0x00000004;
        }

        // Bit modes
        /// <summary>
        /// Permitted bit mode values for FTDI devices.  For use with SetBitMode
        /// </summary>
        public class FTBitModes
        {
            /// <summary>
            /// Reset bit mode
            /// </summary>
            public const byte FtBitModeReset			= 0x00;
            /// <summary>
            /// Asynchronous bit-bang mode
            /// </summary>
            public const byte FtBitModeAsyncBitbang	= 0x01;
            /// <summary>
            /// MPSSE bit mode - only available on FT2232, FT2232H, FT4232H and FT232H
            /// </summary>
            public const byte FtBitModeMpsse			= 0x02;
            /// <summary>
            /// Synchronous bit-bang mode
            /// </summary>
            public const byte FtBitModeSyncBitbang	= 0x04;
            /// <summary>
            /// MCU host bus emulation mode - only available on FT2232, FT2232H, FT4232H and FT232H
            /// </summary>
            public const byte FtBitModeMcuHost		= 0x08;
            /// <summary>
            /// Fast opto-isolated serial mode - only available on FT2232, FT2232H, FT4232H and FT232H
            /// </summary>
            public const byte FtBitModeFastSerial	= 0x10;
            /// <summary>
            /// CBUS bit-bang mode - only available on FT232R and FT232H
            /// </summary>
            public const byte FtBitModeCbusBitbang	= 0x20;
            /// <summary>
            /// Single channel synchronous 245 FIFO mode - only available on FT2232H channel A and FT232H
            /// </summary>
            public const byte FtBitModeSyncFifo		= 0x40;
        }

        // FT232R CBUS Options
        /// <summary>
        /// Available functions for the FT232R CBUS pins.  Controlled by FT232R EEPROM settings
        /// </summary>
        public class FTCbusOptions
        {
            /// <summary>
            /// FT232R CBUS EEPROM options - Tx Data Enable
            /// </summary>
            public const byte FtCbusTxden			= 0x00;
            /// <summary>
            /// FT232R CBUS EEPROM options - Power On
            /// </summary>
            public const byte FtCbusPwron			= 0x01;
            /// <summary>
            /// FT232R CBUS EEPROM options - Rx LED
            /// </summary>
            public const byte FtCbusRxled			= 0x02;
            /// <summary>
            /// FT232R CBUS EEPROM options - Tx LED
            /// </summary>
            public const byte FtCbusTxled			= 0x03;
            /// <summary>
            /// FT232R CBUS EEPROM options - Tx and Rx LED
            /// </summary>
            public const byte FtCbusTxrxled		= 0x04;
            /// <summary>
            /// FT232R CBUS EEPROM options - Sleep
            /// </summary>
            public const byte FtCbusSleep			= 0x05;
            /// <summary>
            /// FT232R CBUS EEPROM options - 48MHz clock
            /// </summary>
            public const byte FtCbusClk48			= 0x06;
            /// <summary>
            /// FT232R CBUS EEPROM options - 24MHz clock
            /// </summary>
            public const byte FtCbusClk24			= 0x07;
            /// <summary>
            /// FT232R CBUS EEPROM options - 12MHz clock
            /// </summary>
            public const byte FtCbusClk12			= 0x08;
            /// <summary>
            /// FT232R CBUS EEPROM options - 6MHz clock
            /// </summary>
            public const byte FtCbusClk6			= 0x09;
            /// <summary>
            /// FT232R CBUS EEPROM options - IO mode
            /// </summary>
            public const byte FtCbusIomode		= 0x0A;
            /// <summary>
            /// FT232R CBUS EEPROM options - Bit-bang write strobe
            /// </summary>
            public const byte FtCbusBitbangWr	= 0x0B;
            /// <summary>
            /// FT232R CBUS EEPROM options - Bit-bang read strobe
            /// </summary>
            public const byte FtCbusBitbangRd	= 0x0C;
        }

        // FT232H CBUS Options
        /// <summary>
        /// Available functions for the FT232H CBUS pins.  Controlled by FT232H EEPROM settings
        /// </summary>
        public class FT232HCbusOptions
        {
            /// <summary>
            /// FT232H CBUS EEPROM options - Tristate
            /// </summary>
            public const byte FtCbusTristate = 0x00;
            /// <summary>
            /// FT232H CBUS EEPROM options - Rx LED
            /// </summary>
            public const byte FtCbusRxled = 0x01;
            /// <summary>
            /// FT232H CBUS EEPROM options - Tx LED
            /// </summary>
            public const byte FtCbusTxled = 0x02;
            /// <summary>
            /// FT232H CBUS EEPROM options - Tx and Rx LED
            /// </summary>
            public const byte FtCbusTxrxled = 0x03;
            /// <summary>
            /// FT232H CBUS EEPROM options - Power Enable
            /// </summary>
            public const byte FtCbusPwren = 0x04;
            /// <summary>
            /// FT232H CBUS EEPROM options - Sleep
            /// </summary>
            public const byte FtCbusSleep = 0x05;
            /// <summary>
            /// FT232H CBUS EEPROM options - Drive pin to logic 0
            /// </summary>
            public const byte FtCbusDrive0 = 0x06;
            /// <summary>
            /// FT232H CBUS EEPROM options - Drive pin to logic 1
            /// </summary>
            public const byte FtCbusDrive1 = 0x07;
            /// <summary>
            /// FT232H CBUS EEPROM options - IO Mode
            /// </summary>
            public const byte FtCbusIomode = 0x08;
            /// <summary>
            /// FT232H CBUS EEPROM options - Tx Data Enable
            /// </summary>
            public const byte FtCbusTxden = 0x09;
            /// <summary>
            /// FT232H CBUS EEPROM options - 30MHz clock
            /// </summary>
            public const byte FtCbusClk30 = 0x0A;
            /// <summary>
            /// FT232H CBUS EEPROM options - 15MHz clock
            /// </summary>
            public const byte FtCbusClk15 = 0x0B;/// <summary>
            /// FT232H CBUS EEPROM options - 7.5MHz clock
            /// </summary>
            public const byte FtCbusClk75 = 0x0C;
        }

        /// <summary>
        /// Available functions for the X-Series CBUS pins.  Controlled by X-Series EEPROM settings
        /// </summary>
        public class FtXseriesCbusOptions
        {
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Tristate
            /// </summary>
            public const byte FtCbusTristate = 0x00;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - RxLED#
            /// </summary>
            public const byte FtCbusRxled = 0x01;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - TxLED#
            /// </summary>
            public const byte FtCbusTxled = 0x02;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - TxRxLED
            /// </summary>
            public const byte FtCbusTxrxled = 0x03;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - PwrEn#
            /// </summary>
            public const byte FtCbusPwren = 0x04;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Sleep#
            /// </summary>
            public const byte FtCbusSleep = 0x05;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Drive_0
            /// </summary>
            public const byte FtCbusDrive0 = 0x06;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Drive_1
            /// </summary>
            public const byte FtCbusDrive1 = 0x07;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - GPIO
            /// </summary>
            public const byte FtCbusGpio = 0x08;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - TxdEn
            /// </summary>
            public const byte FtCbusTxden = 0x09;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Clk24MHz
            /// </summary>
            public const byte FtCbusClk24MHz = 0x0A;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Clk12MHz
            /// </summary>
            public const byte FtCbusClk12MHz = 0x0B;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Clk6MHz
            /// </summary>
            public const byte FtCbusClk6MHz = 0x0C;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - BCD_Charger
            /// </summary>
            public const byte FtCbusBcdCharger = 0x0D;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - BCD_Charger#
            /// </summary>
            public const byte FtCbusBcdChargerN = 0x0E;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - I2C_TXE#
            /// </summary>
            public const byte FtCbusI2CTxe = 0x0F;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - I2C_RXF#
            /// </summary>
            public const byte FtCbusI2CRxf = 0x10;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - VBUS_Sense
            /// </summary>
            public const byte FtCbusVbusSense = 0x11;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - BitBang_WR#
            /// </summary>
            public const byte FtCbusBitBangWr = 0x12;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - BitBang_RD#
            /// </summary>
            public const byte FtCbusBitBangRd = 0x13;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Time_Stampe
            /// </summary>
            public const byte FtCbusTimeStamp = 0x14;
            /// <summary>
            /// FT X-Series CBUS EEPROM options - Keep_Awake#
            /// </summary>
            public const byte FtCbusKeepAwake = 0x15;
        }

        // Flag values for FT_GetDeviceInfoDetail and FT_GetDeviceInfo
        /// <summary>
        /// Flags that provide information on the FTDI device state
        /// </summary>
        public class FtFlags
        {
            /// <summary>
            /// Indicates that the device is open
            /// </summary>
            public const UInt32 FtFlagsOpened		= 0x00000001;
            /// <summary>
            /// Indicates that the device is enumerated as a hi-speed USB device
            /// </summary>
            public const UInt32 FtFlagsHispeed	= 0x00000002;
        }

        // Valid drive current values for FT2232H, FT4232H and FT232H devices
        /// <summary>
        /// Valid values for drive current options on FT2232H, FT4232H and FT232H devices.
        /// </summary>
        public class FTDriveCurrent
        {
            /// <summary>
            /// 4mA drive current
            /// </summary>
            public const byte FtDriveCurrent4Ma	= 4;
            /// <summary>
            /// 8mA drive current
            /// </summary>
            public const byte FtDriveCurrent8Ma	= 8;
            /// <summary>
            /// 12mA drive current
            /// </summary>
            public const byte FtDriveCurrent12Ma	= 12;
            /// <summary>
            /// 16mA drive current
            /// </summary>
            public const byte FtDriveCurrent16Ma	= 16;
        }

        // Device type identifiers for FT_GetDeviceInfoDetail and FT_GetDeviceInfo
        /// <summary>
        /// List of FTDI device types
        /// </summary>
        public enum FtDevice
        {
            /// <summary>
            /// FT232B or FT245B device
            /// </summary>
            FtDeviceBm = 0,
            /// <summary>
            /// FT8U232AM or FT8U245AM device
            /// </summary>
            FtDeviceAm,
            /// <summary>
            /// FT8U100AX device
            /// </summary>
            FtDevice100Ax,
            /// <summary>
            /// Unknown device
            /// </summary>
            FtDeviceUnknown,
            /// <summary>
            /// FT2232 device
            /// </summary>
            FtDevice2232,
            /// <summary>
            /// FT232R or FT245R device
            /// </summary>
            FtDevice232R,
            /// <summary>
            /// FT2232H device
            /// </summary>
            FtDevice2232H,
            /// <summary>
            /// FT4232H device
            /// </summary>
            FtDevice4232H,
            /// <summary>
            /// FT232H device
            /// </summary>
            FtDevice232H,
            /// <summary>
            /// FT232X device
            /// </summary>
            FtDeviceXSeries
        };
        #endregion CONSTANT_VALUES

        //#region DEFAULT_VALUES
        //private const UInt32 FtDefaultBaudRate			= 9600;
        //private const UInt32 FtDefaultDeadmanTimeout		= 5000;
        //private const Int32 FtComPortNotAssigned		= -1;
        //private const UInt32 FtDefaultInTransferSize	= 0x1000;
        //private const UInt32 FtDefaultOutTransferSize	= 0x1000;
        //private const byte FtDefaultLatency				= 16;
        //private const UInt32 FtDefaultDeviceID			= 0x04036001;
        //#endregion DEFAULT_VALUES

        #region VARIABLES
        // Create private variables for the device within the class
        private IntPtr _ftHandle = IntPtr.Zero;
        #endregion

        #region TYPEDEFS
        /// <summary>
        /// Type that holds device information for GetDeviceInformation method.
        /// Used with FT_GetDeviceInfo and FT_GetDeviceInfoDetail in FTD2XX.DLL
        /// </summary>
        public class FtDeviceInfoNode
        {
            /// <summary>
            /// Indicates device state.  Can be any combination of the following: FT_FLAGS_OPENED, FT_FLAGS_HISPEED
            /// </summary>
            public UInt32 Flags;
            /// <summary>
            /// Indicates the device type.  Can be one of the following: FT_DEVICE_232R, FT_DEVICE_2232C, FT_DEVICE_BM, FT_DEVICE_AM, FT_DEVICE_100AX or FT_DEVICE_UNKNOWN
            /// </summary>
            public FtDevice Type;
            /// <summary>
            /// The Vendor ID and Product ID of the device
            /// </summary>
            public UInt32 ID;
            /// <summary>
            /// The physical location identifier of the device
            /// </summary>
            public UInt32 LocId;
            /// <summary>
            /// The device serial number
            /// </summary>
            public string SerialNumber;
            /// <summary>
            /// The device description
            /// </summary>
            public string Description;
            /// <summary>
            /// The device handle.  This value is not used externally and is provided for information only.
            /// If the device is not open, this value is 0.
            /// </summary>
            public IntPtr FtHandle;
        }
        #endregion

        //#region EEPROM_STRUCTURES
        //// Internal structure for reading and writing EEPROM contents
        //// NOTE:  NEED Pack=1 for byte alignment!  Without this, data is garbage
        //[StructLayout(LayoutKind.Sequential, Pack = 4)]
        //private class FtProgramData
        //{
        //    // ReSharper disable NotAccessedField.Local
        //    public UInt32 Signature1;
        //    public UInt32 Signature2;
        //    public UInt32 Version;
        //    // ReSharper restore NotAccessedField.Local
        //    public UInt16 VendorID;
        //    public UInt16 ProductID;

        //    public IntPtr Manufacturer;
        //    public IntPtr ManufacturerID;
        //    public IntPtr Description;
        //    public IntPtr SerialNumber;

        //    public UInt16 MaxPower;
        //    public UInt16 SelfPowered;
        //    public UInt16 RemoteWakeup;
        //    // FT232B extensions
        //    public byte PullDownEnable;
        //    public byte SerNumEnable;
        //    public byte USBVersionEnable;
        //    public UInt16 USBVersion;
        //    // FT2232D extensions
        //    public byte PullDownEnable5;
        //    public byte SerNumEnable5;
        //    public byte USBVersionEnable5;
        //    public UInt16 USBVersion5;
        //    public byte AIsHighCurrent;
        //    public byte BIsHighCurrent;
        //    public byte IFAIsFifo;
        //    public byte IFAIsFifoTar;
        //    public byte IFAIsFastSer;
        //    public byte AIsVCP;
        //    public byte IFBIsFifo;
        //    public byte IFBIsFifoTar;
        //    public byte IFBIsFastSer;
        //    public byte BIsVCP;
        //    // FT232R extensions
        //    public byte UseExtOsc;
        //    public byte HighDriveIOs;
        //    public byte EndpointSize;
        //    public byte PullDownEnableR;
        //    public byte SerNumEnableR;
        //    public byte InvertTXD;			// non-zero if invert TXD
        //    public byte InvertRXD;			// non-zero if invert RXD
        //    public byte InvertRTS;			// non-zero if invert RTS
        //    public byte InvertCTS;			// non-zero if invert CTS
        //    public byte InvertDTR;			// non-zero if invert DTR
        //    public byte InvertDSR;			// non-zero if invert DSR
        //    public byte InvertDCD;			// non-zero if invert DCD
        //    public byte InvertRI;			// non-zero if invert RI
        //    public byte Cbus0;				// Cbus Mux control - Ignored for FT245R
        //    public byte Cbus1;				// Cbus Mux control - Ignored for FT245R
        //    public byte Cbus2;				// Cbus Mux control - Ignored for FT245R
        //    public byte Cbus3;				// Cbus Mux control - Ignored for FT245R
        //    public byte Cbus4;				// Cbus Mux control - Ignored for FT245R
        //    public byte RIsD2XX;			// Default to loading VCP
        //    // FT2232H extensions
        //    public byte PullDownEnable7;
        //    public byte SerNumEnable7;
        //    public byte ALSlowSlew;			// non-zero if AL pins have slow slew
        //    public byte ALSchmittInput;		// non-zero if AL pins are Schmitt input
        //    public byte ALDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte AHSlowSlew;			// non-zero if AH pins have slow slew
        //    public byte AHSchmittInput;		// non-zero if AH pins are Schmitt input
        //    public byte AHDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte BLSlowSlew;			// non-zero if BL pins have slow slew
        //    public byte BLSchmittInput;		// non-zero if BL pins are Schmitt input
        //    public byte BLDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte BHSlowSlew;			// non-zero if BH pins have slow slew
        //    public byte BHSchmittInput;		// non-zero if BH pins are Schmitt input
        //    public byte BHDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte IFAIsFifo7;			// non-zero if interface is 245 FIFO
        //    public byte IFAIsFifoTar7;		// non-zero if interface is 245 FIFO CPU target
        //    public byte IFAIsFastSer7;		// non-zero if interface is Fast serial
        //    public byte AIsVCP7;			// non-zero if interface is to use VCP drivers
        //    public byte IFBIsFifo7;			// non-zero if interface is 245 FIFO
        //    public byte IFBIsFifoTar7;		// non-zero if interface is 245 FIFO CPU target
        //    public byte IFBIsFastSer7;		// non-zero if interface is Fast serial
        //    public byte BIsVCP7;			// non-zero if interface is to use VCP drivers
        //    public byte PowerSaveEnable;    // non-zero if using BCBUS7 to save power for self-powered designs
        //    // FT4232H extensions
        //    public byte PullDownEnable8;
        //    public byte SerNumEnable8;
        //    public byte ASlowSlew;			// non-zero if AL pins have slow slew
        //    public byte ASchmittInput;		// non-zero if AL pins are Schmitt input
        //    public byte ADriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte BSlowSlew;			// non-zero if AH pins have slow slew
        //    public byte BSchmittInput;		// non-zero if AH pins are Schmitt input
        //    public byte BDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte CSlowSlew;			// non-zero if BL pins have slow slew
        //    public byte CSchmittInput;		// non-zero if BL pins are Schmitt input
        //    public byte CDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte DSlowSlew;			// non-zero if BH pins have slow slew
        //    public byte DSchmittInput;		// non-zero if BH pins are Schmitt input
        //    public byte DDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte ARIIsTXDEN;
        //    public byte BRIIsTXDEN;
        //    public byte CRIIsTXDEN;
        //    public byte DRIIsTXDEN;
        //    public byte AIsVCP8;			// non-zero if interface is to use VCP drivers
        //    public byte BIsVCP8;			// non-zero if interface is to use VCP drivers
        //    public byte CIsVCP8;			// non-zero if interface is to use VCP drivers
        //    public byte DIsVCP8;			// non-zero if interface is to use VCP drivers
        //    // FT232H extensions
        //    public byte PullDownEnableH;	// non-zero if pull down enabled
        //    public byte SerNumEnableH;		// non-zero if serial number to be used
        //    public byte ACSlowSlewH;		// non-zero if AC pins have slow slew
        //    public byte ACSchmittInputH;	// non-zero if AC pins are Schmitt input
        //    public byte ACDriveCurrentH;	// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte ADSlowSlewH;		// non-zero if AD pins have slow slew
        //    public byte ADSchmittInputH;	// non-zero if AD pins are Schmitt input
        //    public byte ADDriveCurrentH;	// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte Cbus0H;				// Cbus Mux control
        //    public byte Cbus1H;				// Cbus Mux control
        //    public byte Cbus2H;				// Cbus Mux control
        //    public byte Cbus3H;				// Cbus Mux control
        //    public byte Cbus4H;				// Cbus Mux control
        //    public byte Cbus5H;				// Cbus Mux control
        //    public byte Cbus6H;				// Cbus Mux control
        //    public byte Cbus7H;				// Cbus Mux control
        //    public byte Cbus8H;				// Cbus Mux control
        //    public byte Cbus9H;				// Cbus Mux control
        //    public byte IsFifoH;			// non-zero if interface is 245 FIFO
        //    public byte IsFifoTarH;			// non-zero if interface is 245 FIFO CPU target
        //    public byte IsFastSerH;			// non-zero if interface is Fast serial
        //    public byte IsFT1248H;			// non-zero if interface is FT1248
        //    public byte FT1248CpolH;		// FT1248 clock polarity
        //    public byte FT1248LsbH;			// FT1248 data is LSB (1) or MSB (0)
        //    public byte FT1248FlowControlH;	// FT1248 flow control enable
        //    public byte IsVCPH;				// non-zero if interface is to use VCP drivers
        //    public byte PowerSaveEnableH;	// non-zero if using ACBUS7 to save power for self-powered designs
        //}

        //[StructLayout(LayoutKind.Sequential, Pack = 4)]
        //struct FtEepromHeader
        //{
        //    public UInt32 deviceType;		// FTxxxx device type to be programmed
        //    // Device descriptor options
        //    public UInt16 VendorId;				// 0x0403
        //    public UInt16 ProductId;				// 0x6001
        //    public byte SerNumEnable;			// non-zero if serial number to be used
        //    // Config descriptor options
        //    public UInt16 MaxPower;				// 0 < MaxPower <= 500
        //    public byte SelfPowered;			// 0 = bus powered, 1 = self powered
        //    public byte RemoteWakeup;			// 0 = not capable, 1 = capable
        //    // Hardware options
        //    public byte PullDownEnable;		// non-zero if pull down in suspend enabled
        //}

        //[StructLayout(LayoutKind.Sequential, Pack = 4)]
        //struct FtXseriesData
        //{
        //    public FtEepromHeader common;

        //    public byte ACSlowSlew;			// non-zero if AC bus pins have slow slew
        //    public byte ACSchmittInput;		// non-zero if AC bus pins are Schmitt input
        //    public byte ACDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    public byte ADSlowSlew;			// non-zero if AD bus pins have slow slew
        //    public byte ADSchmittInput;		// non-zero if AD bus pins are Schmitt input
        //    public byte ADDriveCurrent;		// valid values are 4mA, 8mA, 12mA, 16mA
        //    // CBUS options
        //    public byte Cbus0;				// Cbus Mux control
        //    public byte Cbus1;				// Cbus Mux control
        //    public byte Cbus2;				// Cbus Mux control
        //    public byte Cbus3;				// Cbus Mux control
        //    public byte Cbus4;				// Cbus Mux control
        //    public byte Cbus5;				// Cbus Mux control
        //    public byte Cbus6;				// Cbus Mux control
        //    // UART signal options
        //    public byte InvertTXD;			// non-zero if invert TXD
        //    public byte InvertRXD;			// non-zero if invert RXD
        //    public byte InvertRTS;			// non-zero if invert RTS
        //    public byte InvertCTS;			// non-zero if invert CTS
        //    public byte InvertDTR;			// non-zero if invert DTR
        //    public byte InvertDSR;			// non-zero if invert DSR
        //    public byte InvertDCD;			// non-zero if invert DCD
        //    public byte InvertRI;				// non-zero if invert RI
        //    // Battery Charge Detect options
        //    public byte BCDEnable;			// Enable Battery Charger Detection
        //    public byte BCDForceCbusPWREN;	// asserts the power enable signal on CBUS when charging port detected
        //    public byte BCDDisableSleep;		// forces the device never to go into sleep mode
        //    // I2C options
        //    public UInt16 I2CSlaveAddress;		// I2C slave device address
        //    public UInt32 I2CDeviceId;			// I2C device ID
        //    public byte I2CDisableSchmitt;	// Disable I2C Schmitt trigger
        //    // FT1248 options
        //    public byte FT1248Cpol;			// FT1248 clock polarity - clock idle high (1) or clock idle low (0)
        //    public byte FT1248Lsb;			// FT1248 data is LSB (1) or MSB (0)
        //    public byte FT1248FlowControl;	// FT1248 flow control enable
        //    // Hardware options
        //    public byte RS485EchoSuppress;	// 
        //    public byte PowerSaveEnable;		// 
        //    // Driver option
        //    public byte DriverType;			// 
        //}

        //// Base class for EEPROM structures - these elements are common to all devices
        ///// <summary>
        ///// Common EEPROM elements for all devices.  Inherited to specific device type EEPROMs.
        ///// </summary>
        //public class FtEepromData
        //{
        //    //private const UInt32 Signature1     = 0x00000000;
        //    //private const UInt32 Signature2     = 0xFFFFFFFF;
        //    //private const UInt32 Version        = 0x00000002;
        //    /// <summary>
        //    /// Vendor ID as supplied by the USB Implementers Forum
        //    /// </summary>
        //    public UInt16 VendorID = 0x0403;
        //    /// <summary>
        //    /// Product ID
        //    /// </summary>
        //    public UInt16 ProductID = 0x6001;
        //    /// <summary>
        //    /// Manufacturer name string
        //    /// </summary>
        //    public string Manufacturer = "FTDI";
        //    /// <summary>
        //    /// Manufacturer name abbreviation to be used as a prefix for automatically generated serial numbers
        //    /// </summary>
        //    public string ManufacturerID = "FT";
        //    /// <summary>
        //    /// Device description string
        //    /// </summary>
        //    public string Description = "USB-Serial Converter";
        //    /// <summary>
        //    /// Device serial number string
        //    /// </summary>
        //    public string SerialNumber = "";
        //    /// <summary>
        //    /// Maximum power the device needs
        //    /// </summary>
        //    public UInt16 MaxPower = 0x0090;
        //    //private bool PnP                    = true;
        //    /// <summary>
        //    /// Indicates if the device has its own power supply (self-powered) or gets power from the USB port (bus-powered)
        //    /// </summary>
        //    public bool SelfPowered;
        //    /// <summary>
        //    /// Determines if the device can wake the host PC from suspend by toggling the RI line
        //    /// </summary>
        //    public bool RemoteWakeup;
        //}

        //// EEPROM class for FT232B and FT245B
        ///// <summary>
        ///// EEPROM structure specific to FT232B and FT245B devices.
        ///// Inherits from FT_EEPROM_DATA.
        ///// </summary>
        //public class FT232B_EEPROM_STRUCTURE : FtEepromData
        //{
        //    //private bool Rev4                   = true;
        //    //private bool IsoIn                  = false;
        //    //private bool IsoOut                 = false;
        //    /// <summary>
        //    /// Determines if IOs are pulled down when the device is in suspend
        //    /// </summary>
        //    public bool PullDownEnable;
        //    /// <summary>
        //    /// Determines if the serial number is enabled
        //    /// </summary>
        //    public bool SerNumEnable = true;
        //    /// <summary>
        //    /// Determines if the USB version number is enabled
        //    /// </summary>
        //    public bool USBVersionEnable = true;
        //    /// <summary>
        //    /// The USB version number.  Should be either 0x0110 (USB 1.1) or 0x0200 (USB 2.0)
        //    /// </summary>
        //    public UInt16 USBVersion = 0x0200;
        //}

        //// EEPROM class for FT2232C, FT2232L and FT2232D
        ///// <summary>
        ///// EEPROM structure specific to FT2232 devices.
        ///// Inherits from FT_EEPROM_DATA.
        ///// </summary>
        //public class FT2232_EEPROM_STRUCTURE : FtEepromData
        //{
        //    //private bool Rev5                   = true;
        //    //private bool IsoInA                 = false;
        //    //private bool IsoInB                 = false;
        //    //private bool IsoOutA                = false;
        //    //private bool IsoOutB                = false;
        //    /// <summary>
        //    /// Determines if IOs are pulled down when the device is in suspend
        //    /// </summary>
        //    public bool PullDownEnable;
        //    /// <summary>
        //    /// Determines if the serial number is enabled
        //    /// </summary>
        //    public bool SerNumEnable = true;
        //    /// <summary>
        //    /// Determines if the USB version number is enabled
        //    /// </summary>
        //    public bool USBVersionEnable = true;
        //    /// <summary>
        //    /// The USB version number.  Should be either 0x0110 (USB 1.1) or 0x0200 (USB 2.0)
        //    /// </summary>
        //    public UInt16 USBVersion = 0x0200;
        //    /// <summary>
        //    /// Enables high current IOs on channel A
        //    /// </summary>
        //    public bool AIsHighCurrent;
        //    /// <summary>
        //    /// Enables high current IOs on channel B
        //    /// </summary>
        //    public bool BIsHighCurrent;
        //    /// <summary>
        //    /// Determines if channel A is in FIFO mode
        //    /// </summary>
        //    public bool IFAIsFifo;
        //    /// <summary>
        //    /// Determines if channel A is in FIFO target mode
        //    /// </summary>
        //    public bool IFAIsFifoTar;
        //    /// <summary>
        //    /// Determines if channel A is in fast serial mode
        //    /// </summary>
        //    public bool IFAIsFastSer;
        //    /// <summary>
        //    /// Determines if channel A loads the VCP driver
        //    /// </summary>
        //    public bool AisVCP = true;
        //    /// <summary>
        //    /// Determines if channel B is in FIFO mode
        //    /// </summary>
        //    public bool IFBIsFifo;
        //    /// <summary>
        //    /// Determines if channel B is in FIFO target mode
        //    /// </summary>
        //    public bool IFBIsFifoTar;
        //    /// <summary>
        //    /// Determines if channel B is in fast serial mode
        //    /// </summary>
        //    public bool IFBIsFastSer;
        //    /// <summary>
        //    /// Determines if channel B loads the VCP driver
        //    /// </summary>
        //    public bool BisVCP = true;
        //}

        //// EEPROM class for FT232R and FT245R
        ///// <summary>
        ///// EEPROM structure specific to FT232R and FT245R devices.
        ///// Inherits from FT_EEPROM_DATA.
        ///// </summary>
        //public class FT232R_EEPROM_STRUCTURE : FtEepromData
        //{
        //    /// <summary>
        //    /// Disables the FT232R internal clock source.  
        //    /// If the device has external oscillator enabled it must have an external oscillator fitted to function
        //    /// </summary>
        //    public bool UseExtOsc;
        //    /// <summary>
        //    /// Enables high current IOs
        //    /// </summary>
        //    public bool HighDriveIOs;
        //    /// <summary>
        //    /// Sets the endpoint size.  This should always be set to 64
        //    /// </summary>
        //    public byte EndpointSize = 64;
        //    /// <summary>
        //    /// Determines if IOs are pulled down when the device is in suspend
        //    /// </summary>
        //    public bool PullDownEnable;
        //    /// <summary>
        //    /// Determines if the serial number is enabled
        //    /// </summary>
        //    public bool SerNumEnable = true;
        //    /// <summary>
        //    /// Inverts the sense of the TXD line
        //    /// </summary>
        //    public bool InvertTXD;
        //    /// <summary>
        //    /// Inverts the sense of the RXD line
        //    /// </summary>
        //    public bool InvertRXD;
        //    /// <summary>
        //    /// Inverts the sense of the RTS line
        //    /// </summary>
        //    public bool InvertRTS;
        //    /// <summary>
        //    /// Inverts the sense of the CTS line
        //    /// </summary>
        //    public bool InvertCTS;
        //    /// <summary>
        //    /// Inverts the sense of the DTR line
        //    /// </summary>
        //    public bool InvertDTR;
        //    /// <summary>
        //    /// Inverts the sense of the DSR line
        //    /// </summary>
        //    public bool InvertDSR;
        //    /// <summary>
        //    /// Inverts the sense of the DCD line
        //    /// </summary>
        //    public bool InvertDCD;
        //    /// <summary>
        //    /// Inverts the sense of the RI line
        //    /// </summary>
        //    public bool InvertRI;
        //    /// <summary>
        //    /// Sets the function of the CBUS0 pin for FT232R devices.
        //    /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED, 
        //    /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12, 
        //    /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
        //    /// </summary>
        //    public byte Cbus0 = FT_CBUS_OPTIONS.FtCbusSleep;
        //    /// <summary>
        //    /// Sets the function of the CBUS1 pin for FT232R devices.
        //    /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED, 
        //    /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12, 
        //    /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
        //    /// </summary>
        //    public byte Cbus1 = FT_CBUS_OPTIONS.FtCbusSleep;
        //    /// <summary>
        //    /// Sets the function of the CBUS2 pin for FT232R devices.
        //    /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED, 
        //    /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12, 
        //    /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
        //    /// </summary>
        //    public byte Cbus2 = FT_CBUS_OPTIONS.FtCbusSleep;
        //    /// <summary>
        //    /// Sets the function of the CBUS3 pin for FT232R devices.
        //    /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED, 
        //    /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12, 
        //    /// FT_CBUS_CLK6, FT_CBUS_IOMODE, FT_CBUS_BITBANG_WR, FT_CBUS_BITBANG_RD
        //    /// </summary>
        //    public byte Cbus3 = FT_CBUS_OPTIONS.FtCbusSleep;
        //    /// <summary>
        //    /// Sets the function of the CBUS4 pin for FT232R devices.
        //    /// Valid values are FT_CBUS_TXDEN, FT_CBUS_PWRON , FT_CBUS_RXLED, FT_CBUS_TXLED, 
        //    /// FT_CBUS_TXRXLED, FT_CBUS_SLEEP, FT_CBUS_CLK48, FT_CBUS_CLK24, FT_CBUS_CLK12, 
        //    /// FT_CBUS_CLK6
        //    /// </summary>
        //    public byte Cbus4 = FT_CBUS_OPTIONS.FtCbusSleep;
        //    /// <summary>
        //    /// Determines if the VCP driver is loaded
        //    /// </summary>
        //    public bool RisD2XX;
        //}

        //// EEPROM class for FT2232H
        ///// <summary>
        ///// EEPROM structure specific to FT2232H devices.
        ///// Inherits from FT_EEPROM_DATA.
        ///// </summary>
        //public class FT2232H_EEPROM_STRUCTURE : FtEepromData
        //{
        //    /// <summary>
        //    /// Determines if IOs are pulled down when the device is in suspend
        //    /// </summary>
        //    public bool PullDownEnable;
        //    /// <summary>
        //    /// Determines if the serial number is enabled
        //    /// </summary>
        //    public bool SerNumEnable = true;
        //    /// <summary>
        //    /// Determines if AL pins have a slow slew rate
        //    /// </summary>
        //    public bool ALSlowSlew;
        //    /// <summary>
        //    /// Determines if the AL pins have a Schmitt input
        //    /// </summary>
        //    public bool ALSchmittInput;
        //    /// <summary>
        //    /// Determines the AL pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte ALDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if AH pins have a slow slew rate
        //    /// </summary>
        //    public bool AHSlowSlew;
        //    /// <summary>
        //    /// Determines if the AH pins have a Schmitt input
        //    /// </summary>
        //    public bool AHSchmittInput;
        //    /// <summary>
        //    /// Determines the AH pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte AHDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if BL pins have a slow slew rate
        //    /// </summary>
        //    public bool BLSlowSlew;
        //    /// <summary>
        //    /// Determines if the BL pins have a Schmitt input
        //    /// </summary>
        //    public bool BLSchmittInput;
        //    /// <summary>
        //    /// Determines the BL pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte BLDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if BH pins have a slow slew rate
        //    /// </summary>
        //    public bool BHSlowSlew;
        //    /// <summary>
        //    /// Determines if the BH pins have a Schmitt input
        //    /// </summary>
        //    public bool BHSchmittInput;
        //    /// <summary>
        //    /// Determines the BH pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte BHDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if channel A is in FIFO mode
        //    /// </summary>
        //    public bool IFAIsFifo;
        //    /// <summary>
        //    /// Determines if channel A is in FIFO target mode
        //    /// </summary>
        //    public bool IFAIsFifoTar;
        //    /// <summary>
        //    /// Determines if channel A is in fast serial mode
        //    /// </summary>
        //    public bool IFAIsFastSer;
        //    /// <summary>
        //    /// Determines if channel A loads the VCP driver
        //    /// </summary>
        //    public bool AisVCP = true;
        //    /// <summary>
        //    /// Determines if channel B is in FIFO mode
        //    /// </summary>
        //    public bool IFBIsFifo;
        //    /// <summary>
        //    /// Determines if channel B is in FIFO target mode
        //    /// </summary>
        //    public bool IFBIsFifoTar;
        //    /// <summary>
        //    /// Determines if channel B is in fast serial mode
        //    /// </summary>
        //    public bool IFBIsFastSer;
        //    /// <summary>
        //    /// Determines if channel B loads the VCP driver
        //    /// </summary>
        //    public bool BisVCP = true;
        //    /// <summary>
        //    /// For self-powered designs, keeps the FT2232H in low power state until BCBUS7 is high
        //    /// </summary>
        //    public bool PowerSaveEnable;
        //}

        //// EEPROM class for FT4232H
        ///// <summary>
        ///// EEPROM structure specific to FT4232H devices.
        ///// Inherits from FT_EEPROM_DATA.
        ///// </summary>
        //public abstract class FT4232HEepromStructure : FtEepromData
        //{
        //    /// <summary>
        //    /// Determines if IOs are pulled down when the device is in suspend
        //    /// </summary>
        //    public bool PullDownEnable;
        //    /// <summary>
        //    /// Determines if the serial number is enabled
        //    /// </summary>
        //    public bool SerNumEnable = true;
        //    /// <summary>
        //    /// Determines if A pins have a slow slew rate
        //    /// </summary>
        //    public bool ASlowSlew;
        //    /// <summary>
        //    /// Determines if the A pins have a Schmitt input
        //    /// </summary>
        //    public bool ASchmittInput;
        //    /// <summary>
        //    /// Determines the A pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte ADriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if B pins have a slow slew rate
        //    /// </summary>
        //    public bool BSlowSlew;
        //    /// <summary>
        //    /// Determines if the B pins have a Schmitt input
        //    /// </summary>
        //    public bool BSchmittInput;
        //    /// <summary>
        //    /// Determines the B pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte BDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if C pins have a slow slew rate
        //    /// </summary>
        //    public bool CSlowSlew;
        //    /// <summary>
        //    /// Determines if the C pins have a Schmitt input
        //    /// </summary>
        //    public bool CSchmittInput;
        //    /// <summary>
        //    /// Determines the C pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte CDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if D pins have a slow slew rate
        //    /// </summary>
        //    public bool DSlowSlew;
        //    /// <summary>
        //    /// Determines if the D pins have a Schmitt input
        //    /// </summary>
        //    public bool DSchmittInput;
        //    /// <summary>
        //    /// Determines the D pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte DDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// RI of port A acts as RS485 transmit enable (TXDEN)
        //    /// </summary>
        //    public bool AriisTXDEN;
        //    /// <summary>
        //    /// RI of port B acts as RS485 transmit enable (TXDEN)
        //    /// </summary>
        //    public bool BriisTXDEN;
        //    /// <summary>
        //    /// RI of port C acts as RS485 transmit enable (TXDEN)
        //    /// </summary>
        //    public bool CriisTXDEN;
        //    /// <summary>
        //    /// RI of port D acts as RS485 transmit enable (TXDEN)
        //    /// </summary>
        //    public bool DriisTXDEN;
        //    /// <summary>
        //    /// Determines if channel A loads the VCP driver
        //    /// </summary>
        //    public bool AisVCP = true;
        //    /// <summary>
        //    /// Determines if channel B loads the VCP driver
        //    /// </summary>
        //    public bool BisVCP = true;
        //    /// <summary>
        //    /// Determines if channel C loads the VCP driver
        //    /// </summary>
        //    public bool CisVCP = true;
        //    /// <summary>
        //    /// Determines if channel D loads the VCP driver
        //    /// </summary>
        //    public bool DisVCP = true;
        //}
        //// EEPROM class for FT232H
        ///// <summary>
        ///// EEPROM structure specific to FT232H devices.
        ///// Inherits from FT_EEPROM_DATA.
        ///// </summary>
        //public class FT232H_EEPROM_STRUCTURE : FtEepromData
        //{
        //    /// <summary>
        //    /// Determines if IOs are pulled down when the device is in suspend
        //    /// </summary>
        //    public bool PullDownEnable;
        //    /// <summary>
        //    /// Determines if the serial number is enabled
        //    /// </summary>
        //    public bool SerNumEnable = true;
        //    /// <summary>
        //    /// Determines if AC pins have a slow slew rate
        //    /// </summary>
        //    public bool ACSlowSlew;
        //    /// <summary>
        //    /// Determines if the AC pins have a Schmitt input
        //    /// </summary>
        //    public bool ACSchmittInput;
        //    /// <summary>
        //    /// Determines the AC pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte ACDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Determines if AD pins have a slow slew rate
        //    /// </summary>
        //    public bool ADSlowSlew;
        //    /// <summary>
        //    /// Determines if the AD pins have a Schmitt input
        //    /// </summary>
        //    public bool ADSchmittInput;
        //    /// <summary>
        //    /// Determines the AD pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte ADDriveCurrent = FT_DRIVE_CURRENT.FtDriveCurrent4Ma;
        //    /// <summary>
        //    /// Sets the function of the CBUS0 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK30,
        //    /// FT_CBUS_CLK15, FT_CBUS_CLK7_5
        //    /// </summary>
        //    public byte Cbus0 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS1 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK30,
        //    /// FT_CBUS_CLK15, FT_CBUS_CLK7_5
        //    /// </summary>
        //    public byte Cbus1 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS2 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
        //    /// </summary>
        //    public byte Cbus2 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS3 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
        //    /// </summary>
        //    public byte Cbus3 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS4 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN
        //    /// </summary>
        //    public byte Cbus4 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS5 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
        //    /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
        //    /// </summary>
        //    public byte Cbus5 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS6 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
        //    /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
        //    /// </summary>
        //    public byte Cbus6 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS7 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE
        //    /// </summary>
        //    public byte Cbus7 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS8 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
        //    /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
        //    /// </summary>
        //    public byte Cbus8 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Sets the function of the CBUS9 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_IOMODE,
        //    /// FT_CBUS_TXDEN, FT_CBUS_CLK30, FT_CBUS_CLK15, FT_CBUS_CLK7_5
        //    /// </summary>
        //    public byte Cbus9 = FT_232H_CBUS_OPTIONS.FtCbusTristate;
        //    /// <summary>
        //    /// Determines if the device is in FIFO mode
        //    /// </summary>
        //    public bool IsFifo;
        //    /// <summary>
        //    /// Determines if the device is in FIFO target mode
        //    /// </summary>
        //    public bool IsFifoTar;
        //    /// <summary>
        //    /// Determines if the device is in fast serial mode
        //    /// </summary>
        //    public bool IsFastSer;
        //    /// <summary>
        //    /// Determines if the device is in FT1248 mode
        //    /// </summary>
        //    public bool IsFT1248;
        //    /// <summary>
        //    /// Determines FT1248 mode clock polarity
        //    /// </summary>
        //    public bool FT1248Cpol;
        //    /// <summary>
        //    /// Determines if data is ent MSB (0) or LSB (1) in FT1248 mode
        //    /// </summary>
        //    public bool FT1248Lsb;
        //    /// <summary>
        //    /// Determines if FT1248 mode uses flow control
        //    /// </summary>
        //    public bool FT1248FlowControl;
        //    /// <summary>
        //    /// Determines if the VCP driver is loaded
        //    /// </summary>
        //    public bool IsVCP = true;
        //    /// <summary>
        //    /// For self-powered designs, keeps the FT232H in low power state until ACBUS7 is high
        //    /// </summary>
        //    public bool PowerSaveEnable;
        //}

        ///// <summary>
        ///// EEPROM structure specific to X-Series devices.
        ///// Inherits from FT_EEPROM_DATA.
        ///// </summary>
        //public class FT_XSERIES_EEPROM_STRUCTURE : FtEepromData
        //{
        //    /// <summary>
        //    /// Determines if IOs are pulled down when the device is in suspend
        //    /// </summary>
        //    public bool PullDownEnable;
        //    /// <summary>
        //    /// Determines if the serial number is enabled
        //    /// </summary>
        //    public bool SerNumEnable = true;
        //    /// <summary>
        //    /// Determines if the USB version number is enabled
        //    /// </summary>
        //    public bool USBVersionEnable = true;
        //    /// <summary>
        //    /// The USB version number: 0x0200 (USB 2.0)
        //    /// </summary>
        //    public UInt16 USBVersion = 0x0200;
        //    /// <summary>
        //    /// Determines if AC pins have a slow slew rate
        //    /// </summary>
        //    public byte ACSlowSlew;
        //    /// <summary>
        //    /// Determines if the AC pins have a Schmitt input
        //    /// </summary>
        //    public byte ACSchmittInput;
        //    /// <summary>
        //    /// Determines the AC pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte ACDriveCurrent;
        //    /// <summary>
        //    /// Determines if AD pins have a slow slew rate
        //    /// </summary>
        //    public byte ADSlowSlew;
        //    /// <summary>
        //    /// Determines if AD pins have a schmitt input
        //    /// </summary>
        //    public byte ADSchmittInput;
        //    /// <summary>
        //    /// Determines the AD pins drive current in mA.  Valid values are FT_DRIVE_CURRENT_4MA, FT_DRIVE_CURRENT_8MA, FT_DRIVE_CURRENT_12MA or FT_DRIVE_CURRENT_16MA
        //    /// </summary>
        //    public byte ADDriveCurrent;
        //    /// <summary>
        //    /// Sets the function of the CBUS0 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
        //    /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
        //    /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
        //    /// </summary>
        //    public byte Cbus0;
        //    /// <summary>
        //    /// Sets the function of the CBUS1 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
        //    /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
        //    /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
        //    /// </summary>
        //    public byte Cbus1;
        //    /// <summary>
        //    /// Sets the function of the CBUS2 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
        //    /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
        //    /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
        //    /// </summary>
        //    public byte Cbus2;
        //    /// <summary>
        //    /// Sets the function of the CBUS3 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_GPIO, FT_CBUS_TXDEN, FT_CBUS_CLK24,
        //    /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
        //    /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
        //    /// </summary>
        //    public byte Cbus3;
        //    /// <summary>
        //    /// Sets the function of the CBUS4 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
        //    /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
        //    /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
        //    /// </summary>
        //    public byte Cbus4;
        //    /// <summary>
        //    /// Sets the function of the CBUS5 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
        //    /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
        //    /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
        //    /// </summary>
        //    public byte Cbus5;
        //    /// <summary>
        //    /// Sets the function of the CBUS6 pin for FT232H devices.
        //    /// Valid values are FT_CBUS_TRISTATE, FT_CBUS_RXLED, FT_CBUS_TXLED, FT_CBUS_TXRXLED,
        //    /// FT_CBUS_PWREN, FT_CBUS_SLEEP, FT_CBUS_DRIVE_0, FT_CBUS_DRIVE_1, FT_CBUS_TXDEN, FT_CBUS_CLK24,
        //    /// FT_CBUS_CLK12, FT_CBUS_CLK6, FT_CBUS_BCD_CHARGER, FT_CBUS_BCD_CHARGER_N, FT_CBUS_VBUS_SENSE, FT_CBUS_BITBANG_WR,
        //    /// FT_CBUS_BITBANG_RD, FT_CBUS_TIME_STAMP, FT_CBUS_KEEP_AWAKE
        //    /// </summary>
        //    public byte Cbus6;
        //    /// <summary>
        //    /// Inverts the sense of the TXD line
        //    /// </summary>
        //    public byte InvertTXD;
        //    /// <summary>
        //    /// Inverts the sense of the RXD line
        //    /// </summary>
        //    public byte InvertRXD;
        //    /// <summary>
        //    /// Inverts the sense of the RTS line
        //    /// </summary>
        //    public byte InvertRTS;
        //    /// <summary>
        //    /// Inverts the sense of the CTS line
        //    /// </summary>
        //    public byte InvertCTS;
        //    /// <summary>
        //    /// Inverts the sense of the DTR line
        //    /// </summary>
        //    public byte InvertDTR;
        //    /// <summary>
        //    /// Inverts the sense of the DSR line
        //    /// </summary>
        //    public byte InvertDSR;
        //    /// <summary>
        //    /// Inverts the sense of the DCD line
        //    /// </summary>
        //    public byte InvertDCD;
        //    /// <summary>
        //    /// Inverts the sense of the RI line
        //    /// </summary>
        //    public byte InvertRI;
        //    /// <summary>
        //    /// Determines whether the Battery Charge Detection option is enabled.
        //    /// </summary>
        //    public byte BCDEnable;
        //    /// <summary>
        //    /// Asserts the power enable signal on CBUS when charging port detected.
        //    /// </summary>
        //    public byte BCDForceCbusPWREN;
        //    /// <summary>
        //    /// Forces the device never to go into sleep mode.
        //    /// </summary>
        //    public byte BCDDisableSleep;
        //    /// <summary>
        //    /// I2C slave device address.
        //    /// </summary>
        //    public ushort I2CSlaveAddress;
        //    /// <summary>
        //    /// I2C device ID
        //    /// </summary>
        //    public UInt32 I2CDeviceId;
        //    /// <summary>
        //    /// Disable I2C Schmitt trigger.
        //    /// </summary>
        //    public byte I2CDisableSchmitt;
        //    /// <summary>
        //    /// FT1248 clock polarity - clock idle high (1) or clock idle low (0)
        //    /// </summary>
        //    public byte FT1248Cpol;
        //    /// <summary>
        //    /// FT1248 data is LSB (1) or MSB (0)
        //    /// </summary>
        //    public byte FT1248Lsb;
        //    /// <summary>
        //    /// FT1248 flow control enable.
        //    /// </summary>
        //    public byte FT1248FlowControl;
        //    /// <summary>
        //    /// Enable RS485 Echo Suppression
        //    /// </summary>
        //    public byte RS485EchoSuppress;
        //    /// <summary>
        //    /// Enable Power Save mode.
        //    /// </summary>
        //    public byte PowerSaveEnable;
        //    /// <summary>
        //    /// Determines whether the VCP driver is loaded.
        //    /// </summary>
        //    public byte IsVCP;
        //}

        //#endregion EEPROM_STRUCTURES

        #region EXCEPTION_HANDLING
//        /// <summary>
//        /// Exceptions thrown by errors within the FTDI class.
//        /// </summary>
//        [Serializable] private class FTException : Exception
//        {
///*
//            /// <summary>
//            /// 
//            /// </summary>
//            public FTException() { }
//*/
//            /// <summary>
//            /// 
//            /// </summary>
//            /// <param name="message"></param>
//            public FTException(string message) : base(message) { }
///*
//            /// <summary>
//            /// 
//            /// </summary>
//            /// <param name="message"></param>
//            /// <param name="inner"></param>
//            public FTException(string message, Exception inner) : base(message, inner) { }
//*/
//            /// <summary>
//            /// 
//            /// </summary>
//            /// <param name="info"></param>
//            /// <param name="context"></param>
//            protected FTException(
//            System.Runtime.Serialization.SerializationInfo info,
//            System.Runtime.Serialization.StreamingContext context)
//                : base(info, context) { }
//        }
        #endregion

        #region FUNCTION_IMPORTS_FTD2XX.DLL
        // Handle to our DLL - used with GetProcAddress to load all of our functions
        IntPtr _hFtd2Xxdll = IntPtr.Zero;
        // Declare pointers to each of the functions we are going to use in FT2DXX.DLL
        // These are assigned in our constructor and freed in our destructor.
        //readonly IntPtr _pFTCreateDeviceInfoList = IntPtr.Zero;
        //readonly IntPtr _pFTGetDeviceInfoDetail = IntPtr.Zero;
        //readonly IntPtr _pFTOpen = IntPtr.Zero;
        //readonly IntPtr _pFTOpenEx = IntPtr.Zero;
        //readonly IntPtr _pFTClose = IntPtr.Zero;
        readonly IntPtr _pFTRead = IntPtr.Zero;
        readonly IntPtr _pFTWrite = IntPtr.Zero;
        //readonly IntPtr _pFTGetQueueStatus = IntPtr.Zero;
        //readonly IntPtr _pFTGetModemStatus = IntPtr.Zero;
        //readonly IntPtr _pFTGetStatus = IntPtr.Zero;
        //readonly IntPtr _pFTSetBaudRate = IntPtr.Zero;
        //readonly IntPtr _pFTSetDataCharacteristics = IntPtr.Zero;
        //readonly IntPtr _pFTSetFlowControl = IntPtr.Zero;
        //readonly IntPtr _pFTSetDtr = IntPtr.Zero;
        //readonly IntPtr _pFTClrDtr = IntPtr.Zero;
        //readonly IntPtr _pFTSetRts = IntPtr.Zero;
        //readonly IntPtr _pFTClrRts = IntPtr.Zero;
        //readonly IntPtr _pFTResetDevice = IntPtr.Zero;
        //readonly IntPtr _pFTResetPort = IntPtr.Zero;
        //readonly IntPtr _pFTCyclePort = IntPtr.Zero;
        //readonly IntPtr _pFTRescan = IntPtr.Zero;
        //readonly IntPtr _pFTReload = IntPtr.Zero;
        //readonly IntPtr _pFTPurge = IntPtr.Zero;
        //readonly IntPtr _pFTSetTimeouts = IntPtr.Zero;
        //readonly IntPtr _pFTSetBreakOn = IntPtr.Zero;
        //readonly IntPtr _pFTSetBreakOff = IntPtr.Zero;
        //readonly IntPtr _pFTGetDeviceInfo = IntPtr.Zero;
        //readonly IntPtr _pFTSetResetPipeRetryCount = IntPtr.Zero;
        //readonly IntPtr _pFTStopInTask = IntPtr.Zero;
        //readonly IntPtr _pFTRestartInTask = IntPtr.Zero;
        //readonly IntPtr _pFTGetDriverVersion = IntPtr.Zero;
        //readonly IntPtr _pFTGetLibraryVersion = IntPtr.Zero;
        //readonly IntPtr _pFTSetDeadmanTimeout = IntPtr.Zero;
        //readonly IntPtr _pFTSetChars = IntPtr.Zero;
        //readonly IntPtr _pFTSetEventNotification = IntPtr.Zero;
        //readonly IntPtr _pFTGetComPortNumber = IntPtr.Zero;
        //readonly IntPtr _pFTSetLatencyTimer = IntPtr.Zero;
        //readonly IntPtr _pFTGetLatencyTimer = IntPtr.Zero;
        //readonly IntPtr _pFTSetBitMode = IntPtr.Zero;
        //readonly IntPtr _pFTGetBitMode = IntPtr.Zero;
        //readonly IntPtr _pFTSetUSBParameters = IntPtr.Zero;
        //readonly IntPtr _pFTReadEe = IntPtr.Zero;
        //readonly IntPtr _pFTWriteEe = IntPtr.Zero;
        //readonly IntPtr _pFTEraseEe = IntPtr.Zero;
        //readonly IntPtr _pFTEeUaSize = IntPtr.Zero;
        //readonly IntPtr _pFTEeUaRead = IntPtr.Zero;
        //readonly IntPtr _pFTEeUaWrite = IntPtr.Zero;
        //readonly IntPtr _pFTEeRead = IntPtr.Zero;
        //readonly IntPtr _pFTEeProgram = IntPtr.Zero;
        //readonly IntPtr _pFTEepromRead = IntPtr.Zero;
        //readonly IntPtr _pFTEepromProgram = IntPtr.Zero;
        #endregion FUNCTION_IMPORTS_FTD2XX.DLL

        // ReSharper disable CSharpWarnings::CS1584
        // ReSharper disable CSharpWarnings::CS1571
        #region METHOD_DEFINITIONS
        //**************************************************************************
        // GetNumberOfDevices
        //**************************************************************************
        // Intellisense comments
        //public FtStatus GetNumberOfDevices(ref UInt32 devcount)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTCreateDeviceInfoList != IntPtr.Zero)
        //    {
        //        var ftCreateDeviceInfoList = (FtCreateDeviceInfoList)Marshal.GetDelegateForFunctionPointer(_pFTCreateDeviceInfoList, typeof(FtCreateDeviceInfoList));

        //        // Call FT_CreateDeviceInfoList
        //        ftStatus = ftCreateDeviceInfoList(ref devcount);
        //    }
        //    //else
        //    //{
        //    //    //MessageBox.Show("Failed to load function FT_CreateDeviceInfoList.");
        //    //}
        //    return ftStatus;

        //}


        //**************************************************************************
        // GetDeviceList
        //**************************************************************************
        // Intellisense comments
        //public FtStatus GetDeviceList(FtDeviceInfoNode[] devicelist)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTCreateDeviceInfoList != IntPtr.Zero) & (_pFTGetDeviceInfoDetail != IntPtr.Zero))
        //    {
        //        UInt32 devcount = 0;

        //        FtCreateDeviceInfoList ftCreateDeviceInfoList = (FtCreateDeviceInfoList)Marshal.GetDelegateForFunctionPointer(_pFTCreateDeviceInfoList, typeof(FtCreateDeviceInfoList));
        //        FtGetDeviceInfoDetail ftGetDeviceInfoDetail = (FtGetDeviceInfoDetail)Marshal.GetDelegateForFunctionPointer(_pFTGetDeviceInfoDetail, typeof(FtGetDeviceInfoDetail));

        //        // Call FT_CreateDeviceInfoList
        //        ftStatus = ftCreateDeviceInfoList(ref devcount);

        //        // Allocate the required storage for our list

        //        var sernum = new byte[16];
        //        var desc = new byte[64];

        //        if (devcount > 0)
        //        {
        //            // Check the size of the buffer passed in is big enough
        //            if (devicelist.Length < devcount) {
        //                // Buffer not big enough
        //                const FtError ftErrorCondition = FtError.FtBufferSize;
        //                // Throw exception
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Instantiate the array elements as FT_DEVICE_INFO_NODE
        //            for (UInt32 i = 0; i < devcount; i++)
        //            {
        //                devicelist[i] = new FtDeviceInfoNode();
        //                // Call FT_GetDeviceInfoDetail
        //                ftStatus = ftGetDeviceInfoDetail(i, ref devicelist[i].Flags, ref devicelist[i].Type, ref devicelist[i].ID, ref devicelist[i].LocId, sernum, desc, ref devicelist[i].FtHandle);
        //                // Convert byte arrays to strings
        //                devicelist[i].SerialNumber = Encoding.ASCII.GetString(sernum);
        //                devicelist[i].Description = Encoding.ASCII.GetString(desc);
        //                // Trim strings to first occurrence of a null terminator character
        //                devicelist[i].SerialNumber = devicelist[i].SerialNumber.Substring(0, devicelist[i].SerialNumber.IndexOf("\0", StringComparison.Ordinal));
        //                devicelist[i].Description = devicelist[i].Description.Substring(0, devicelist[i].Description.IndexOf("\0", StringComparison.Ordinal));
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTCreateDeviceInfoList == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_CreateDeviceInfoList.");
        //        }
        //        if (_pFTGetDeviceInfoDetail == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetDeviceInfoListDetail.");
        //        }
        //    }
        //    return ftStatus;
        //}


        //**************************************************************************
        // OpenByIndex
        //**************************************************************************
        // Intellisense comments
        //public FtStatus OpenByIndex(UInt32 index)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTOpen != IntPtr.Zero) & (_pFTSetDataCharacteristics != IntPtr.Zero) & (_pFTSetFlowControl != IntPtr.Zero) & (_pFTSetBaudRate != IntPtr.Zero))
        //    {
        //        var ftOpen = (FtOpen)Marshal.GetDelegateForFunctionPointer(_pFTOpen, typeof(FtOpen));
        //        var ftSetDataCharacteristics = (FtSetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(_pFTSetDataCharacteristics, typeof(FtSetDataCharacteristics));
        //        var ftSetFlowControl = (FtSetFlowControl)Marshal.GetDelegateForFunctionPointer(_pFTSetFlowControl, typeof(FtSetFlowControl));
        //        var ftSetBaudRate = (FtSetBaudRate)Marshal.GetDelegateForFunctionPointer(_pFTSetBaudRate, typeof(FtSetBaudRate));

        //        // Call FT_Open
        //        ftStatus = ftOpen(index, ref _ftHandle);

        //        // Appears that the handle value can be non-NULL on a fail, so set it explicitly
        //        if (ftStatus != FtStatus.FtOk)
        //            _ftHandle = IntPtr.Zero;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Initialise port data characteristics
        //            const byte wordLength = FT_DATA_BITS.FtBits8;
        //            const byte stopBits = FT_STOP_BITS.FtStopBits1;
        //            const byte parity = FT_PARITY.FtParityNone;
        //            ftSetDataCharacteristics(_ftHandle, wordLength, stopBits, parity);
        //            // Initialise to no flow control
        //            const ushort flowControl = FT_FLOW_CONTROL.FtFlowNone;
        //            const byte xon = 0x11;
        //            const byte xoff = 0x13;
        //            ftSetFlowControl(_ftHandle, flowControl, xon, xoff);
        //            // Initialise Baud rate
        //            const uint baudRate = 9600;
        //            ftStatus = ftSetBaudRate(_ftHandle, baudRate);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTOpen == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_Open.");
        //        }
        //        if (_pFTSetDataCharacteristics == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
        //        }
        //        if (_pFTSetFlowControl == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetFlowControl.");
        //        }
        //        if (_pFTSetBaudRate == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBaudRate.");
        //        }
        //    }
        //    return ftStatus;
        //}


        //**************************************************************************
        // OpenBySerialNumber
        //**************************************************************************
        // Intellisense comments
        //public FtStatus OpenBySerialNumber(string serialnumber)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTOpenEx != IntPtr.Zero) & (_pFTSetDataCharacteristics != IntPtr.Zero) & (_pFTSetFlowControl != IntPtr.Zero) & (_pFTSetBaudRate != IntPtr.Zero))
        //    {
        //        var ftOpenEx = (FtOpenEx)Marshal.GetDelegateForFunctionPointer(_pFTOpenEx, typeof(FtOpenEx));
        //        var ftSetDataCharacteristics = (FtSetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(_pFTSetDataCharacteristics, typeof(FtSetDataCharacteristics));
        //        var ftSetFlowControl = (FtSetFlowControl)Marshal.GetDelegateForFunctionPointer(_pFTSetFlowControl, typeof(FtSetFlowControl));
        //        var ftSetBaudRate = (FtSetBaudRate)Marshal.GetDelegateForFunctionPointer(_pFTSetBaudRate, typeof(FtSetBaudRate));

        //        // Call FT_OpenEx
        //        ftStatus = ftOpenEx(serialnumber, FtOpenBySerialNumber, ref _ftHandle);

        //        // Appears that the handle value can be non-NULL on a fail, so set it explicitly
        //        if (ftStatus != FtStatus.FtOk)
        //            _ftHandle = IntPtr.Zero;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Initialise port data characteristics
        //            const byte wordLength = FT_DATA_BITS.FtBits8;
        //            const byte stopBits = FT_STOP_BITS.FtStopBits1;
        //            const byte parity = FT_PARITY.FtParityNone;
        //            ftSetDataCharacteristics(_ftHandle, wordLength, stopBits, parity);
        //            // Initialise to no flow control
        //            const ushort flowControl = FT_FLOW_CONTROL.FtFlowNone;
        //            const byte xon = 0x11;
        //            const byte xoff = 0x13;
        //            ftSetFlowControl(_ftHandle, flowControl, xon, xoff);
        //            // Initialise Baud rate
        //            const uint baudRate = 9600;
        //            ftStatus = ftSetBaudRate(_ftHandle, baudRate);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTOpenEx == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_OpenEx.");
        //        }
        //        if (_pFTSetDataCharacteristics == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
        //        }
        //        if (_pFTSetFlowControl == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetFlowControl.");
        //        }
        //        if (_pFTSetBaudRate == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBaudRate.");
        //        }
        //    }
        //    return ftStatus;
        //}


        //**************************************************************************
        // OpenByDescription
        //**************************************************************************
        // Intellisense comments
        //public FtStatus OpenByDescription(string description)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTOpenEx != IntPtr.Zero) & (_pFTSetDataCharacteristics != IntPtr.Zero) & (_pFTSetFlowControl != IntPtr.Zero) & (_pFTSetBaudRate != IntPtr.Zero))
        //    {
        //        var ftOpenEx = (FtOpenEx)Marshal.GetDelegateForFunctionPointer(_pFTOpenEx, typeof(FtOpenEx));
        //        var ftSetDataCharacteristics = (FtSetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(_pFTSetDataCharacteristics, typeof(FtSetDataCharacteristics));
        //        var ftSetFlowControl = (FtSetFlowControl)Marshal.GetDelegateForFunctionPointer(_pFTSetFlowControl, typeof(FtSetFlowControl));
        //        var ftSetBaudRate = (FtSetBaudRate)Marshal.GetDelegateForFunctionPointer(_pFTSetBaudRate, typeof(FtSetBaudRate));

        //        // Call FT_OpenEx
        //        ftStatus = ftOpenEx(description, FtOpenByDescription, ref _ftHandle);

        //        // Appears that the handle value can be non-NULL on a fail, so set it explicitly
        //        if (ftStatus != FtStatus.FtOk)
        //            _ftHandle = IntPtr.Zero;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Initialise port data characteristics
        //            const byte wordLength = FT_DATA_BITS.FtBits8;
        //            const byte stopBits = FT_STOP_BITS.FtStopBits1;
        //            const byte parity = FT_PARITY.FtParityNone;
        //            ftSetDataCharacteristics(_ftHandle, wordLength, stopBits, parity);
        //            // Initialise to no flow control
        //            const ushort flowControl = FT_FLOW_CONTROL.FtFlowNone;
        //            const byte xon = 0x11;
        //            const byte xoff = 0x13;
        //            ftSetFlowControl(_ftHandle, flowControl, xon, xoff);
        //            // Initialise Baud rate
        //            const uint baudRate = 9600;
        //            ftStatus = ftSetBaudRate(_ftHandle, baudRate);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTOpenEx == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_OpenEx.");
        //        }
        //        if (_pFTSetDataCharacteristics == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
        //        }
        //        if (_pFTSetFlowControl == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetFlowControl.");
        //        }
        //        if (_pFTSetBaudRate == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBaudRate.");
        //        }
        //    }
        //    return ftStatus;
        //}


        //**************************************************************************
        // OpenByLocation
        //**************************************************************************
        // Intellisense comments
        //public FtStatus OpenByLocation(UInt32 location)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTOpenEx != IntPtr.Zero) & (_pFTSetDataCharacteristics != IntPtr.Zero) & (_pFTSetFlowControl != IntPtr.Zero) & (_pFTSetBaudRate != IntPtr.Zero))
        //    {
        //        var ftOpenEx = (FtOpenExLoc)Marshal.GetDelegateForFunctionPointer(_pFTOpenEx, typeof(FtOpenExLoc));
        //        var ftSetDataCharacteristics = (FtSetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(_pFTSetDataCharacteristics, typeof(FtSetDataCharacteristics));
        //        var ftSetFlowControl = (FtSetFlowControl)Marshal.GetDelegateForFunctionPointer(_pFTSetFlowControl, typeof(FtSetFlowControl));
        //        var ftSetBaudRate = (FtSetBaudRate)Marshal.GetDelegateForFunctionPointer(_pFTSetBaudRate, typeof(FtSetBaudRate));

        //        // Call FT_OpenEx
        //        ftStatus = ftOpenEx(location, FtOpenByLocation, ref _ftHandle);

        //        // Appears that the handle value can be non-NULL on a fail, so set it explicitly
        //        if (ftStatus != FtStatus.FtOk)
        //            _ftHandle = IntPtr.Zero;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Initialise port data characteristics
        //            const byte wordLength = FT_DATA_BITS.FtBits8;
        //            const byte stopBits = FT_STOP_BITS.FtStopBits1;
        //            const byte parity = FT_PARITY.FtParityNone;
        //            ftSetDataCharacteristics(_ftHandle, wordLength, stopBits, parity);
        //            // Initialise to no flow control
        //            const ushort flowControl = FT_FLOW_CONTROL.FtFlowNone;
        //            const byte xon = 0x11;
        //            const byte xoff = 0x13;
        //            ftSetFlowControl(_ftHandle, flowControl, xon, xoff);
        //            // Initialise Baud rate
        //            const uint baudRate = 9600;
        //            ftStatus = ftSetBaudRate(_ftHandle, baudRate);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTOpenEx == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_OpenEx.");
        //        }
        //        if (_pFTSetDataCharacteristics == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
        //        }
        //        if (_pFTSetFlowControl == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetFlowControl.");
        //        }
        //        if (_pFTSetBaudRate == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBaudRate.");
        //        }
        //    }
        //    return ftStatus;
        //}


        //**************************************************************************
        // Close
        //**************************************************************************
        // Intellisense comments
        //public FtStatus Close()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTClose != IntPtr.Zero)
        //    {
        //        var ftClose = (FtClose)Marshal.GetDelegateForFunctionPointer(_pFTClose, typeof(FtClose));

        //        // Call FT_Close
        //        ftStatus = ftClose(_ftHandle);

        //        if (ftStatus == FtStatus.FtOk)
        //        {
        //            _ftHandle = IntPtr.Zero;
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTClose == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_Close.");
        //        }
        //    }
        //    return ftStatus;
        //}


        //**************************************************************************
        // Read
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the number of FTDI devices available.  
        /// </summary>
        /// <returns>FT_STATUS value from FT_CreateDeviceInfoList in FTD2XX.DLL</returns>
        /// <param name="dataBuffer">An array of bytes which will be populated with the data read from the device.</param>
        /// <param name="numBytesToRead">The number of bytes requested from the device.</param>
        /// <param name="numBytesRead">The number of bytes actually read.</param>
        /// <param name="devcount">The number of FTDI devices available.</param>
        /// <summary>
        /// Gets information on all of the FTDI devices available.  
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetDeviceInfoDetail in FTD2XX.DLL</returns>
        /// <param name="devicelist">An array of type FT_DEVICE_INFO_NODE to contain the device information for all available devices.</param>
        /// <exception cref="FTException">Thrown when the supplied buffer is not large enough to contain the device info list.</exception>
        /// <summary>
        /// Opens the FTDI device with the specified index.  
        /// </summary>
        /// <returns>FT_STATUS value from FT_Open in FTD2XX.DLL</returns>
        /// <param name="index">Index of the device to open.
        /// Note that this cannot be guaranteed to open a specific device.</param>
        /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
        /// <summary>
        /// Opens the FTDI device with the specified serial number.  
        /// </summary>
        /// <returns>FT_STATUS value from FT_OpenEx in FTD2XX.DLL</returns>
        /// <param name="serialnumber">Serial number of the device to open.</param>
        /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
        /// <summary>
        /// Opens the FTDI device with the specified description.  
        /// </summary>
        /// <returns>FT_STATUS value from FT_OpenEx in FTD2XX.DLL</returns>
        /// <param name="description">Description of the device to open.</param>
        /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
        /// <summary>
        /// Opens the FTDI device at the specified physical location.  
        /// </summary>
        /// <returns>FT_STATUS value from FT_OpenEx in FTD2XX.DLL</returns>
        /// <param name="location">Location of the device to open.</param>
        /// <remarks>Initialises the device to 8 data bits, 1 stop bit, no parity, no flow control and 9600 Baud.</remarks>
        /// <summary>
        /// Closes the handle to an open FTDI device.  
        /// </summary>
        /// <returns>FT_STATUS value from FT_Close in FTD2XX.DLL</returns>
        /// <summary>
        /// Read data from an open FTDI device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Read in FTD2XX.DLL</returns>
        public void Read(byte[] dataBuffer, uint numBytesToRead, ref uint numBytesRead)
        {
            // Initialise ftStatus to something other than FT_OK
            _lastStatus = FtStatus.FtOtherError;

            // If the DLL hasn't been loaded, just return here
            if (_hFtd2Xxdll == IntPtr.Zero)
                return;

            // Check for our required function pointers being set up
            if (_pFTRead != IntPtr.Zero)
            {

                var ftRead = (FtRead)Marshal.GetDelegateForFunctionPointer(_pFTRead, typeof(FtRead));

                // If the buffer is not big enough to receive the amount of data requested, adjust the number of bytes to read
                if (dataBuffer.Length < numBytesToRead)
                {
                    numBytesToRead = (uint)dataBuffer.Length;
                }

                if (_ftHandle != IntPtr.Zero)
                {
                    // Call FT_Read
                    _lastStatus = ftRead(_ftHandle, dataBuffer, numBytesToRead, ref numBytesRead);
                }
            }
            else
            {
                if (_pFTRead == IntPtr.Zero)
                {
                    //MessageBox.Show("Failed to load function FT_Read.");
                }
            }
        }

        // Intellisense comments
        /// <summary>
        /// Read data from an open FTDI device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Read in FTD2XX.DLL</returns>
        /// <param name="dataBuffer">A string containing the data read</param>
        /// <param name="numBytesToRead">The number of bytes requested from the device.</param>
        /// <param name="numBytesRead">The number of bytes actually read.</param>
        //public FtStatus Read(out string dataBuffer, UInt32 numBytesToRead, ref UInt32 numBytesRead)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // As dataBuffer is an OUT parameter, needs to be assigned before returning
        //    dataBuffer = string.Empty;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTRead != IntPtr.Zero)
        //    {
        //        var ftRead = (FtRead)Marshal.GetDelegateForFunctionPointer(_pFTRead, typeof(FtRead));

        //        var byteDataBuffer = new byte[numBytesToRead];

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_Read
        //            ftStatus = ftRead(_ftHandle, byteDataBuffer, numBytesToRead, ref numBytesRead);

        //            // Convert ASCII byte array back to Unicode string for passing back
        //            dataBuffer = Encoding.ASCII.GetString(byteDataBuffer);
        //            // Trim buffer to actual bytes read
        //            dataBuffer = dataBuffer.Substring(0, (int)numBytesRead);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // Write
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Write data to an open FTDI device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>

        /// <param name="dataBuffer">An array of bytes which contains the data to be written to the device.</param>
        /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
        /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
        public FtStatus Write(byte[] dataBuffer, Int32 numBytesToWrite, ref UInt32 numBytesWritten)
        {
            // Initialise ftStatus to something other than FT_OK
            var ftStatus = FtStatus.FtOtherError;

            // If the DLL hasn't been loaded, just return here
            if (_hFtd2Xxdll == IntPtr.Zero)
                return ftStatus;

            // Check for our required function pointers being set up
            if (_pFTWrite != IntPtr.Zero)
            {
                var ftWrite = (FtWrite)Marshal.GetDelegateForFunctionPointer(_pFTWrite, typeof(FtWrite));

                if (_ftHandle != IntPtr.Zero)
                {
                    // Call FT_Write
                    ftStatus = ftWrite(_ftHandle, dataBuffer, (UInt32)numBytesToWrite, ref numBytesWritten);
                }
            }
            else
            {
                if (_pFTWrite == IntPtr.Zero)
                {
                    //MessageBox.Show("Failed to load function FT_Write.");
                }
            }
            return ftStatus;
        }

        // Intellisense comments
        /// <summary>
        /// Write data to an open FTDI device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>
        /// <param name="dataBuffer">An array of bytes which contains the data to be written to the device.</param>
        /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
        /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
        public FtStatus Write(byte[] dataBuffer, UInt32 numBytesToWrite, ref UInt32 numBytesWritten)
        {
            // Initialise ftStatus to something other than FT_OK
            var ftStatus = FtStatus.FtOtherError;

            // If the DLL hasn't been loaded, just return here
            if (_hFtd2Xxdll == IntPtr.Zero)
                return ftStatus;

            // Check for our required function pointers being set up
            if (_pFTWrite != IntPtr.Zero)
            {
                var ftWrite = (FtWrite)Marshal.GetDelegateForFunctionPointer(_pFTWrite, typeof(FtWrite));

                if (_ftHandle != IntPtr.Zero)
                {
                    // Call FT_Write
                    ftStatus = ftWrite(_ftHandle, dataBuffer, numBytesToWrite, ref numBytesWritten);
                }
            }
            else
            {
                if (_pFTWrite == IntPtr.Zero)
                {
                    //MessageBox.Show("Failed to load function FT_Write.");
                }
            }
            return ftStatus;
        }

        // Intellisense comments
        /// <summary>
        /// Write data to an open FTDI device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>
        /// <param name="dataBuffer">A  string which contains the data to be written to the device.</param>
        /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
        /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
        public FtStatus Write(string dataBuffer, Int32 numBytesToWrite, ref UInt32 numBytesWritten)
        {
            // Initialise ftStatus to something other than FT_OK
            var ftStatus = FtStatus.FtOtherError;

            // If the DLL hasn't been loaded, just return here
            if (_hFtd2Xxdll == IntPtr.Zero)
                return ftStatus;

            // Check for our required function pointers being set up
            if (_pFTWrite != IntPtr.Zero)
            {
                var ftWrite = (FtWrite)Marshal.GetDelegateForFunctionPointer(_pFTWrite, typeof(FtWrite));

                // Convert Unicode string to ASCII byte array
                var byteDataBuffer = Encoding.ASCII.GetBytes(dataBuffer);

                if (_ftHandle != IntPtr.Zero)
                {
                    // Call FT_Write
                    ftStatus = ftWrite(_ftHandle, byteDataBuffer, (UInt32)numBytesToWrite, ref numBytesWritten);
                }
            }
            else
            {
                if (_pFTWrite == IntPtr.Zero)
                {
                    //MessageBox.Show("Failed to load function FT_Write.");
                }
            }
            return ftStatus;
        }

        // Intellisense comments
        /// <summary>
        /// Write data to an open FTDI device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Write in FTD2XX.DLL</returns>
        /// <param name="dataBuffer">A  string which contains the data to be written to the device.</param>
        /// <param name="numBytesToWrite">The number of bytes to be written to the device.</param>
        /// <param name="numBytesWritten">The number of bytes actually written to the device.</param>
        public FtStatus Write(string dataBuffer, UInt32 numBytesToWrite, ref UInt32 numBytesWritten)
        {
            // Initialise ftStatus to something other than FT_OK
            var ftStatus = FtStatus.FtOtherError;

            // If the DLL hasn't been loaded, just return here
            if (_hFtd2Xxdll == IntPtr.Zero)
                return ftStatus;

            // Check for our required function pointers being set up
            if (_pFTWrite != IntPtr.Zero)
            {
                var ftWrite = (FtWrite)Marshal.GetDelegateForFunctionPointer(_pFTWrite, typeof(FtWrite));

                // Convert Unicode string to ASCII byte array
                var byteDataBuffer = Encoding.ASCII.GetBytes(dataBuffer);

                if (_ftHandle != IntPtr.Zero)
                {
                    // Call FT_Write
                    ftStatus = ftWrite(_ftHandle, byteDataBuffer, numBytesToWrite, ref numBytesWritten);
                }
            }
            else
            {
                if (_pFTWrite == IntPtr.Zero)
                {
                    //MessageBox.Show("Failed to load function FT_Write.");
                }
            }
            return ftStatus;
        }

        //**************************************************************************
        // ResetDevice
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reset an open FTDI device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_ResetDevice in FTD2XX.DLL</returns>
        //public FtStatus ResetDevice()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTResetDevice != IntPtr.Zero)
        //    {
        //        var ftResetDevice = (FtResetDevice)Marshal.GetDelegateForFunctionPointer(_pFTResetDevice, typeof(FtResetDevice));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_ResetDevice
        //            ftStatus = ftResetDevice(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTResetDevice == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_ResetDevice.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // Purge
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Purge data from the devices transmit and/or receive buffers.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Purge in FTD2XX.DLL</returns>
        /// <param name="purgemask">Specifies which buffer(s) to be purged.  Valid values are any combination of the following flags: FT_PURGE_RX, FT_PURGE_TX</param>
        //public FtStatus Purge(UInt32 purgemask)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTPurge != IntPtr.Zero)
        //    {
        //        var ftPurge = (FtPurge)Marshal.GetDelegateForFunctionPointer(_pFTPurge, typeof(FtPurge));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_Purge
        //            ftStatus = ftPurge(_ftHandle, purgemask);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTPurge == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_Purge.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetEventNotification
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Register for event notification.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetEventNotification in FTD2XX.DLL</returns>
        /// <remarks>After setting event notification, the event can be caught by executing the WaitOne() method of the EventWaitHandle.  If multiple event types are being monitored, the event that fired can be determined from the GetEventType method.</remarks>
        /// <param name="eventmask">The type of events to signal.  Can be any combination of the following: FT_EVENT_RXCHAR, FT_EVENT_MODEM_STATUS, FT_EVENT_LINE_STATUS</param>
        /// <param name="eventhandle">Handle to the event that will receive the notification</param>
        //public FtStatus SetEventNotification(UInt32 eventmask, EventWaitHandle eventhandle)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetEventNotification != IntPtr.Zero)
        //    {
        //        var ftSetEventNotification = (FtSetEventNotification)Marshal.GetDelegateForFunctionPointer(_pFTSetEventNotification, typeof(FtSetEventNotification));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetSetEventNotification
        //            ftStatus = ftSetEventNotification(_ftHandle, eventmask, eventhandle.SafeWaitHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetEventNotification == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetEventNotification.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // StopInTask
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Stops the driver issuing USB in requests.
        /// </summary>
        /// <returns>FT_STATUS value from FT_StopInTask in FTD2XX.DLL</returns>
        //public FtStatus StopInTask()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTStopInTask != IntPtr.Zero)
        //    {
        //        var ftStopInTask = (FtStopInTask)Marshal.GetDelegateForFunctionPointer(_pFTStopInTask, typeof(FtStopInTask));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_StopInTask
        //            ftStatus = ftStopInTask(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTStopInTask == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_StopInTask.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // RestartInTask
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Resumes the driver issuing USB in requests.
        /// </summary>
        /// <returns>FT_STATUS value from FT_RestartInTask in FTD2XX.DLL</returns>
        //public FtStatus RestartInTask()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTRestartInTask != IntPtr.Zero)
        //    {
        //        var ftRestartInTask = (FtRestartInTask)Marshal.GetDelegateForFunctionPointer(_pFTRestartInTask, typeof(FtRestartInTask));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_RestartInTask
        //            ftStatus = ftRestartInTask(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTRestartInTask == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_RestartInTask.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ResetPort
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Resets the device port.
        /// </summary>
        /// <returns>FT_STATUS value from FT_ResetPort in FTD2XX.DLL</returns>
        //public FtStatus ResetPort()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTResetPort != IntPtr.Zero)
        //    {
        //        var ftResetPort = (FtResetPort)Marshal.GetDelegateForFunctionPointer(_pFTResetPort, typeof(FtResetPort));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_ResetPort
        //            ftStatus = ftResetPort(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTResetPort == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_ResetPort.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // CyclePort
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Causes the device to be re-enumerated on the USB bus.  This is equivalent to unplugging and replugging the device.
        /// Also calls FT_Close if FT_CyclePort is successful, so no need to call this separately in the application.
        /// </summary>
        /// <returns>FT_STATUS value from FT_CyclePort in FTD2XX.DLL</returns>
        //public FtStatus CyclePort()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTCyclePort != IntPtr.Zero) & (_pFTClose != IntPtr.Zero))
        //    {
        //        var ftCyclePort = (FtCyclePort)Marshal.GetDelegateForFunctionPointer(_pFTCyclePort, typeof(FtCyclePort));
        //        var ftClose = (FtClose)Marshal.GetDelegateForFunctionPointer(_pFTClose, typeof(FtClose));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_CyclePort
        //            ftStatus = ftCyclePort(_ftHandle);
        //            if (ftStatus == FtStatus.FtOk)
        //            {
        //                // If successful, call FT_Close
        //                ftStatus = ftClose(_ftHandle);
        //                if (ftStatus == FtStatus.FtOk)
        //                {
        //                    _ftHandle = IntPtr.Zero;
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTCyclePort == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_CyclePort.");
        //        }
        //        if (_pFTClose == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_Close.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // Rescan
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Causes the system to check for USB hardware changes.  This is equivalent to clicking on the "Scan for hardware changes" button in the Device Manager.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Rescan in FTD2XX.DLL</returns>
        //public FtStatus Rescan()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTRescan != IntPtr.Zero)
        //    {
        //        var ftRescan = (FtRescan)Marshal.GetDelegateForFunctionPointer(_pFTRescan, typeof(FtRescan));

        //        // Call FT_Rescan
        //        ftStatus = ftRescan();
        //    }
        //    else
        //    {
        //        if (_pFTRescan == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_Rescan.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // Reload
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Forces a reload of the driver for devices with a specific VID and PID combination.
        /// </summary>
        /// <returns>FT_STATUS value from FT_Reload in FTD2XX.DLL</returns>
        /// <remarks>If the VID and PID parameters are 0, the drivers for USB root hubs will be reloaded, causing all USB devices connected to reload their drivers</remarks>
        /// <param name="vendorID">Vendor ID of the devices to have the driver reloaded</param>
        /// <param name="productID">Product ID of the devices to have the driver reloaded</param>
        //public FtStatus Reload(UInt16 vendorID, UInt16 productID)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTReload != IntPtr.Zero)
        //    {
        //        var ftReload = (FtReload)Marshal.GetDelegateForFunctionPointer(_pFTReload, typeof(FtReload));

        //        // Call FT_Reload
        //        ftStatus = ftReload(vendorID, productID);
        //    }
        //    else
        //    {
        //        if (_pFTReload == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_Reload.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetBitMode
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Puts the device in a mode other than the default UART or FIFO mode.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetBitMode in FTD2XX.DLL</returns>
        /// <param name="mask">Sets up which bits are inputs and which are outputs.  A bit value of 0 sets the corresponding pin to an input, a bit value of 1 sets the corresponding pin to an output.
        /// In the case of CBUS Bit Bang, the upper nibble of this value controls which pins are inputs and outputs, while the lower nibble controls which of the outputs are high and low.</param>
        /// <param name="bitMode"> For FT232H devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_CBUS_BITBANG, FT_BIT_MODE_MCU_HOST, FT_BIT_MODE_FAST_SERIAL, FT_BIT_MODE_SYNC_FIFO.
        /// For FT2232H devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_MCU_HOST, FT_BIT_MODE_FAST_SERIAL, FT_BIT_MODE_SYNC_FIFO.
        /// For FT4232H devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG.
        /// For FT232R devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_CBUS_BITBANG.
        /// For FT245R devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_SYNC_BITBANG.
        /// For FT2232 devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG, FT_BIT_MODE_MPSSE, FT_BIT_MODE_SYNC_BITBANG, FT_BIT_MODE_MCU_HOST, FT_BIT_MODE_FAST_SERIAL.
        /// For FT232B and FT245B devices, valid values are FT_BIT_MODE_RESET, FT_BIT_MODE_ASYNC_BITBANG.</param>
        /// <exception cref="FTException">Thrown when the current device does not support the requested bit mode.</exception>
        //public FtStatus SetBitMode(byte mask, byte bitMode)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetBitMode != IntPtr.Zero)
        //    {
        //        var ftSetBitMode = (FtSetBitMode)Marshal.GetDelegateForFunctionPointer(_pFTSetBitMode, typeof(FtSetBitMode));

        //        if (_ftHandle != IntPtr.Zero)
        //        {

        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Set Bit Mode does not apply to FT8U232AM, FT8U245AM or FT8U100AX devices
        //            GetDeviceType(ref deviceType);
        //            FtError ftErrorCondition;
        //            switch (deviceType) {
        //                case FtDevice.FtDeviceAm:
        //                    ftErrorCondition = FtError.FtInvalidBitmode;
        //                    ErrorHandler(ftStatus, ftErrorCondition);
        //                    break;
        //                case FtDevice.FtDevice100Ax:
        //                    ftErrorCondition = FtError.FtInvalidBitmode;
        //                    ErrorHandler(ftStatus, ftErrorCondition);
        //                    break;
        //                default:
        //                    if ((deviceType == FtDevice.FtDeviceBm) && (bitMode != FT_BIT_MODES.FtBitModeReset))
        //                    {
        //                        if ((bitMode & (FT_BIT_MODES.FtBitModeAsyncBitbang)) == 0)
        //                        {
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                    }
        //                    else if ((deviceType == FtDevice.FtDevice2232) && (bitMode != FT_BIT_MODES.FtBitModeReset))
        //                    {
        //                        if ((bitMode & (FT_BIT_MODES.FtBitModeAsyncBitbang | FT_BIT_MODES.FtBitModeMpsse | FT_BIT_MODES.FtBitModeSyncBitbang | FT_BIT_MODES.FtBitModeMcuHost | FT_BIT_MODES.FtBitModeFastSerial)) == 0)
        //                        {
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                        if ((bitMode == FT_BIT_MODES.FtBitModeMpsse) & (InterfaceIdentifier != "A"))
        //                        {
        //                            // MPSSE mode is only available on channel A
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                    }
        //                    else if ((deviceType == FtDevice.FtDevice232R) && (bitMode != FT_BIT_MODES.FtBitModeReset))
        //                    {
        //                        if ((bitMode & (FT_BIT_MODES.FtBitModeAsyncBitbang | FT_BIT_MODES.FtBitModeSyncBitbang | FT_BIT_MODES.FtBitModeCbusBitbang)) == 0)
        //                        {
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                    }
        //                    else if ((deviceType == FtDevice.FtDevice2232H) && (bitMode != FT_BIT_MODES.FtBitModeReset))
        //                    {
        //                        if ((bitMode & (FT_BIT_MODES.FtBitModeAsyncBitbang | FT_BIT_MODES.FtBitModeMpsse | FT_BIT_MODES.FtBitModeSyncBitbang | FT_BIT_MODES.FtBitModeMcuHost | FT_BIT_MODES.FtBitModeFastSerial | FT_BIT_MODES.FtBitModeSyncFifo)) == 0)
        //                        {
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                        if (((bitMode == FT_BIT_MODES.FtBitModeMcuHost) | (bitMode == FT_BIT_MODES.FtBitModeSyncFifo)) & (InterfaceIdentifier != "A"))
        //                        {
        //                            // MCU Host Emulation and Single channel synchronous 245 FIFO mode is only available on channel A
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                    }
        //                    else if ((deviceType == FtDevice.FtDevice4232H) && (bitMode != FT_BIT_MODES.FtBitModeReset))
        //                    {
        //                        if ((bitMode & (FT_BIT_MODES.FtBitModeAsyncBitbang | FT_BIT_MODES.FtBitModeMpsse | FT_BIT_MODES.FtBitModeSyncBitbang)) == 0)
        //                        {
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                        if ((bitMode == FT_BIT_MODES.FtBitModeMpsse) & ((InterfaceIdentifier != "A") & (InterfaceIdentifier != "B")))
        //                        {
        //                            // MPSSE mode is only available on channel A and B
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                    }
        //                    else if ((deviceType == FtDevice.FtDevice232H) && (bitMode != FT_BIT_MODES.FtBitModeReset))
        //                    {
        //                        // FT232H supports all current bit modes!
        //                        if (bitMode > FT_BIT_MODES.FtBitModeSyncFifo)
        //                        {
        //                            // Throw an exception
        //                            ftErrorCondition = FtError.FtInvalidBitmode;
        //                            ErrorHandler(ftStatus, ftErrorCondition);
        //                        }
        //                    }
        //                    break;
        //            }

        //            // Requested bit mode is supported
        //            // Note FT_BIT_MODES.FT_BIT_MODE_RESET falls through to here - no bits set so cannot check for AND
        //            // Call FT_SetBitMode
        //            ftStatus = ftSetBitMode(_ftHandle, mask, bitMode);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetBitMode == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBitMode.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetPinStates
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the instantaneous state of the device IO pins.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetBitMode in FTD2XX.DLL</returns>
        /// <param name="bitMode">A bitmap value containing the instantaneous state of the device IO pins</param>
        //public FtStatus GetPinStates(ref byte bitMode)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetBitMode != IntPtr.Zero)
        //    {
        //        var ftGetBitMode = (FtGetBitMode)Marshal.GetDelegateForFunctionPointer(_pFTGetBitMode, typeof(FtGetBitMode));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetBitMode
        //            ftStatus = ftGetBitMode(_ftHandle, ref bitMode);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetBitMode == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetBitMode.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadEEPROMLocation
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads an individual word value from a specified location in the device's EEPROM.
        /// </summary>
        /// <returns>FT_STATUS value from FT_ReadEE in FTD2XX.DLL</returns>
        /// <param name="address">The EEPROM location to read data from</param>
        /// <param name="eeValue">The WORD value read from the EEPROM location specified in the Address paramter</param>
        //public FtStatus ReadEEPROMLocation(UInt32 address, ref UInt16 eeValue)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTReadEe != IntPtr.Zero)
        //    {
        //        var ftReadEe = (FtReadEe)Marshal.GetDelegateForFunctionPointer(_pFTReadEe, typeof(FtReadEe));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_ReadEE
        //            ftStatus = ftReadEe(_ftHandle, address, ref eeValue);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTReadEe == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_ReadEE.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteEEPROMLocation
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes an individual word value to a specified location in the device's EEPROM.
        /// </summary>
        /// <returns>FT_STATUS value from FT_WriteEE in FTD2XX.DLL</returns>
        /// <param name="address">The EEPROM location to read data from</param>
        /// <param name="eeValue">The WORD value to write to the EEPROM location specified by the Address parameter</param>
        //public FtStatus WriteEEPROMLocation(UInt32 address, UInt16 eeValue)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTWriteEe != IntPtr.Zero)
        //    {
        //        var ftWriteEe = (FtWriteEe)Marshal.GetDelegateForFunctionPointer(_pFTWriteEe, typeof(FtWriteEe));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_WriteEE
        //            ftStatus = ftWriteEe(_ftHandle, address, eeValue);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTWriteEe == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_WriteEE.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // EraseEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Erases the device EEPROM.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EraseEE in FTD2XX.DLL</returns>
        /// <exception cref="FTException">Thrown when attempting to erase the EEPROM of a device with an internal EEPROM such as an FT232R or FT245R.</exception>
        //public FtStatus EraseEEPROM()
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEraseEe != IntPtr.Zero)
        //    {
        //        var ftEraseEe = (FtEraseEe)Marshal.GetDelegateForFunctionPointer(_pFTEraseEe, typeof(FtEraseEe));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is not an FT232R or FT245R that we are trying to erase
        //            GetDeviceType(ref deviceType);
        //            if (deviceType == FtDevice.FtDevice232R) {
        //                // If it is a device with an internal EEPROM, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Call FT_EraseEE
        //            ftStatus = ftEraseEe(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEraseEe == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EraseEE.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadFT232BEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads the EEPROM contents of an FT232B or FT245B device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
        /// <param name="ee232B">An FT232B_EEPROM_STRUCTURE which contains only the relevant information for an FT232B and FT245B device.</param>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus ReadFT232BEEPROM(FT232B_EEPROM_STRUCTURE ee232B)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeRead != IntPtr.Zero)
        //    {
        //        var ftEeRead = (FtEeRead)Marshal.GetDelegateForFunctionPointer(_pFTEeRead, typeof(FtEeRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232B or FT245B that we are trying to read
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDeviceBm) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            var eedata = new FtProgramData {
        //                                               Signature1 = 0x00000000,
        //                                               Signature2 = 0xFFFFFFFF,
        //                                               Version = 2,
        //                                               Manufacturer = Marshal.AllocHGlobal(32),
        //                                               ManufacturerID = Marshal.AllocHGlobal(16),
        //                                               Description = Marshal.AllocHGlobal(64),
        //                                               SerialNumber = Marshal.AllocHGlobal(16)
        //                                           };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Call FT_EE_Read
        //            ftStatus = ftEeRead(_ftHandle, eedata);

        //            // Retrieve string values
        //            ee232B.Manufacturer = Marshal.PtrToStringAnsi(eedata.Manufacturer);
        //            ee232B.ManufacturerID = Marshal.PtrToStringAnsi(eedata.ManufacturerID);
        //            ee232B.Description = Marshal.PtrToStringAnsi(eedata.Description);
        //            ee232B.SerialNumber = Marshal.PtrToStringAnsi(eedata.SerialNumber);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);

        //            // Map non-string elements to structure to be returned
        //            // Standard elements
        //            ee232B.VendorID = eedata.VendorID;
        //            ee232B.ProductID = eedata.ProductID;
        //            ee232B.MaxPower = eedata.MaxPower;
        //            ee232B.SelfPowered = Convert.ToBoolean(eedata.SelfPowered);
        //            ee232B.RemoteWakeup = Convert.ToBoolean(eedata.RemoteWakeup);
        //            // B specific fields
        //            ee232B.PullDownEnable = Convert.ToBoolean(eedata.PullDownEnable);
        //            ee232B.SerNumEnable = Convert.ToBoolean(eedata.SerNumEnable);
        //            ee232B.USBVersionEnable = Convert.ToBoolean(eedata.USBVersionEnable);
        //            ee232B.USBVersion = eedata.USBVersion;
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadFT2232EEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads the EEPROM contents of an FT2232 device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
        /// <param name="ee2232">An FT2232_EEPROM_STRUCTURE which contains only the relevant information for an FT2232 device.</param>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus ReadFT2232EEPROM(FT2232_EEPROM_STRUCTURE ee2232)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeRead != IntPtr.Zero)
        //    {
        //        var ftEeRead = (FtEeRead)Marshal.GetDelegateForFunctionPointer(_pFTEeRead, typeof(FtEeRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT2232 that we are trying to read
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice2232) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 2,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Call FT_EE_Read
        //            ftStatus = ftEeRead(_ftHandle, eedata);

        //            // Retrieve string values
        //            ee2232.Manufacturer = Marshal.PtrToStringAnsi(eedata.Manufacturer);
        //            ee2232.ManufacturerID = Marshal.PtrToStringAnsi(eedata.ManufacturerID);
        //            ee2232.Description = Marshal.PtrToStringAnsi(eedata.Description);
        //            ee2232.SerialNumber = Marshal.PtrToStringAnsi(eedata.SerialNumber);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);

        //            // Map non-string elements to structure to be returned
        //            // Standard elements
        //            ee2232.VendorID = eedata.VendorID;
        //            ee2232.ProductID = eedata.ProductID;
        //            ee2232.MaxPower = eedata.MaxPower;
        //            ee2232.SelfPowered = Convert.ToBoolean(eedata.SelfPowered);
        //            ee2232.RemoteWakeup = Convert.ToBoolean(eedata.RemoteWakeup);
        //            // 2232 specific fields
        //            ee2232.PullDownEnable = Convert.ToBoolean(eedata.PullDownEnable5);
        //            ee2232.SerNumEnable = Convert.ToBoolean(eedata.SerNumEnable5);
        //            ee2232.USBVersionEnable = Convert.ToBoolean(eedata.USBVersionEnable5);
        //            ee2232.USBVersion = eedata.USBVersion5;
        //            ee2232.AIsHighCurrent = Convert.ToBoolean(eedata.AIsHighCurrent);
        //            ee2232.BIsHighCurrent = Convert.ToBoolean(eedata.BIsHighCurrent);
        //            ee2232.IFAIsFifo = Convert.ToBoolean(eedata.IFAIsFifo);
        //            ee2232.IFAIsFifoTar = Convert.ToBoolean(eedata.IFAIsFifoTar);
        //            ee2232.IFAIsFastSer = Convert.ToBoolean(eedata.IFAIsFastSer);
        //            ee2232.AisVCP = Convert.ToBoolean(eedata.AIsVCP);
        //            ee2232.IFBIsFifo = Convert.ToBoolean(eedata.IFBIsFifo);
        //            ee2232.IFBIsFifoTar = Convert.ToBoolean(eedata.IFBIsFifoTar);
        //            ee2232.IFBIsFastSer = Convert.ToBoolean(eedata.IFBIsFastSer);
        //            ee2232.BisVCP = Convert.ToBoolean(eedata.BIsVCP);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadFT232REEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads the EEPROM contents of an FT232R or FT245R device.
        /// Calls FT_EE_Read in FTD2XX DLL
        /// </summary>
        /// <returns>An FT232R_EEPROM_STRUCTURE which contains only the relevant information for an FT232R and FT245R device.</returns>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus ReadFT232REEPROM(FT232R_EEPROM_STRUCTURE ee232R)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeRead != IntPtr.Zero)
        //    {
        //        var ftEeRead = (FtEeRead)Marshal.GetDelegateForFunctionPointer(_pFTEeRead, typeof(FtEeRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232R or FT245R that we are trying to read
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice232R) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 2,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Call FT_EE_Read
        //            ftStatus = ftEeRead(_ftHandle, eedata);

        //            // Retrieve string values
        //            ee232R.Manufacturer = Marshal.PtrToStringAnsi(eedata.Manufacturer);
        //            ee232R.ManufacturerID = Marshal.PtrToStringAnsi(eedata.ManufacturerID);
        //            ee232R.Description = Marshal.PtrToStringAnsi(eedata.Description);
        //            ee232R.SerialNumber = Marshal.PtrToStringAnsi(eedata.SerialNumber);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);

        //            // Map non-string elements to structure to be returned
        //            // Standard elements
        //            ee232R.VendorID = eedata.VendorID;
        //            ee232R.ProductID = eedata.ProductID;
        //            ee232R.MaxPower = eedata.MaxPower;
        //            ee232R.SelfPowered = Convert.ToBoolean(eedata.SelfPowered);
        //            ee232R.RemoteWakeup = Convert.ToBoolean(eedata.RemoteWakeup);
        //            // 232R specific fields
        //            ee232R.UseExtOsc = Convert.ToBoolean(eedata.UseExtOsc);
        //            ee232R.HighDriveIOs = Convert.ToBoolean(eedata.HighDriveIOs);
        //            ee232R.EndpointSize = eedata.EndpointSize;
        //            ee232R.PullDownEnable = Convert.ToBoolean(eedata.PullDownEnableR);
        //            ee232R.SerNumEnable = Convert.ToBoolean(eedata.SerNumEnableR);
        //            ee232R.InvertTXD = Convert.ToBoolean(eedata.InvertTXD);
        //            ee232R.InvertRXD = Convert.ToBoolean(eedata.InvertRXD);
        //            ee232R.InvertRTS = Convert.ToBoolean(eedata.InvertRTS);
        //            ee232R.InvertCTS = Convert.ToBoolean(eedata.InvertCTS);
        //            ee232R.InvertDTR = Convert.ToBoolean(eedata.InvertDTR);
        //            ee232R.InvertDSR = Convert.ToBoolean(eedata.InvertDSR);
        //            ee232R.InvertDCD = Convert.ToBoolean(eedata.InvertDCD);
        //            ee232R.InvertRI = Convert.ToBoolean(eedata.InvertRI);
        //            ee232R.Cbus0 = eedata.Cbus0;
        //            ee232R.Cbus1 = eedata.Cbus1;
        //            ee232R.Cbus2 = eedata.Cbus2;
        //            ee232R.Cbus3 = eedata.Cbus3;
        //            ee232R.Cbus4 = eedata.Cbus4;
        //            ee232R.RisD2XX = Convert.ToBoolean(eedata.RIsD2XX);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadFT2232HEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads the EEPROM contents of an FT2232H device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
        /// <param name="ee2232H">An FT2232H_EEPROM_STRUCTURE which contains only the relevant information for an FT2232H device.</param>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus ReadFT2232HEEPROM(FT2232H_EEPROM_STRUCTURE ee2232H)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeRead != IntPtr.Zero)
        //    {
        //        var ftEeRead = (FtEeRead)Marshal.GetDelegateForFunctionPointer(_pFTEeRead, typeof(FtEeRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT2232H that we are trying to read
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice2232H) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 3,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Call FT_EE_Read
        //            ftStatus = ftEeRead(_ftHandle, eedata);

        //            // Retrieve string values
        //            ee2232H.Manufacturer = Marshal.PtrToStringAnsi(eedata.Manufacturer);
        //            ee2232H.ManufacturerID = Marshal.PtrToStringAnsi(eedata.ManufacturerID);
        //            ee2232H.Description = Marshal.PtrToStringAnsi(eedata.Description);
        //            ee2232H.SerialNumber = Marshal.PtrToStringAnsi(eedata.SerialNumber);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);

        //            // Map non-string elements to structure to be returned
        //            // Standard elements
        //            ee2232H.VendorID = eedata.VendorID;
        //            ee2232H.ProductID = eedata.ProductID;
        //            ee2232H.MaxPower = eedata.MaxPower;
        //            ee2232H.SelfPowered = Convert.ToBoolean(eedata.SelfPowered);
        //            ee2232H.RemoteWakeup = Convert.ToBoolean(eedata.RemoteWakeup);
        //            // 2232H specific fields
        //            ee2232H.PullDownEnable = Convert.ToBoolean(eedata.PullDownEnable7);
        //            ee2232H.SerNumEnable = Convert.ToBoolean(eedata.SerNumEnable7);
        //            ee2232H.ALSlowSlew = Convert.ToBoolean(eedata.ALSlowSlew);
        //            ee2232H.ALSchmittInput = Convert.ToBoolean(eedata.ALSchmittInput);
        //            ee2232H.ALDriveCurrent = eedata.ALDriveCurrent;
        //            ee2232H.AHSlowSlew = Convert.ToBoolean(eedata.AHSlowSlew);
        //            ee2232H.AHSchmittInput = Convert.ToBoolean(eedata.AHSchmittInput);
        //            ee2232H.AHDriveCurrent = eedata.AHDriveCurrent;
        //            ee2232H.BLSlowSlew = Convert.ToBoolean(eedata.BLSlowSlew);
        //            ee2232H.BLSchmittInput = Convert.ToBoolean(eedata.BLSchmittInput);
        //            ee2232H.BLDriveCurrent = eedata.BLDriveCurrent;
        //            ee2232H.BHSlowSlew = Convert.ToBoolean(eedata.BHSlowSlew);
        //            ee2232H.BHSchmittInput = Convert.ToBoolean(eedata.BHSchmittInput);
        //            ee2232H.BHDriveCurrent = eedata.BHDriveCurrent;
        //            ee2232H.IFAIsFifo = Convert.ToBoolean(eedata.IFAIsFifo7);
        //            ee2232H.IFAIsFifoTar = Convert.ToBoolean(eedata.IFAIsFifoTar7);
        //            ee2232H.IFAIsFastSer = Convert.ToBoolean(eedata.IFAIsFastSer7);
        //            ee2232H.AisVCP = Convert.ToBoolean(eedata.AIsVCP7);
        //            ee2232H.IFBIsFifo = Convert.ToBoolean(eedata.IFBIsFifo7);
        //            ee2232H.IFBIsFifoTar = Convert.ToBoolean(eedata.IFBIsFifoTar7);
        //            ee2232H.IFBIsFastSer = Convert.ToBoolean(eedata.IFBIsFastSer7);
        //            ee2232H.BisVCP = Convert.ToBoolean(eedata.BIsVCP7);
        //            ee2232H.PowerSaveEnable = Convert.ToBoolean(eedata.PowerSaveEnable);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadFT4232HEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads the EEPROM contents of an FT4232H device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
        /// <param name="ee4232H">An FT4232H_EEPROM_STRUCTURE which contains only the relevant information for an FT4232H device.</param>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus ReadFT4232HEEPROM(FT4232HEepromStructure ee4232H)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeRead != IntPtr.Zero)
        //    {
        //        var ftEeRead = (FtEeRead)Marshal.GetDelegateForFunctionPointer(_pFTEeRead, typeof(FtEeRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT4232H that we are trying to read
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice4232H) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 4,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Call FT_EE_Read
        //            ftStatus = ftEeRead(_ftHandle, eedata);

        //            // Retrieve string values
        //            ee4232H.Manufacturer = Marshal.PtrToStringAnsi(eedata.Manufacturer);
        //            ee4232H.ManufacturerID = Marshal.PtrToStringAnsi(eedata.ManufacturerID);
        //            ee4232H.Description = Marshal.PtrToStringAnsi(eedata.Description);
        //            ee4232H.SerialNumber = Marshal.PtrToStringAnsi(eedata.SerialNumber);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);

        //            // Map non-string elements to structure to be returned
        //            // Standard elements
        //            ee4232H.VendorID = eedata.VendorID;
        //            ee4232H.ProductID = eedata.ProductID;
        //            ee4232H.MaxPower = eedata.MaxPower;
        //            ee4232H.SelfPowered = Convert.ToBoolean(eedata.SelfPowered);
        //            ee4232H.RemoteWakeup = Convert.ToBoolean(eedata.RemoteWakeup);
        //            // 4232H specific fields
        //            ee4232H.PullDownEnable = Convert.ToBoolean(eedata.PullDownEnable8);
        //            ee4232H.SerNumEnable = Convert.ToBoolean(eedata.SerNumEnable8);
        //            ee4232H.ASlowSlew = Convert.ToBoolean(eedata.ASlowSlew);
        //            ee4232H.ASchmittInput = Convert.ToBoolean(eedata.ASchmittInput);
        //            ee4232H.ADriveCurrent = eedata.ADriveCurrent;
        //            ee4232H.BSlowSlew = Convert.ToBoolean(eedata.BSlowSlew);
        //            ee4232H.BSchmittInput = Convert.ToBoolean(eedata.BSchmittInput);
        //            ee4232H.BDriveCurrent = eedata.BDriveCurrent;
        //            ee4232H.CSlowSlew = Convert.ToBoolean(eedata.CSlowSlew);
        //            ee4232H.CSchmittInput = Convert.ToBoolean(eedata.CSchmittInput);
        //            ee4232H.CDriveCurrent = eedata.CDriveCurrent;
        //            ee4232H.DSlowSlew = Convert.ToBoolean(eedata.DSlowSlew);
        //            ee4232H.DSchmittInput = Convert.ToBoolean(eedata.DSchmittInput);
        //            ee4232H.DDriveCurrent = eedata.DDriveCurrent;
        //            ee4232H.AriisTXDEN = Convert.ToBoolean(eedata.ARIIsTXDEN);
        //            ee4232H.BriisTXDEN = Convert.ToBoolean(eedata.BRIIsTXDEN);
        //            ee4232H.CriisTXDEN = Convert.ToBoolean(eedata.CRIIsTXDEN);
        //            ee4232H.DriisTXDEN = Convert.ToBoolean(eedata.DRIIsTXDEN);
        //            ee4232H.AisVCP = Convert.ToBoolean(eedata.AIsVCP8);
        //            ee4232H.BisVCP = Convert.ToBoolean(eedata.BIsVCP8);
        //            ee4232H.CisVCP = Convert.ToBoolean(eedata.CIsVCP8);
        //            ee4232H.DisVCP = Convert.ToBoolean(eedata.DIsVCP8);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadFT232HEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads the EEPROM contents of an FT232H device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Read in FTD2XX DLL</returns>
        /// <param name="ee232H">An FT232H_EEPROM_STRUCTURE which contains only the relevant information for an FT232H device.</param>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus ReadFT232HEEPROM(FT232H_EEPROM_STRUCTURE ee232H)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeRead != IntPtr.Zero)
        //    {
        //        var ftEeRead = (FtEeRead)Marshal.GetDelegateForFunctionPointer(_pFTEeRead, typeof(FtEeRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232H that we are trying to read
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice232H) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            var eedata = new FtProgramData {
        //                                               Signature1 = 0x00000000,
        //                                               Signature2 = 0xFFFFFFFF,
        //                                               Version = 5,
        //                                               Manufacturer = Marshal.AllocHGlobal(32),
        //                                               ManufacturerID = Marshal.AllocHGlobal(16),
        //                                               Description = Marshal.AllocHGlobal(64),
        //                                               SerialNumber = Marshal.AllocHGlobal(16)
        //                                           };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Call FT_EE_Read
        //            ftStatus = ftEeRead(_ftHandle, eedata);

        //            // Retrieve string values
        //            ee232H.Manufacturer = Marshal.PtrToStringAnsi(eedata.Manufacturer);
        //            ee232H.ManufacturerID = Marshal.PtrToStringAnsi(eedata.ManufacturerID);
        //            ee232H.Description = Marshal.PtrToStringAnsi(eedata.Description);
        //            ee232H.SerialNumber = Marshal.PtrToStringAnsi(eedata.SerialNumber);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);

        //            // Map non-string elements to structure to be returned
        //            // Standard elements
        //            ee232H.VendorID = eedata.VendorID;
        //            ee232H.ProductID = eedata.ProductID;
        //            ee232H.MaxPower = eedata.MaxPower;
        //            ee232H.SelfPowered = Convert.ToBoolean(eedata.SelfPowered);
        //            ee232H.RemoteWakeup = Convert.ToBoolean(eedata.RemoteWakeup);
        //            // 232H specific fields
        //            ee232H.PullDownEnable = Convert.ToBoolean(eedata.PullDownEnableH);
        //            ee232H.SerNumEnable = Convert.ToBoolean(eedata.SerNumEnableH);
        //            ee232H.ACSlowSlew = Convert.ToBoolean(eedata.ACSlowSlewH);
        //            ee232H.ACSchmittInput = Convert.ToBoolean(eedata.ACSchmittInputH);
        //            ee232H.ACDriveCurrent = eedata.ACDriveCurrentH;
        //            ee232H.ADSlowSlew = Convert.ToBoolean(eedata.ADSlowSlewH);
        //            ee232H.ADSchmittInput = Convert.ToBoolean(eedata.ADSchmittInputH);
        //            ee232H.ADDriveCurrent = eedata.ADDriveCurrentH;
        //            ee232H.Cbus0 = eedata.Cbus0H;
        //            ee232H.Cbus1 = eedata.Cbus1H;
        //            ee232H.Cbus2 = eedata.Cbus2H;
        //            ee232H.Cbus3 = eedata.Cbus3H;
        //            ee232H.Cbus4 = eedata.Cbus4H;
        //            ee232H.Cbus5 = eedata.Cbus5H;
        //            ee232H.Cbus6 = eedata.Cbus6H;
        //            ee232H.Cbus7 = eedata.Cbus7H;
        //            ee232H.Cbus8 = eedata.Cbus8H;
        //            ee232H.Cbus9 = eedata.Cbus9H;
        //            ee232H.IsFifo = Convert.ToBoolean(eedata.IsFifoH);
        //            ee232H.IsFifoTar = Convert.ToBoolean(eedata.IsFifoTarH);
        //            ee232H.IsFastSer = Convert.ToBoolean(eedata.IsFastSerH);
        //            ee232H.IsFT1248 = Convert.ToBoolean(eedata.IsFT1248H);
        //            ee232H.FT1248Cpol = Convert.ToBoolean(eedata.FT1248CpolH);
        //            ee232H.FT1248Lsb =  Convert.ToBoolean(eedata.FT1248LsbH);
        //            ee232H.FT1248FlowControl = Convert.ToBoolean(eedata.FT1248FlowControlH);
        //            ee232H.IsVCP = Convert.ToBoolean(eedata.IsVCPH);
        //            ee232H.PowerSaveEnable = Convert.ToBoolean(eedata.PowerSaveEnableH);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // ReadXSeriesEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads the EEPROM contents of an X-Series device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EEPROM_Read in FTD2XX DLL</returns>
        /// <param name="eeX">An FT_XSERIES_EEPROM_STRUCTURE which contains only the relevant information for an X-Series device.</param>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus ReadXSeriesEEPROM(FT_XSERIES_EEPROM_STRUCTURE eeX)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEepromRead != IntPtr.Zero)
        //    {
        //        var ftEEPROMRead = (FtEepromRead)Marshal.GetDelegateForFunctionPointer(_pFTEepromRead, typeof(FtEepromRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232H that we are trying to read
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDeviceXSeries) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            var eeData = new FtXseriesData();
        //            var eeHeader = new FtEepromHeader();

        //            var manufacturer = new byte[32];
        //            var manufacturerID = new byte[16];
        //            var description = new byte[64];
        //            var serialNumber = new byte[16];

        //            eeHeader.deviceType = (uint)FtDevice.FtDeviceXSeries;
        //            eeData.common = eeHeader;

        //            // Calculate the size of our data structure...
        //            var size = Marshal.SizeOf(eeData);

        //            // Allocate space for our pointer...
        //            var eeDataMarshal = Marshal.AllocHGlobal(size);
        //            Marshal.StructureToPtr(eeData, eeDataMarshal, false);
                    
        //            // Call FT_EEPROM_Read
        //            ftStatus = ftEEPROMRead(_ftHandle, eeDataMarshal, (uint)size, manufacturer, manufacturerID, description, serialNumber);

        //            if (ftStatus == FtStatus.FtOk)
        //            {
        //                // Get the data back from the pointer...
        //                eeData = (FtXseriesData)Marshal.PtrToStructure(eeDataMarshal, typeof(FtXseriesData));

        //                // Retrieve string values
        //                var enc = new UTF8Encoding();
        //                eeX.Manufacturer = enc.GetString(manufacturer);
        //                eeX.ManufacturerID = enc.GetString(manufacturerID);
        //                eeX.Description = enc.GetString(description);
        //                eeX.SerialNumber = enc.GetString(serialNumber);
        //                // Map non-string elements to structure to be returned
        //                // Standard elements
        //                eeX.VendorID = eeData.common.VendorId;
        //                eeX.ProductID = eeData.common.ProductId;
        //                eeX.MaxPower = eeData.common.MaxPower;
        //                eeX.SelfPowered = Convert.ToBoolean(eeData.common.SelfPowered);
        //                eeX.RemoteWakeup = Convert.ToBoolean(eeData.common.RemoteWakeup);
        //                eeX.SerNumEnable = Convert.ToBoolean(eeData.common.SerNumEnable);
        //                eeX.PullDownEnable = Convert.ToBoolean(eeData.common.PullDownEnable);
        //                // X-Series specific fields
        //                // CBUS
        //                eeX.Cbus0 = eeData.Cbus0;
        //                eeX.Cbus1 = eeData.Cbus1;
        //                eeX.Cbus2 = eeData.Cbus2;
        //                eeX.Cbus3 = eeData.Cbus3;
        //                eeX.Cbus4 = eeData.Cbus4;
        //                eeX.Cbus5 = eeData.Cbus5;
        //                eeX.Cbus6 = eeData.Cbus6;
        //                // Drive Options
        //                eeX.ACDriveCurrent = eeData.ACDriveCurrent;
        //                eeX.ACSchmittInput = eeData.ACSchmittInput;
        //                eeX.ACSlowSlew = eeData.ACSlowSlew;
        //                eeX.ADDriveCurrent = eeData.ADDriveCurrent;
        //                eeX.ADSchmittInput = eeData.ADSchmittInput;
        //                eeX.ADSlowSlew = eeData.ADSlowSlew;
        //                // BCD
        //                eeX.BCDDisableSleep = eeData.BCDDisableSleep;
        //                eeX.BCDEnable = eeData.BCDEnable;
        //                eeX.BCDForceCbusPWREN = eeData.BCDForceCbusPWREN;
        //                // FT1248
        //                eeX.FT1248Cpol = eeData.FT1248Cpol;
        //                eeX.FT1248FlowControl = eeData.FT1248FlowControl;
        //                eeX.FT1248Lsb = eeData.FT1248Lsb;
        //                // I2C
        //                eeX.I2CDeviceId = eeData.I2CDeviceId;
        //                eeX.I2CDisableSchmitt = eeData.I2CDisableSchmitt;
        //                eeX.I2CSlaveAddress = eeData.I2CSlaveAddress;
        //                // RS232 Signals
        //                eeX.InvertCTS = eeData.InvertCTS;
        //                eeX.InvertDCD = eeData.InvertDCD;
        //                eeX.InvertDSR = eeData.InvertDSR;
        //                eeX.InvertDTR = eeData.InvertDTR;
        //                eeX.InvertRI = eeData.InvertRI;
        //                eeX.InvertRTS = eeData.InvertRTS;
        //                eeX.InvertRXD = eeData.InvertRXD;
        //                eeX.InvertTXD = eeData.InvertTXD;
        //                // Hardware Options
        //                eeX.PowerSaveEnable = eeData.PowerSaveEnable;
        //                eeX.RS485EchoSuppress = eeData.RS485EchoSuppress;
        //                // Driver Option
        //                eeX.IsVCP = eeData.DriverType;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Read.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteFT232BEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes the specified values to the EEPROM of an FT232B or FT245B device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
        /// <param name="ee232B">The EEPROM settings to be written to the device</param>
        /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus WriteFT232BEEPROM(FT232B_EEPROM_STRUCTURE ee232B)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeProgram != IntPtr.Zero)
        //    {
        //        var ftEeProgram = (FtEeProgram)Marshal.GetDelegateForFunctionPointer(_pFTEeProgram, typeof(FtEeProgram));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232B or FT245B that we are trying to write
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDeviceBm) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Check for VID and PID of 0x0000
        //            if ((ee232B.VendorID == 0x0000) | (ee232B.ProductID == 0x0000))
        //            {
        //                // Do not allow users to program the device with VID or PID of 0x0000
        //                return FtStatus.FtInvalidParameter;
        //            }

        //            var eedata = new FtProgramData {
        //                                               Signature1 = 0x00000000,
        //                                               Signature2 = 0xFFFFFFFF,
        //                                               Version = 2,
        //                                               Manufacturer = Marshal.AllocHGlobal(32),
        //                                               ManufacturerID = Marshal.AllocHGlobal(16),
        //                                               Description = Marshal.AllocHGlobal(64),
        //                                               SerialNumber = Marshal.AllocHGlobal(16)
        //                                           };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Check lengths of strings to make sure that they are within our limits
        //            // If not, trim them to make them our maximum length
        //            if (ee232B.Manufacturer.Length > 32)
        //                ee232B.Manufacturer = ee232B.Manufacturer.Substring(0, 32);
        //            if (ee232B.ManufacturerID.Length > 16)
        //                ee232B.ManufacturerID = ee232B.ManufacturerID.Substring(0, 16);
        //            if (ee232B.Description.Length > 64)
        //                ee232B.Description = ee232B.Description.Substring(0, 64);
        //            if (ee232B.SerialNumber.Length > 16)
        //                ee232B.SerialNumber = ee232B.SerialNumber.Substring(0, 16);

        //            // Set string values
        //            eedata.Manufacturer = Marshal.StringToHGlobalAnsi(ee232B.Manufacturer);
        //            eedata.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232B.ManufacturerID);
        //            eedata.Description = Marshal.StringToHGlobalAnsi(ee232B.Description);
        //            eedata.SerialNumber = Marshal.StringToHGlobalAnsi(ee232B.SerialNumber);

        //            // Map non-string elements to structure
        //            // Standard elements
        //            eedata.VendorID = ee232B.VendorID;
        //            eedata.ProductID = ee232B.ProductID;
        //            eedata.MaxPower = ee232B.MaxPower;
        //            eedata.SelfPowered = Convert.ToUInt16(ee232B.SelfPowered);
        //            eedata.RemoteWakeup = Convert.ToUInt16(ee232B.RemoteWakeup);
        //            // B specific fields
        //            eedata.PullDownEnable = Convert.ToByte(ee232B.PullDownEnable);
        //            eedata.SerNumEnable = Convert.ToByte(ee232B.SerNumEnable);
        //            eedata.USBVersionEnable = Convert.ToByte(ee232B.USBVersionEnable);
        //            eedata.USBVersion = ee232B.USBVersion;

        //            // Call FT_EE_Program
        //            ftStatus = ftEeProgram(_ftHandle, eedata);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeProgram == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Program.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteFT2232EEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes the specified values to the EEPROM of an FT2232 device.
        /// Calls FT_EE_Program in FTD2XX DLL
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
        /// <param name="ee2232">The EEPROM settings to be written to the device</param>
        /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus WriteFT2232EEPROM(FT2232_EEPROM_STRUCTURE ee2232)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeProgram != IntPtr.Zero)
        //    {
        //        var ftEeProgram = (FtEeProgram)Marshal.GetDelegateForFunctionPointer(_pFTEeProgram, typeof(FtEeProgram));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT2232 that we are trying to write
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice2232) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Check for VID and PID of 0x0000
        //            if ((ee2232.VendorID == 0x0000) | (ee2232.ProductID == 0x0000))
        //            {
        //                // Do not allow users to program the device with VID or PID of 0x0000
        //                return FtStatus.FtInvalidParameter;
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 2,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Check lengths of strings to make sure that they are within our limits
        //            // If not, trim them to make them our maximum length
        //            if (ee2232.Manufacturer.Length > 32)
        //                ee2232.Manufacturer = ee2232.Manufacturer.Substring(0, 32);
        //            if (ee2232.ManufacturerID.Length > 16)
        //                ee2232.ManufacturerID = ee2232.ManufacturerID.Substring(0, 16);
        //            if (ee2232.Description.Length > 64)
        //                ee2232.Description = ee2232.Description.Substring(0, 64);
        //            if (ee2232.SerialNumber.Length > 16)
        //                ee2232.SerialNumber = ee2232.SerialNumber.Substring(0, 16);

        //            // Set string values
        //            eedata.Manufacturer = Marshal.StringToHGlobalAnsi(ee2232.Manufacturer);
        //            eedata.ManufacturerID = Marshal.StringToHGlobalAnsi(ee2232.ManufacturerID);
        //            eedata.Description = Marshal.StringToHGlobalAnsi(ee2232.Description);
        //            eedata.SerialNumber = Marshal.StringToHGlobalAnsi(ee2232.SerialNumber);

        //            // Map non-string elements to structure
        //            // Standard elements
        //            eedata.VendorID = ee2232.VendorID;
        //            eedata.ProductID = ee2232.ProductID;
        //            eedata.MaxPower = ee2232.MaxPower;
        //            eedata.SelfPowered = Convert.ToUInt16(ee2232.SelfPowered);
        //            eedata.RemoteWakeup = Convert.ToUInt16(ee2232.RemoteWakeup);
        //            // 2232 specific fields
        //            eedata.PullDownEnable5 = Convert.ToByte(ee2232.PullDownEnable);
        //            eedata.SerNumEnable5 = Convert.ToByte(ee2232.SerNumEnable);
        //            eedata.USBVersionEnable5 = Convert.ToByte(ee2232.USBVersionEnable);
        //            eedata.USBVersion5 = ee2232.USBVersion;
        //            eedata.AIsHighCurrent = Convert.ToByte(ee2232.AIsHighCurrent);
        //            eedata.BIsHighCurrent = Convert.ToByte(ee2232.BIsHighCurrent);
        //            eedata.IFAIsFifo = Convert.ToByte(ee2232.IFAIsFifo);
        //            eedata.IFAIsFifoTar = Convert.ToByte(ee2232.IFAIsFifoTar);
        //            eedata.IFAIsFastSer = Convert.ToByte(ee2232.IFAIsFastSer);
        //            eedata.AIsVCP = Convert.ToByte(ee2232.AisVCP);
        //            eedata.IFBIsFifo = Convert.ToByte(ee2232.IFBIsFifo);
        //            eedata.IFBIsFifoTar = Convert.ToByte(ee2232.IFBIsFifoTar);
        //            eedata.IFBIsFastSer = Convert.ToByte(ee2232.IFBIsFastSer);
        //            eedata.BIsVCP = Convert.ToByte(ee2232.BisVCP);

        //            // Call FT_EE_Program
        //            ftStatus = ftEeProgram(_ftHandle, eedata);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeProgram == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Program.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteFT232REEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes the specified values to the EEPROM of an FT232R or FT245R device.
        /// Calls FT_EE_Program in FTD2XX DLL
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
        /// <param name="ee232R">The EEPROM settings to be written to the device</param>
        /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus WriteFT232REEPROM(FT232R_EEPROM_STRUCTURE ee232R)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeProgram != IntPtr.Zero)
        //    {
        //        var ftEeProgram = (FtEeProgram)Marshal.GetDelegateForFunctionPointer(_pFTEeProgram, typeof(FtEeProgram));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232R or FT245R that we are trying to write
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice232R) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Check for VID and PID of 0x0000
        //            if ((ee232R.VendorID == 0x0000) | (ee232R.ProductID == 0x0000))
        //            {
        //                // Do not allow users to program the device with VID or PID of 0x0000
        //                return FtStatus.FtInvalidParameter;
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 2,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Check lengths of strings to make sure that they are within our limits
        //            // If not, trim them to make them our maximum length
        //            if (ee232R.Manufacturer.Length > 32)
        //                ee232R.Manufacturer = ee232R.Manufacturer.Substring(0, 32);
        //            if (ee232R.ManufacturerID.Length > 16)
        //                ee232R.ManufacturerID = ee232R.ManufacturerID.Substring(0, 16);
        //            if (ee232R.Description.Length > 64)
        //                ee232R.Description = ee232R.Description.Substring(0, 64);
        //            if (ee232R.SerialNumber.Length > 16)
        //                ee232R.SerialNumber = ee232R.SerialNumber.Substring(0, 16);

        //            // Set string values
        //            eedata.Manufacturer = Marshal.StringToHGlobalAnsi(ee232R.Manufacturer);
        //            eedata.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232R.ManufacturerID);
        //            eedata.Description = Marshal.StringToHGlobalAnsi(ee232R.Description);
        //            eedata.SerialNumber = Marshal.StringToHGlobalAnsi(ee232R.SerialNumber);

        //            // Map non-string elements to structure
        //            // Standard elements
        //            eedata.VendorID = ee232R.VendorID;
        //            eedata.ProductID = ee232R.ProductID;
        //            eedata.MaxPower = ee232R.MaxPower;
        //            eedata.SelfPowered = Convert.ToUInt16(ee232R.SelfPowered);
        //            eedata.RemoteWakeup = Convert.ToUInt16(ee232R.RemoteWakeup);
        //            // 232R specific fields
        //            eedata.PullDownEnableR = Convert.ToByte(ee232R.PullDownEnable);
        //            eedata.SerNumEnableR = Convert.ToByte(ee232R.SerNumEnable);
        //            eedata.UseExtOsc = Convert.ToByte(ee232R.UseExtOsc);
        //            eedata.HighDriveIOs = Convert.ToByte(ee232R.HighDriveIOs);
        //            // Override any endpoint size the user has selected and force 64 bytes
        //            // Some users have been known to wreck devices by setting 0 here...
        //            eedata.EndpointSize = 64;
        //            eedata.PullDownEnableR = Convert.ToByte(ee232R.PullDownEnable);
        //            eedata.SerNumEnableR = Convert.ToByte(ee232R.SerNumEnable);
        //            eedata.InvertTXD = Convert.ToByte(ee232R.InvertTXD);
        //            eedata.InvertRXD = Convert.ToByte(ee232R.InvertRXD);
        //            eedata.InvertRTS = Convert.ToByte(ee232R.InvertRTS);
        //            eedata.InvertCTS = Convert.ToByte(ee232R.InvertCTS);
        //            eedata.InvertDTR = Convert.ToByte(ee232R.InvertDTR);
        //            eedata.InvertDSR = Convert.ToByte(ee232R.InvertDSR);
        //            eedata.InvertDCD = Convert.ToByte(ee232R.InvertDCD);
        //            eedata.InvertRI = Convert.ToByte(ee232R.InvertRI);
        //            eedata.Cbus0 = ee232R.Cbus0;
        //            eedata.Cbus1 = ee232R.Cbus1;
        //            eedata.Cbus2 = ee232R.Cbus2;
        //            eedata.Cbus3 = ee232R.Cbus3;
        //            eedata.Cbus4 = ee232R.Cbus4;
        //            eedata.RIsD2XX = Convert.ToByte(ee232R.RisD2XX);

        //            // Call FT_EE_Program
        //            ftStatus = ftEeProgram(_ftHandle, eedata);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeProgram == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Program.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteFT2232HEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes the specified values to the EEPROM of an FT2232H device.
        /// Calls FT_EE_Program in FTD2XX DLL
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
        /// <param name="ee2232H">The EEPROM settings to be written to the device</param>
        /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus WriteFT2232HEEPROM(FT2232H_EEPROM_STRUCTURE ee2232H)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeProgram != IntPtr.Zero)
        //    {
        //        var ftEeProgram = (FtEeProgram)Marshal.GetDelegateForFunctionPointer(_pFTEeProgram, typeof(FtEeProgram));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT2232H that we are trying to write
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice2232H) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Check for VID and PID of 0x0000
        //            if ((ee2232H.VendorID == 0x0000) | (ee2232H.ProductID == 0x0000))
        //            {
        //                // Do not allow users to program the device with VID or PID of 0x0000
        //                return FtStatus.FtInvalidParameter;
        //            }

        //            var eedata = new FtProgramData {
        //                                               Signature1 = 0x00000000,
        //                                               Signature2 = 0xFFFFFFFF,
        //                                               Version = 3,
        //                                               Manufacturer = Marshal.AllocHGlobal(32),
        //                                               ManufacturerID = Marshal.AllocHGlobal(16),
        //                                               Description = Marshal.AllocHGlobal(64),
        //                                               SerialNumber = Marshal.AllocHGlobal(16)
        //                                           };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Check lengths of strings to make sure that they are within our limits
        //            // If not, trim them to make them our maximum length
        //            if (ee2232H.Manufacturer.Length > 32)
        //                ee2232H.Manufacturer = ee2232H.Manufacturer.Substring(0, 32);
        //            if (ee2232H.ManufacturerID.Length > 16)
        //                ee2232H.ManufacturerID = ee2232H.ManufacturerID.Substring(0, 16);
        //            if (ee2232H.Description.Length > 64)
        //                ee2232H.Description = ee2232H.Description.Substring(0, 64);
        //            if (ee2232H.SerialNumber.Length > 16)
        //                ee2232H.SerialNumber = ee2232H.SerialNumber.Substring(0, 16);

        //            // Set string values
        //            eedata.Manufacturer = Marshal.StringToHGlobalAnsi(ee2232H.Manufacturer);
        //            eedata.ManufacturerID = Marshal.StringToHGlobalAnsi(ee2232H.ManufacturerID);
        //            eedata.Description = Marshal.StringToHGlobalAnsi(ee2232H.Description);
        //            eedata.SerialNumber = Marshal.StringToHGlobalAnsi(ee2232H.SerialNumber);

        //            // Map non-string elements to structure
        //            // Standard elements
        //            eedata.VendorID = ee2232H.VendorID;
        //            eedata.ProductID = ee2232H.ProductID;
        //            eedata.MaxPower = ee2232H.MaxPower;
        //            eedata.SelfPowered = Convert.ToUInt16(ee2232H.SelfPowered);
        //            eedata.RemoteWakeup = Convert.ToUInt16(ee2232H.RemoteWakeup);
        //            // 2232H specific fields
        //            eedata.PullDownEnable7 = Convert.ToByte(ee2232H.PullDownEnable);
        //            eedata.SerNumEnable7 = Convert.ToByte(ee2232H.SerNumEnable);
        //            eedata.ALSlowSlew = Convert.ToByte(ee2232H.ALSlowSlew);
        //            eedata.ALSchmittInput = Convert.ToByte(ee2232H.ALSchmittInput);
        //            eedata.ALDriveCurrent = ee2232H.ALDriveCurrent;
        //            eedata.AHSlowSlew = Convert.ToByte(ee2232H.AHSlowSlew);
        //            eedata.AHSchmittInput = Convert.ToByte(ee2232H.AHSchmittInput);
        //            eedata.AHDriveCurrent = ee2232H.AHDriveCurrent;
        //            eedata.BLSlowSlew = Convert.ToByte(ee2232H.BLSlowSlew);
        //            eedata.BLSchmittInput = Convert.ToByte(ee2232H.BLSchmittInput);
        //            eedata.BLDriveCurrent = ee2232H.BLDriveCurrent;
        //            eedata.BHSlowSlew = Convert.ToByte(ee2232H.BHSlowSlew);
        //            eedata.BHSchmittInput = Convert.ToByte(ee2232H.BHSchmittInput);
        //            eedata.BHDriveCurrent = ee2232H.BHDriveCurrent;
        //            eedata.IFAIsFifo7 = Convert.ToByte(ee2232H.IFAIsFifo);
        //            eedata.IFAIsFifoTar7 = Convert.ToByte(ee2232H.IFAIsFifoTar);
        //            eedata.IFAIsFastSer7 = Convert.ToByte(ee2232H.IFAIsFastSer);
        //            eedata.AIsVCP7 = Convert.ToByte(ee2232H.AisVCP);
        //            eedata.IFBIsFifo7 = Convert.ToByte(ee2232H.IFBIsFifo);
        //            eedata.IFBIsFifoTar7 = Convert.ToByte(ee2232H.IFBIsFifoTar);
        //            eedata.IFBIsFastSer7 = Convert.ToByte(ee2232H.IFBIsFastSer);
        //            eedata.BIsVCP7 = Convert.ToByte(ee2232H.BisVCP);
        //            eedata.PowerSaveEnable = Convert.ToByte(ee2232H.PowerSaveEnable);

        //            // Call FT_EE_Program
        //            ftStatus = ftEeProgram(_ftHandle, eedata);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeProgram == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Program.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteFT4232HEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes the specified values to the EEPROM of an FT4232H device.
        /// Calls FT_EE_Program in FTD2XX DLL
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
        /// <param name="ee4232H">The EEPROM settings to be written to the device</param>
        /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus WriteFT4232HEEPROM(FT4232HEepromStructure ee4232H)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeProgram != IntPtr.Zero)
        //    {
        //        var ftEeProgram = (FtEeProgram)Marshal.GetDelegateForFunctionPointer(_pFTEeProgram, typeof(FtEeProgram));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            FtDevice deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT4232H that we are trying to write
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice4232H) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Check for VID and PID of 0x0000
        //            if ((ee4232H.VendorID == 0x0000) | (ee4232H.ProductID == 0x0000))
        //            {
        //                // Do not allow users to program the device with VID or PID of 0x0000
        //                return FtStatus.FtInvalidParameter;
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 4,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Check lengths of strings to make sure that they are within our limits
        //            // If not, trim them to make them our maximum length
        //            if (ee4232H.Manufacturer.Length > 32)
        //                ee4232H.Manufacturer = ee4232H.Manufacturer.Substring(0, 32);
        //            if (ee4232H.ManufacturerID.Length > 16)
        //                ee4232H.ManufacturerID = ee4232H.ManufacturerID.Substring(0, 16);
        //            if (ee4232H.Description.Length > 64)
        //                ee4232H.Description = ee4232H.Description.Substring(0, 64);
        //            if (ee4232H.SerialNumber.Length > 16)
        //                ee4232H.SerialNumber = ee4232H.SerialNumber.Substring(0, 16);

        //            // Set string values
        //            eedata.Manufacturer = Marshal.StringToHGlobalAnsi(ee4232H.Manufacturer);
        //            eedata.ManufacturerID = Marshal.StringToHGlobalAnsi(ee4232H.ManufacturerID);
        //            eedata.Description = Marshal.StringToHGlobalAnsi(ee4232H.Description);
        //            eedata.SerialNumber = Marshal.StringToHGlobalAnsi(ee4232H.SerialNumber);

        //            // Map non-string elements to structure
        //            // Standard elements
        //            eedata.VendorID = ee4232H.VendorID;
        //            eedata.ProductID = ee4232H.ProductID;
        //            eedata.MaxPower = ee4232H.MaxPower;
        //            eedata.SelfPowered = Convert.ToUInt16(ee4232H.SelfPowered);
        //            eedata.RemoteWakeup = Convert.ToUInt16(ee4232H.RemoteWakeup);
        //            // 4232H specific fields
        //            eedata.PullDownEnable8 = Convert.ToByte(ee4232H.PullDownEnable);
        //            eedata.SerNumEnable8 = Convert.ToByte(ee4232H.SerNumEnable);
        //            eedata.ASlowSlew = Convert.ToByte(ee4232H.ASlowSlew);
        //            eedata.ASchmittInput = Convert.ToByte(ee4232H.ASchmittInput);
        //            eedata.ADriveCurrent = ee4232H.ADriveCurrent;
        //            eedata.BSlowSlew = Convert.ToByte(ee4232H.BSlowSlew);
        //            eedata.BSchmittInput = Convert.ToByte(ee4232H.BSchmittInput);
        //            eedata.BDriveCurrent = ee4232H.BDriveCurrent;
        //            eedata.CSlowSlew = Convert.ToByte(ee4232H.CSlowSlew);
        //            eedata.CSchmittInput = Convert.ToByte(ee4232H.CSchmittInput);
        //            eedata.CDriveCurrent = ee4232H.CDriveCurrent;
        //            eedata.DSlowSlew = Convert.ToByte(ee4232H.DSlowSlew);
        //            eedata.DSchmittInput = Convert.ToByte(ee4232H.DSchmittInput);
        //            eedata.DDriveCurrent = ee4232H.DDriveCurrent;
        //            eedata.ARIIsTXDEN = Convert.ToByte(ee4232H.AriisTXDEN);
        //            eedata.BRIIsTXDEN = Convert.ToByte(ee4232H.BriisTXDEN);
        //            eedata.CRIIsTXDEN = Convert.ToByte(ee4232H.CriisTXDEN);
        //            eedata.DRIIsTXDEN = Convert.ToByte(ee4232H.DriisTXDEN);
        //            eedata.AIsVCP8 = Convert.ToByte(ee4232H.AisVCP);
        //            eedata.BIsVCP8 = Convert.ToByte(ee4232H.BisVCP);
        //            eedata.CIsVCP8 = Convert.ToByte(ee4232H.CisVCP);
        //            eedata.DIsVCP8 = Convert.ToByte(ee4232H.DisVCP);

        //            // Call FT_EE_Program
        //            ftStatus = ftEeProgram(_ftHandle, eedata);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeProgram == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Program.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteFT232HEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes the specified values to the EEPROM of an FT232H device.
        /// Calls FT_EE_Program in FTD2XX DLL
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_Program in FTD2XX DLL</returns>
        /// <param name="ee232H">The EEPROM settings to be written to the device</param>
        /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus WriteFT232HEEPROM(FT232H_EEPROM_STRUCTURE ee232H)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeProgram != IntPtr.Zero)
        //    {
        //        var ftEeProgram = (FtEeProgram)Marshal.GetDelegateForFunctionPointer(_pFTEeProgram, typeof(FtEeProgram));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232H that we are trying to write
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDevice232H) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Check for VID and PID of 0x0000
        //            if ((ee232H.VendorID == 0x0000) | (ee232H.ProductID == 0x0000))
        //            {
        //                // Do not allow users to program the device with VID or PID of 0x0000
        //                return FtStatus.FtInvalidParameter;
        //            }

        //            var eedata = new FtProgramData {
        //                                                         Signature1 = 0x00000000,
        //                                                         Signature2 = 0xFFFFFFFF,
        //                                                         Version = 5,
        //                                                         Manufacturer =
        //                                                             Marshal.AllocHGlobal(32),
        //                                                         ManufacturerID =
        //                                                             Marshal.AllocHGlobal(16),
        //                                                         Description =
        //                                                             Marshal.AllocHGlobal(64),
        //                                                         SerialNumber =
        //                                                             Marshal.AllocHGlobal(16)
        //                                                     };

        //            // Set up structure headers

        //            // Allocate space from unmanaged heap

        //            // Check lengths of strings to make sure that they are within our limits
        //            // If not, trim them to make them our maximum length
        //            if (ee232H.Manufacturer.Length > 32)
        //                ee232H.Manufacturer = ee232H.Manufacturer.Substring(0, 32);
        //            if (ee232H.ManufacturerID.Length > 16)
        //                ee232H.ManufacturerID = ee232H.ManufacturerID.Substring(0, 16);
        //            if (ee232H.Description.Length > 64)
        //                ee232H.Description = ee232H.Description.Substring(0, 64);
        //            if (ee232H.SerialNumber.Length > 16)
        //                ee232H.SerialNumber = ee232H.SerialNumber.Substring(0, 16);

        //            // Set string values
        //            eedata.Manufacturer = Marshal.StringToHGlobalAnsi(ee232H.Manufacturer);
        //            eedata.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232H.ManufacturerID);
        //            eedata.Description = Marshal.StringToHGlobalAnsi(ee232H.Description);
        //            eedata.SerialNumber = Marshal.StringToHGlobalAnsi(ee232H.SerialNumber);

        //            // Map non-string elements to structure
        //            // Standard elements
        //            eedata.VendorID = ee232H.VendorID;
        //            eedata.ProductID = ee232H.ProductID;
        //            eedata.MaxPower = ee232H.MaxPower;
        //            eedata.SelfPowered = Convert.ToUInt16(ee232H.SelfPowered);
        //            eedata.RemoteWakeup = Convert.ToUInt16(ee232H.RemoteWakeup);
        //            // 232H specific fields
        //            eedata.PullDownEnableH = Convert.ToByte(ee232H.PullDownEnable);
        //            eedata.SerNumEnableH = Convert.ToByte(ee232H.SerNumEnable);
        //            eedata.ACSlowSlewH = Convert.ToByte(ee232H.ACSlowSlew);
        //            eedata.ACSchmittInputH = Convert.ToByte(ee232H.ACSchmittInput);
        //            eedata.ACDriveCurrentH = Convert.ToByte(ee232H.ACDriveCurrent);
        //            eedata.ADSlowSlewH = Convert.ToByte(ee232H.ADSlowSlew);
        //            eedata.ADSchmittInputH = Convert.ToByte(ee232H.ADSchmittInput);
        //            eedata.ADDriveCurrentH = Convert.ToByte(ee232H.ADDriveCurrent);
        //            eedata.Cbus0H = Convert.ToByte(ee232H.Cbus0);
        //            eedata.Cbus1H = Convert.ToByte(ee232H.Cbus1);
        //            eedata.Cbus2H = Convert.ToByte(ee232H.Cbus2);
        //            eedata.Cbus3H = Convert.ToByte(ee232H.Cbus3);
        //            eedata.Cbus4H = Convert.ToByte(ee232H.Cbus4);
        //            eedata.Cbus5H = Convert.ToByte(ee232H.Cbus5);
        //            eedata.Cbus6H = Convert.ToByte(ee232H.Cbus6);
        //            eedata.Cbus7H = Convert.ToByte(ee232H.Cbus7);
        //            eedata.Cbus8H = Convert.ToByte(ee232H.Cbus8);
        //            eedata.Cbus9H = Convert.ToByte(ee232H.Cbus9);
        //            eedata.IsFifoH = Convert.ToByte(ee232H.IsFifo);
        //            eedata.IsFifoTarH = Convert.ToByte(ee232H.IsFifoTar);
        //            eedata.IsFastSerH = Convert.ToByte(ee232H.IsFastSer);
        //            eedata.IsFT1248H = Convert.ToByte(ee232H.IsFT1248);
        //            eedata.FT1248CpolH = Convert.ToByte(ee232H.FT1248Cpol);
        //            eedata.FT1248LsbH = Convert.ToByte(ee232H.FT1248Lsb);
        //            eedata.FT1248FlowControlH = Convert.ToByte(ee232H.FT1248FlowControl);
        //            eedata.IsVCPH = Convert.ToByte(ee232H.IsVCP);
        //            eedata.PowerSaveEnableH = Convert.ToByte(ee232H.PowerSaveEnable);

        //            // Call FT_EE_Program
        //            ftStatus = ftEeProgram(_ftHandle, eedata);

        //            // Free unmanaged buffers
        //            Marshal.FreeHGlobal(eedata.Manufacturer);
        //            Marshal.FreeHGlobal(eedata.ManufacturerID);
        //            Marshal.FreeHGlobal(eedata.Description);
        //            Marshal.FreeHGlobal(eedata.SerialNumber);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeProgram == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_Program.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // WriteXSeriesEEPROM
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes the specified values to the EEPROM of an X-Series device.
        /// Calls FT_EEPROM_Program in FTD2XX DLL
        /// </summary>
        /// <returns>FT_STATUS value from FT_EEPROM_Program in FTD2XX DLL</returns>
        /// <param name="eeX">The EEPROM settings to be written to the device</param>
        /// <remarks>If the strings are too long, they will be truncated to their maximum permitted lengths</remarks>
        /// <exception cref="FTException">Thrown when the current device does not match the type required by this method.</exception>
        //public FtStatus WriteXSeriesEEPROM(FT_XSERIES_EEPROM_STRUCTURE eeX)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    const FtStatus ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEepromProgram != IntPtr.Zero) 
        //    {
        //        var ftEEPROMProgram = (FtEepromProgram)Marshal.GetDelegateForFunctionPointer(_pFTEepromProgram, typeof(FtEepromProgram));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Check that it is an FT232H that we are trying to write
        //            GetDeviceType(ref deviceType);
        //            if (deviceType != FtDevice.FtDeviceXSeries) {
        //                // If it is not, throw an exception
        //                const FtError ftErrorCondition = FtError.FtIncorrectDevice;
        //                ErrorHandler(ftStatus, ftErrorCondition);
        //            }

        //            // Check for VID and PID of 0x0000
        //            if ((eeX.VendorID == 0x0000) | (eeX.ProductID == 0x0000))
        //            {
        //                // Do not allow users to program the device with VID or PID of 0x0000
        //                return FtStatus.FtInvalidParameter;
        //            }

        //            var eeData = new FtXseriesData();

        //            // String manipulation...
        //            // Allocate space from unmanaged heap

        //            // Check lengths of strings to make sure that they are within our limits
        //            // If not, trim them to make them our maximum length
        //            if (eeX.Manufacturer.Length > 32)
        //                eeX.Manufacturer = eeX.Manufacturer.Substring(0, 32);
        //            if (eeX.ManufacturerID.Length > 16)
        //                eeX.ManufacturerID = eeX.ManufacturerID.Substring(0, 16);
        //            if (eeX.Description.Length > 64)
        //                eeX.Description = eeX.Description.Substring(0, 64);
        //            if (eeX.SerialNumber.Length > 16)
        //                eeX.SerialNumber = eeX.SerialNumber.Substring(0, 16);

        //            // Set string values
        //            var encoding = new UTF8Encoding();
        //            var manufacturer = encoding.GetBytes(eeX.Manufacturer);
        //            var manufacturerID = encoding.GetBytes(eeX.ManufacturerID);
        //            var description = encoding.GetBytes(eeX.Description);
        //            var serialNumber = encoding.GetBytes(eeX.SerialNumber);

        //            // Map non-string elements to structure to be returned
        //            // Standard elements
        //            eeData.common.deviceType = (uint)FtDevice.FtDeviceXSeries;
        //            eeData.common.VendorId = eeX.VendorID;
        //            eeData.common.ProductId = eeX.ProductID;
        //            eeData.common.MaxPower = eeX.MaxPower;
        //            eeData.common.SelfPowered = Convert.ToByte(eeX.SelfPowered);
        //            eeData.common.RemoteWakeup = Convert.ToByte(eeX.RemoteWakeup);
        //            eeData.common.SerNumEnable = Convert.ToByte(eeX.SerNumEnable);
        //            eeData.common.PullDownEnable = Convert.ToByte(eeX.PullDownEnable);
        //            // X-Series specific fields
        //            // CBUS
        //            eeData.Cbus0 = eeX.Cbus0;
        //            eeData.Cbus1 = eeX.Cbus1;
        //            eeData.Cbus2 = eeX.Cbus2;
        //            eeData.Cbus3 = eeX.Cbus3;
        //            eeData.Cbus4 = eeX.Cbus4;
        //            eeData.Cbus5 = eeX.Cbus5;
        //            eeData.Cbus6 = eeX.Cbus6;
        //            // Drive Options
        //            eeData.ACDriveCurrent = eeX.ACDriveCurrent;
        //            eeData.ACSchmittInput = eeX.ACSchmittInput;
        //            eeData.ACSlowSlew = eeX.ACSlowSlew;
        //            eeData.ADDriveCurrent = eeX.ADDriveCurrent;
        //            eeData.ADSchmittInput = eeX.ADSchmittInput;
        //            eeData.ADSlowSlew = eeX.ADSlowSlew;
        //            // BCD
        //            eeData.BCDDisableSleep = eeX.BCDDisableSleep;
        //            eeData.BCDEnable = eeX.BCDEnable;
        //            eeData.BCDForceCbusPWREN = eeX.BCDForceCbusPWREN;
        //            // FT1248
        //            eeData.FT1248Cpol = eeX.FT1248Cpol;
        //            eeData.FT1248FlowControl = eeX.FT1248FlowControl;
        //            eeData.FT1248Lsb = eeX.FT1248Lsb;
        //            // I2C
        //            eeData.I2CDeviceId = eeX.I2CDeviceId;
        //            eeData.I2CDisableSchmitt = eeX.I2CDisableSchmitt;
        //            eeData.I2CSlaveAddress = eeX.I2CSlaveAddress;
        //            // RS232 Signals
        //            eeData.InvertCTS = eeX.InvertCTS;
        //            eeData.InvertDCD = eeX.InvertDCD;
        //            eeData.InvertDSR = eeX.InvertDSR;
        //            eeData.InvertDTR = eeX.InvertDTR;
        //            eeData.InvertRI = eeX.InvertRI;
        //            eeData.InvertRTS = eeX.InvertRTS;
        //            eeData.InvertRXD = eeX.InvertRXD;
        //            eeData.InvertTXD = eeX.InvertTXD;
        //            // Hardware Options
        //            eeData.PowerSaveEnable = eeX.PowerSaveEnable;
        //            eeData.RS485EchoSuppress = eeX.RS485EchoSuppress;
        //            // Driver Option
        //            eeData.DriverType = eeX.IsVCP;

        //            // Check the size of the structure...
        //            var size = Marshal.SizeOf(eeData);
        //            // Allocate space for our pointer...
        //            var eeDataMarshal = Marshal.AllocHGlobal(size);
        //            Marshal.StructureToPtr(eeData, eeDataMarshal, false);

        //            return ftEEPROMProgram(_ftHandle, eeDataMarshal, (uint)size, manufacturer, manufacturerID, description, serialNumber);
        //        }
        //    }

        //    return FtStatus.FtDeviceNotFound;
        //}

        //**************************************************************************
        // EEReadUserArea
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Reads data from the user area of the device EEPROM.
        /// </summary>
        /// <returns>FT_STATUS from FT_UARead in FTD2XX.DLL</returns>
        /// <param name="userAreaDataBuffer">An array of bytes which will be populated with the data read from the device EEPROM user area.</param>
        /// <param name="numBytesRead">The number of bytes actually read from the EEPROM user area.</param>
        //public FtStatus EEReadUserArea(byte[] userAreaDataBuffer, ref UInt32 numBytesRead)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTEeUaSize != IntPtr.Zero) & (_pFTEeUaRead != IntPtr.Zero))
        //    {
        //        var ftEEUaSize = (FtEeUaSize)Marshal.GetDelegateForFunctionPointer(_pFTEeUaSize, typeof(FtEeUaSize));
        //        var ftEEUaRead = (FtEeUaRead)Marshal.GetDelegateForFunctionPointer(_pFTEeUaRead, typeof(FtEeUaRead));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            UInt32 uaSize = 0;
        //            // Get size of user area to allocate an array of the correct size.
        //            // The application must also get the UA size for its copy
        //            ftStatus = ftEEUaSize(_ftHandle, ref uaSize);

        //            // Make sure we have enough storage for the whole user area
        //            if (userAreaDataBuffer.Length >= uaSize)
        //            {
        //                // Call FT_EE_UARead
        //                ftStatus = ftEEUaRead(_ftHandle, userAreaDataBuffer, userAreaDataBuffer.Length, ref numBytesRead);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeUaSize == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_UASize.");
        //        }
        //        if (_pFTEeUaRead == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_UARead.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // EEWriteUserArea
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Writes data to the user area of the device EEPROM.
        /// </summary>
        /// <returns>FT_STATUS value from FT_UAWrite in FTD2XX.DLL</returns>
        /// <param name="userAreaDataBuffer">An array of bytes which will be written to the device EEPROM user area.</param>
        //public FtStatus EEWriteUserArea(byte[] userAreaDataBuffer)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTEeUaSize != IntPtr.Zero) & (_pFTEeUaWrite != IntPtr.Zero))
        //    {
        //        var ftEEUaSize = (FtEeUaSize)Marshal.GetDelegateForFunctionPointer(_pFTEeUaSize, typeof(FtEeUaSize));
        //        var ftEEUaWrite = (FtEeUaWrite)Marshal.GetDelegateForFunctionPointer(_pFTEeUaWrite, typeof(FtEeUaWrite));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            UInt32 uaSize = 0;
        //            // Get size of user area to allocate an array of the correct size.
        //            // The application must also get the UA size for its copy
        //            ftStatus = ftEEUaSize(_ftHandle, ref uaSize);

        //            // Make sure we have enough storage for all the data in the EEPROM
        //            if (userAreaDataBuffer.Length <= uaSize)
        //            {
        //                // Call FT_EE_UAWrite
        //                ftStatus = ftEEUaWrite(_ftHandle, userAreaDataBuffer, userAreaDataBuffer.Length);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeUaSize == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_UASize.");
        //        }
        //        if (_pFTEeUaWrite == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_UAWrite.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetDeviceType
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the chip type of the current device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
        /// <param name="deviceType">The FTDI chip type of the current device.</param>
        //private void GetDeviceType(ref FtDevice deviceType)
        //{
        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetDeviceInfo != IntPtr.Zero)
        //    {
        //        FtGetDeviceInfo ftGetDeviceInfo = (FtGetDeviceInfo)Marshal.GetDelegateForFunctionPointer(_pFTGetDeviceInfo, typeof(FtGetDeviceInfo));

        //        UInt32 deviceID = 0;
        //        var sernum = new byte[16];
        //        var desc = new byte[64];

        //        deviceType = FtDevice.FtDeviceUnknown;

        //        if (_ftHandle != IntPtr.Zero && ftGetDeviceInfo != null) {
        //            ftGetDeviceInfo(_ftHandle,
        //                            ref deviceType,
        //                            ref deviceID,
        //                            sernum,
        //                            desc,
        //                            IntPtr.Zero);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetDeviceInfo == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
        //        }
        //    }
        //}
        
        //**************************************************************************
        // GetDeviceID
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the Vendor ID and Product ID of the current device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
        /// <param name="deviceID">The device ID (Vendor ID and Product ID) of the current device.</param>
        //public FtStatus GetDeviceID(ref UInt32 deviceID)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetDeviceInfo != IntPtr.Zero)
        //    {
        //        var ftGetDeviceInfo = (FtGetDeviceInfo)Marshal.GetDelegateForFunctionPointer(_pFTGetDeviceInfo, typeof(FtGetDeviceInfo));

        //        var deviceType = FtDevice.FtDeviceUnknown;
        //        var sernum = new byte[16];
        //        var desc = new byte[64];

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetDeviceInfo
        //            ftStatus = ftGetDeviceInfo(_ftHandle, ref deviceType, ref deviceID, sernum, desc, IntPtr.Zero);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetDeviceInfo == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetDescription
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the description of the current device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
        /// <param name="description">The description of the current device.</param>
        //private void GetDescription(out string description)
        //{
        //    description = String.Empty;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return;


        //    // Check for our required function pointers being set up
        //    if (_pFTGetDeviceInfo != IntPtr.Zero)
        //    {
        //        var ftGetDeviceInfo = (FtGetDeviceInfo)Marshal.GetDelegateForFunctionPointer(_pFTGetDeviceInfo, typeof(FtGetDeviceInfo));

        //        UInt32 deviceID = 0;
        //        var deviceType = FtDevice.FtDeviceUnknown;
        //        var sernum = new byte[16];
        //        var desc = new byte[64];

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetDeviceInfo
        //            if (ftGetDeviceInfo != null) {
        //                ftGetDeviceInfo(_ftHandle, ref deviceType, ref deviceID, sernum, desc, IntPtr.Zero);
        //            }
        //            description = Encoding.ASCII.GetString(desc);
        //            description = description.Substring(0, description.IndexOf("\0", StringComparison.Ordinal));
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetDeviceInfo == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
        //        }
        //    }
        //}

        //**************************************************************************
        // GetSerialNumber
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the serial number of the current device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetDeviceInfo in FTD2XX.DLL</returns>
        /// <param name="serialNumber">The serial number of the current device.</param>
        //public FtStatus GetSerialNumber(out string serialNumber)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    serialNumber = String.Empty;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;


        //    // Check for our required function pointers being set up
        //    if (_pFTGetDeviceInfo != IntPtr.Zero)
        //    {
        //        var ftGetDeviceInfo = (FtGetDeviceInfo)Marshal.GetDelegateForFunctionPointer(_pFTGetDeviceInfo, typeof(FtGetDeviceInfo));

        //        UInt32 deviceID = 0;
        //        var deviceType = FtDevice.FtDeviceUnknown;
        //        var sernum = new byte[16];
        //        var desc = new byte[64];

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetDeviceInfo
        //            ftStatus = ftGetDeviceInfo(_ftHandle, ref deviceType, ref deviceID, sernum, desc, IntPtr.Zero);
        //            serialNumber = Encoding.ASCII.GetString(sernum);
        //            serialNumber = serialNumber.Substring(0, serialNumber.IndexOf("\0", StringComparison.Ordinal));
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetDeviceInfo == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetRxBytesAvailable
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the number of bytes available in the receive buffer.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetQueueStatus in FTD2XX.DLL</returns>
        /// <param name="rxQueue">The number of bytes available to be read.</param>
        //public FtStatus GetRxBytesAvailable(ref UInt32 rxQueue)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    FtStatus ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetQueueStatus != IntPtr.Zero)
        //    {
        //        var ftGetQueueStatus = (FtGetQueueStatus)Marshal.GetDelegateForFunctionPointer(_pFTGetQueueStatus, typeof(FtGetQueueStatus));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetQueueStatus
        //            ftStatus = ftGetQueueStatus(_ftHandle, ref rxQueue);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetQueueStatus == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetQueueStatus.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetTxBytesWaiting
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the number of bytes waiting in the transmit buffer.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetStatus in FTD2XX.DLL</returns>
        /// <param name="txQueue">The number of bytes waiting to be sent.</param>
        //public FtStatus GetTxBytesWaiting(ref UInt32 txQueue)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    FtStatus ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetStatus != IntPtr.Zero)
        //    {
        //        var ftGetStatus = (FtGetStatus)Marshal.GetDelegateForFunctionPointer(_pFTGetStatus, typeof(FtGetStatus));

        //        UInt32 rxQueue = 0;
        //        UInt32 eventStatus = 0;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetStatus
        //            ftStatus = ftGetStatus(_ftHandle, ref rxQueue, ref txQueue, ref eventStatus);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetStatus == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetStatus.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetEventType
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the event type after an event has fired.  Can be used to distinguish which event has been triggered when waiting on multiple event types.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetStatus in FTD2XX.DLL</returns>
        /// <param name="eventType">The type of event that has occurred.</param>
        //public FtStatus GetEventType(ref UInt32 eventType)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetStatus != IntPtr.Zero)
        //    {
        //        var ftGetStatus = (FtGetStatus)Marshal.GetDelegateForFunctionPointer(_pFTGetStatus, typeof(FtGetStatus));

        //        UInt32 rxQueue = 0;
        //        UInt32 txQueue = 0;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetStatus
        //            ftStatus = ftGetStatus(_ftHandle, ref rxQueue, ref txQueue, ref eventType);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetStatus == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetStatus.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetModemStatus
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the current modem status.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetModemStatus in FTD2XX.DLL</returns>
        /// <param name="modemStatus">A bit map representaion of the current modem status.</param>
        //public FtStatus GetModemStatus(ref byte modemStatus)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetModemStatus != IntPtr.Zero)
        //    {
        //        var ftGetModemStatus = (FtGetModemStatus)Marshal.GetDelegateForFunctionPointer(_pFTGetModemStatus, typeof(FtGetModemStatus));

        //        UInt32 modemLineStatus = 0;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetModemStatus
        //            ftStatus = ftGetModemStatus(_ftHandle, ref modemLineStatus);

        //        }
        //        modemStatus = Convert.ToByte(modemLineStatus & 0x000000FF);
        //    }
        //    else
        //    {
        //        if (_pFTGetModemStatus == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetModemStatus.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetLineStatus
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the current line status.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetModemStatus in FTD2XX.DLL</returns>
        /// <param name="lineStatus">A bit map representaion of the current line status.</param>
        //public FtStatus GetLineStatus(ref byte lineStatus)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetModemStatus != IntPtr.Zero)
        //    {
        //        var ftGetModemStatus = (FtGetModemStatus)Marshal.GetDelegateForFunctionPointer(_pFTGetModemStatus, typeof(FtGetModemStatus));

        //        UInt32 modemLineStatus = 0;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetModemStatus
        //            ftStatus = ftGetModemStatus(_ftHandle, ref modemLineStatus);
        //        }
        //        lineStatus = Convert.ToByte((modemLineStatus >> 8) & 0x000000FF);
        //    }
        //    else
        //    {
        //        if (_pFTGetModemStatus == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetModemStatus.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetBaudRate
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets the current Baud rate.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetBaudRate in FTD2XX.DLL</returns>
        /// <param name="baudRate">The desired Baud rate for the device.</param>
        //public FtStatus SetBaudRate(UInt32 baudRate)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetBaudRate != IntPtr.Zero)
        //    {
        //        var ftSetBaudRate = (FtSetBaudRate)Marshal.GetDelegateForFunctionPointer(_pFTSetBaudRate, typeof(FtSetBaudRate));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetBaudRate
        //            ftStatus = ftSetBaudRate(_ftHandle, baudRate);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetBaudRate == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBaudRate.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetDataCharacteristics
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets the data bits, stop bits and parity for the device.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetDataCharacteristics in FTD2XX.DLL</returns>
        /// <param name="dataBits">The number of data bits for UART data.  Valid values are FT_DATA_BITS.FT_DATA_7 or FT_DATA_BITS.FT_BITS_8</param>
        /// <param name="stopBits">The number of stop bits for UART data.  Valid values are FT_STOP_BITS.FT_STOP_BITS_1 or FT_STOP_BITS.FT_STOP_BITS_2</param>
        /// <param name="parity">The parity of the UART data.  Valid values are FT_PARITY.FT_PARITY_NONE, FT_PARITY.FT_PARITY_ODD, FT_PARITY.FT_PARITY_EVEN, FT_PARITY.FT_PARITY_MARK or FT_PARITY.FT_PARITY_SPACE</param>
        //public FtStatus SetDataCharacteristics(byte dataBits, byte stopBits, byte parity)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetDataCharacteristics != IntPtr.Zero)
        //    {
        //        var ftSetDataCharacteristics = (FtSetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(_pFTSetDataCharacteristics, typeof(FtSetDataCharacteristics));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetDataCharacteristics
        //            ftStatus = ftSetDataCharacteristics(_ftHandle, dataBits, stopBits, parity);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetDataCharacteristics == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetFlowControl
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets the flow control type.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetFlowControl in FTD2XX.DLL</returns>
        /// <param name="flowControl">The type of flow control for the UART.  Valid values are FT_FLOW_CONTROL.FT_FLOW_NONE, FT_FLOW_CONTROL.FT_FLOW_RTS_CTS, FT_FLOW_CONTROL.FT_FLOW_DTR_DSR or FT_FLOW_CONTROL.FT_FLOW_XON_XOFF</param>
        /// <param name="xon">The Xon character for Xon/Xoff flow control.  Ignored if not using Xon/XOff flow control.</param>
        /// <param name="xoff">The Xoff character for Xon/Xoff flow control.  Ignored if not using Xon/XOff flow control.</param>
        //public FtStatus SetFlowControl(UInt16 flowControl, byte xon, byte xoff)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetFlowControl != IntPtr.Zero)
        //    {
        //        var ftSetFlowControl = (FtSetFlowControl)Marshal.GetDelegateForFunctionPointer(_pFTSetFlowControl, typeof(FtSetFlowControl));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetFlowControl
        //            ftStatus = ftSetFlowControl(_ftHandle, flowControl, xon, xoff);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetFlowControl == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetFlowControl.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetRTS
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Asserts or de-asserts the Request To Send (RTS) line.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetRts or FT_ClrRts in FTD2XX.DLL</returns>
        /// <param name="enable">If true, asserts RTS.  If false, de-asserts RTS</param>
        //public FtStatus SetRTS(bool enable)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTSetRts != IntPtr.Zero) & (_pFTClrRts != IntPtr.Zero))
        //    {
        //        var ftSetRts = (FtSetRts)Marshal.GetDelegateForFunctionPointer(_pFTSetRts, typeof(FtSetRts));
        //        var ftClrRts = (FtClrRts)Marshal.GetDelegateForFunctionPointer(_pFTClrRts, typeof(FtClrRts));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            ftStatus = enable ? ftSetRts(_ftHandle) : ftClrRts(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetRts == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetRts.");
        //        }
        //        if (_pFTClrRts == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_ClrRts.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetDTR
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Asserts or de-asserts the Data Terminal Ready (DTR) line.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetDtr or FT_ClrDtr in FTD2XX.DLL</returns>
        /// <param name="enable">If true, asserts DTR.  If false, de-asserts DTR.</param>
        //public FtStatus SetDTR(bool enable)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTSetDtr != IntPtr.Zero) & (_pFTClrDtr != IntPtr.Zero))
        //    {
        //        var ftSetDtr = (FtSetDtr)Marshal.GetDelegateForFunctionPointer(_pFTSetDtr, typeof(FtSetDtr));
        //        var ftClrDtr = (FtClrDtr)Marshal.GetDelegateForFunctionPointer(_pFTClrDtr, typeof(FtClrDtr));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            ftStatus = enable ? ftSetDtr(_ftHandle) : ftClrDtr(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetDtr == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetDtr.");
        //        }
        //        if (_pFTClrDtr == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_ClrDtr.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetTimeouts
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets the read and write timeout values.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetTimeouts in FTD2XX.DLL</returns>
        /// <param name="readTimeout">Read timeout value in ms.  A value of 0 indicates an infinite timeout.</param>
        /// <param name="writeTimeout">Write timeout value in ms.  A value of 0 indicates an infinite timeout.</param>
        //public FtStatus SetTimeouts(UInt32 readTimeout, UInt32 writeTimeout)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetTimeouts != IntPtr.Zero)
        //    {
        //        var ftSetTimeouts = (FtSetTimeouts)Marshal.GetDelegateForFunctionPointer(_pFTSetTimeouts, typeof(FtSetTimeouts));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetTimeouts
        //            ftStatus = ftSetTimeouts(_ftHandle, readTimeout, writeTimeout);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetTimeouts == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetTimeouts.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetBreak
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets or clears the break state.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetBreakOn or FT_SetBreakOff in FTD2XX.DLL</returns>
        /// <param name="enable">If true, sets break on.  If false, sets break off.</param>
        //public FtStatus SetBreak(bool enable)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if ((_pFTSetBreakOn != IntPtr.Zero) & (_pFTSetBreakOff != IntPtr.Zero))
        //    {
        //        var ftSetBreakOn = (FtSetBreakOn)Marshal.GetDelegateForFunctionPointer(_pFTSetBreakOn, typeof(FtSetBreakOn));
        //        var ftSetBreakOff = (FtSetBreakOff)Marshal.GetDelegateForFunctionPointer(_pFTSetBreakOff, typeof(FtSetBreakOff));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            ftStatus = enable ? ftSetBreakOn(_ftHandle) : ftSetBreakOff(_ftHandle);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetBreakOn == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBreakOn.");
        //        }
        //        if (_pFTSetBreakOff == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetBreakOff.");
        //        }
        //    }
        //    return ftStatus;
        //}
        
        //**************************************************************************
        // SetResetPipeRetryCount
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets or sets the reset pipe retry count.  Default value is 50.
        /// </summary>
        /// <returns>FT_STATUS vlaue from FT_SetResetPipeRetryCount in FTD2XX.DLL</returns>
        /// <param name="resetPipeRetryCount">The reset pipe retry count.  
        /// Electrically noisy environments may benefit from a larger value.</param>
        //public FtStatus SetResetPipeRetryCount(UInt32 resetPipeRetryCount)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetResetPipeRetryCount != IntPtr.Zero)
        //    {
        //        FtSetResetPipeRetryCount ftSetResetPipeRetryCount = (FtSetResetPipeRetryCount)Marshal.GetDelegateForFunctionPointer(_pFTSetResetPipeRetryCount, typeof(FtSetResetPipeRetryCount));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetResetPipeRetryCount
        //            ftStatus = ftSetResetPipeRetryCount(_ftHandle, resetPipeRetryCount);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetResetPipeRetryCount == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetResetPipeRetryCount.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetDriverVersion
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the current FTDIBUS.SYS driver version number.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetDriverVersion in FTD2XX.DLL</returns>
        /// <param name="driverVersion">The current driver version number.</param>
        //public FtStatus GetDriverVersion(ref UInt32 driverVersion)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetDriverVersion != IntPtr.Zero)
        //    {
        //        var ftGetDriverVersion = (FtGetDriverVersion)Marshal.GetDelegateForFunctionPointer(_pFTGetDriverVersion, typeof(FtGetDriverVersion));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetDriverVersion
        //            ftStatus = ftGetDriverVersion(_ftHandle, ref driverVersion);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetDriverVersion == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetDriverVersion.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetLibraryVersion
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the current FTD2XX.DLL driver version number.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetLibraryVersion in FTD2XX.DLL</returns>
        /// <param name="libraryVersion">The current library version.</param>
        //public FtStatus GetLibraryVersion(ref UInt32 libraryVersion)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetLibraryVersion != IntPtr.Zero)
        //    {
        //        FtGetLibraryVersion ftGetLibraryVersion = (FtGetLibraryVersion)Marshal.GetDelegateForFunctionPointer(_pFTGetLibraryVersion, typeof(FtGetLibraryVersion));

        //        // Call FT_GetLibraryVersion
        //        ftStatus = ftGetLibraryVersion(ref libraryVersion);
        //    }
        //    else
        //    {
        //        if (_pFTGetLibraryVersion == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetLibraryVersion.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetDeadmanTimeout
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets the USB deadman timeout value.  Default is 5000ms.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetDeadmanTimeout in FTD2XX.DLL</returns>
        /// <param name="deadmanTimeout">The deadman timeout value in ms.  Default is 5000ms.</param>
        //public FtStatus SetDeadmanTimeout(UInt32 deadmanTimeout)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetDeadmanTimeout != IntPtr.Zero)
        //    {
        //        var ftSetDeadmanTimeout = (FtSetDeadmanTimeout)Marshal.GetDelegateForFunctionPointer(_pFTSetDeadmanTimeout, typeof(FtSetDeadmanTimeout));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetDeadmanTimeout
        //            ftStatus = ftSetDeadmanTimeout(_ftHandle, deadmanTimeout);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetDeadmanTimeout == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetDeadmanTimeout.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetLatency
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets the value of the latency timer.  Default value is 16ms.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetLatencyTimer in FTD2XX.DLL</returns>
        /// <param name="latency">The latency timer value in ms.
        /// Valid values are 2ms - 255ms for FT232BM, FT245BM and FT2232 devices.
        /// Valid values are 0ms - 255ms for other devices.</param>
        //public FtStatus SetLatency(byte latency)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetLatencyTimer != IntPtr.Zero)
        //    {
        //        var ftSetLatencyTimer = (FtSetLatencyTimer)Marshal.GetDelegateForFunctionPointer(_pFTSetLatencyTimer, typeof(FtSetLatencyTimer));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            var deviceType = FtDevice.FtDeviceUnknown;
        //            // Set Bit Mode does not apply to FT8U232AM, FT8U245AM or FT8U100AX devices
        //            GetDeviceType(ref deviceType);
        //            if ((deviceType == FtDevice.FtDeviceBm) || (deviceType == FtDevice.FtDevice2232))
        //            {
        //                // Do not allow latency of 1ms or 0ms for older devices
        //                // since this can cause problems/lock up due to buffering mechanism
        //                if (latency < 2)
        //                    latency = 2;
        //            }

        //            // Call FT_SetLatencyTimer
        //            ftStatus = ftSetLatencyTimer(_ftHandle, latency);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetLatencyTimer == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetLatencyTimer.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetLatency
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the value of the latency timer.  Default value is 16ms.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetLatencyTimer in FTD2XX.DLL</returns>
        /// <param name="latency">The latency timer value in ms.</param>
        //public FtStatus GetLatency(ref byte latency)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetLatencyTimer != IntPtr.Zero)
        //    {
        //        var ftGetLatencyTimer = (FtGetLatencyTimer)Marshal.GetDelegateForFunctionPointer(_pFTGetLatencyTimer, typeof(FtGetLatencyTimer));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetLatencyTimer
        //            ftStatus = ftGetLatencyTimer(_ftHandle, ref latency);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTGetLatencyTimer == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetLatencyTimer.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetUSBTransferSizes
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets the USB IN and OUT transfer sizes.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetUSBParameters in FTD2XX.DLL</returns>
        /// <param name="inTransferSize">The USB IN transfer size in bytes.</param>
        //public FtStatus InTransferSize(UInt32 inTransferSize)
        //{
        //    // Only support IN transfer sizes at the moment
        //    //public UInt32 InTransferSize(UInt32 InTransferSize, UInt32 OutTransferSize)
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetUSBParameters != IntPtr.Zero)
        //    {
        //        var ftSetUSBParameters = (FtSetUsbParameters)Marshal.GetDelegateForFunctionPointer(_pFTSetUSBParameters, typeof(FtSetUsbParameters));

        //        var outTransferSize = inTransferSize;

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetUSBParameters
        //            ftStatus = ftSetUSBParameters(_ftHandle, inTransferSize, outTransferSize);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetUSBParameters == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetUSBParameters.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // SetCharacters
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Sets an event character, an error character and enables or disables them.
        /// </summary>
        /// <returns>FT_STATUS value from FT_SetChars in FTD2XX.DLL</returns>
        /// <param name="eventChar">A character that will be tigger an IN to the host when this character is received.</param>
        /// <param name="eventCharEnable">Determines if the EventChar is enabled or disabled.</param>
        /// <param name="errorChar">A character that will be inserted into the data stream to indicate that an error has occurred.</param>
        /// <param name="errorCharEnable">Determines if the ErrorChar is enabled or disabled.</param>
        //public FtStatus SetCharacters(byte eventChar, bool eventCharEnable, byte errorChar, bool errorCharEnable)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTSetChars != IntPtr.Zero)
        //    {
        //        var ftSetChars = (FtSetChars)Marshal.GetDelegateForFunctionPointer(_pFTSetChars, typeof(FtSetChars));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_SetChars
        //            ftStatus = ftSetChars(_ftHandle, eventChar, Convert.ToByte(eventCharEnable), errorChar, Convert.ToByte(errorCharEnable));
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTSetChars == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_SetChars.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetEEUserAreaSize
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the size of the EEPROM user area.
        /// </summary>
        /// <returns>FT_STATUS value from FT_EE_UASize in FTD2XX.DLL</returns>
        /// <param name="uaSize">The EEPROM user area size in bytes.</param>
        //public FtStatus EEUserAreaSize(ref UInt32 uaSize)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTEeUaSize != IntPtr.Zero)
        //    {
        //        var ftEEUaSize = (FtEeUaSize)Marshal.GetDelegateForFunctionPointer(_pFTEeUaSize, typeof(FtEeUaSize));

        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            ftStatus = ftEEUaSize(_ftHandle, ref uaSize);
        //        }
        //    }
        //    else
        //    {
        //        if (_pFTEeUaSize == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_EE_UASize.");
        //        }
        //    }
        //    return ftStatus;
        //}

        //**************************************************************************
        // GetCOMPort
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the corresponding COM port number for the current device.  If no COM port is exposed, an empty string is returned.
        /// </summary>
        /// <returns>FT_STATUS value from FT_GetComPortNumber in FTD2XX.DLL</returns>
        /// <param name="comPortName">The COM port name corresponding to the current device.  If no COM port is installed, an empty string is passed back.</param>
        //public FtStatus GetCOMPort(out string comPortName)
        //{
        //    // Initialise ftStatus to something other than FT_OK
        //    var ftStatus = FtStatus.FtOtherError;

        //    // As ComPortName is an OUT paremeter, has to be assigned before returning
        //    comPortName = string.Empty;

        //    // If the DLL hasn't been loaded, just return here
        //    if (_hFtd2Xxdll == IntPtr.Zero)
        //        return ftStatus;

        //    // Check for our required function pointers being set up
        //    if (_pFTGetComPortNumber != IntPtr.Zero)
        //    {
        //        var ftGetComPortNumber = (FtGetComPortNumber)Marshal.GetDelegateForFunctionPointer(_pFTGetComPortNumber, typeof(FtGetComPortNumber));

        //        var comPortNumber = -1;
        //        if (_ftHandle != IntPtr.Zero)
        //        {
        //            // Call FT_GetComPortNumber
        //            ftStatus = ftGetComPortNumber(_ftHandle, ref comPortNumber);
        //        }

        //        comPortName = comPortNumber == -1 ? string.Empty : "COM" + comPortNumber.ToString(CultureInfo.InvariantCulture);
        //    }
        //    else
        //    {
        //        if (_pFTGetComPortNumber == IntPtr.Zero)
        //        {
        //            //MessageBox.Show("Failed to load function FT_GetComPortNumber.");
        //        }
        //    }
        //    return ftStatus;
        //}
        #endregion METHOD_DEFINITIONS
        // ReSharper restore CSharpWarnings::CS1584
        // ReSharper restore CSharpWarnings::CS1571
        // ReSharper disable CSharpWarnings::CS1573
        public void SetFtHandle(IntPtr handle) {
            _ftHandle = handle;
        }
        // ReSharper restore CSharpWarnings::CS1573

        public IntPtr GetFtHandle() {
            return _ftHandle;
        }

        public FtStatus GetLastStatusMsg() {
            return _lastStatus;
        }

        #region PROPERTY_DEFINITIONS
        //**************************************************************************
        // IsOpen
        //**************************************************************************
        // Intellisense comments
        /// <summary>
        /// Gets the open status of the device.
        /// </summary>
        public bool IsOpen
        {
            get {
                return _ftHandle != IntPtr.Zero;
            }
        }

        //**************************************************************************
        // InterfaceIdentifier
        //**************************************************************************
        // Intellisense comments
// ReSharper disable CSharpWarnings::CS1587
        /// <summary>
        /// Gets the interface identifier.
        /// </summary>
        //private string InterfaceIdentifier
        //{
        //    get
        //    {
        //        var identifier = String.Empty;
        //        if (IsOpen)
        //        {
        //            var deviceType = FtDevice.FtDeviceBm;
        //            GetDeviceType(ref deviceType);
        //            if ((deviceType == FtDevice.FtDevice2232) | (deviceType == FtDevice.FtDevice2232H) | (deviceType == FtDevice.FtDevice4232H))
        //            {
        //                string description;
        //                GetDescription(out description);
        //                identifier = description.Substring((description.Length - 1));
        //                return identifier;
        //            }
        //        }
        //        return identifier;
        //    }
        //}
        #endregion

        #region HELPER_METHODS
        //**************************************************************************
        // ErrorHandler
        //**************************************************************************
        /// <summary>
        /// Method to check ftStatus and ftErrorCondition values for error conditions and throw exceptions accordingly.
        /// </summary>
// ReSharper restore CSharpWarnings::CS1587
        //private static void ErrorHandler(FtStatus ftStatus, FtError ftErrorCondition)
        //{
        //    if (ftStatus != FtStatus.FtOk)
        //    {
        //        // Check FT_STATUS values returned from FTD2XX DLL calls
        //        switch (ftStatus) {
        //            case FtStatus.FtDeviceNotFound:
        //                throw new FTException("FTDI device not found.");
        //            case FtStatus.FtDeviceNotOpened:
        //                throw new FTException("FTDI device not opened.");
        //            case FtStatus.FtDeviceNotOpenedForErase:
        //                throw new FTException("FTDI device not opened for erase.");
        //            case FtStatus.FtDeviceNotOpenedForWrite:
        //                throw new FTException("FTDI device not opened for write.");
        //            case FtStatus.FtEepromEraseFailed:
        //                throw new FTException("Failed to erase FTDI device EEPROM.");
        //            case FtStatus.FtEepromNotPresent:
        //                throw new FTException("No EEPROM fitted to FTDI device.");
        //            case FtStatus.FtEepromNotProgrammed:
        //                throw new FTException("FTDI device EEPROM not programmed.");
        //            case FtStatus.FtEepromReadFailed:
        //                throw new FTException("Failed to read FTDI device EEPROM.");
        //            case FtStatus.FtEepromWriteFailed:
        //                throw new FTException("Failed to write FTDI device EEPROM.");
        //            case FtStatus.FtFailedToWriteDevice:
        //                throw new FTException("Failed to write to FTDI device.");
        //            case FtStatus.FtInsufficientResources:
        //                throw new FTException("Insufficient resources.");
        //            case FtStatus.FtInvalidArgs:
        //                throw new FTException("Invalid arguments for FTD2XX function call.");
        //            case FtStatus.FtInvalidBaudRate:
        //                throw new FTException("Invalid Baud rate for FTDI device.");
        //            case FtStatus.FtInvalidHandle:
        //                throw new FTException("Invalid handle for FTDI device.");
        //            case FtStatus.FtInvalidParameter:
        //                throw new FTException("Invalid parameter for FTD2XX function call.");
        //            case FtStatus.FtIoError:
        //                throw new FTException("FTDI device IO error.");
        //            case FtStatus.FtOtherError:
        //                throw new FTException("An unexpected error has occurred when trying to communicate with the FTDI device.");
        //        }
        //    }
        //    if (ftErrorCondition == FtError.FtNoError)
        //        return;
        //    // Check for other error conditions not handled by FTD2XX DLL
        //    switch (ftErrorCondition) {
        //        case FtError.FtIncorrectDevice:
        //            throw new FTException(
        //                "The current device type does not match the EEPROM structure.");
        //        case FtError.FtInvalidBitmode:
        //            throw new FTException(
        //                "The requested bit mode is not valid for the current device.");
        //        case FtError.FtBufferSize:
        //            throw new FTException("The supplied buffer is not big enough.");
        //    }
        //}
        #endregion
    }
}
