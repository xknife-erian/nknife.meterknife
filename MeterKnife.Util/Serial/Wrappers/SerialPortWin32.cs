using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SerialKnife.Wrappers
{
    ///<summary>串口类操作类，通过windows api实现
    ///</summary>
    public class SerialPortWin32
    {
        ///<summary>奇偶校验0-4=no,odd,even,mark,space 
        ///</summary>
        public const byte Parity = 0; //0-4=no,odd,even,mark,space 

        ///<summary>停止位
        ///</summary>
        public const byte StopBits = 0; //0,1,2 = 1, 1.5, 2 

        ///<summary>COM口句柄
        ///</summary>
        private int _HComm = -1;

        ///<summary>串口是否已经打开
        ///</summary>
        public bool Opened;

        public SerialPortWin32()
        {
            BaudRate = 9600;
            ByteSize = 8;
        }

        ///<summary>波特率9600
        ///</summary>
        public int BaudRate { get; internal set; }

        ///<summary>数据位4-8
        ///</summary>
        public byte ByteSize { get; internal set; }

        ///<summary>端口名称(COM1,COM2...COM4...)
        ///</summary>
        public string Port { get; set; }

        ///<summary>超时长
        ///</summary>
        public int ReadTimeout { get; set; }

        ///<summary>设置DCB标志位
        ///</summary>
        ///<param name="whichFlag"></param>
        ///<param name="setting"></param>
        ///<param name="dcb"></param>
        private void SetDcbFlag(int whichFlag, int setting, DCB dcb)
        {
            uint num;
            setting = setting << whichFlag;
            if ((whichFlag == 4) || (whichFlag == 12))
            {
                num = 3;
            }
            else if (whichFlag == 15)
            {
                num = 0x1ffff;
            }
            else
            {
                num = 1;
            }
            dcb.flags &= ~(num << whichFlag);
            dcb.flags |= (uint) setting;
        }

        ///<summary>建立与串口的连接
        ///</summary>
        public int Open()
        {
            var dcb = new DCB();
            var ctoCommPort = new COMMTIMEOUTS();

            // 打开串口 
            //
            _HComm = CreateFile(Port, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, 0, 0);
            //FILE_ATTRIBUTE_NORMAL|FILE_FLAG_OVERLAPPED, //重叠方式
            //hComm = CreateFile(Port, GENERIC_READ | GENERIC_WRITE, 0, 0, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL | FILE_FLAG_OVERLAPPED, 0);

            if (_HComm == INVALID_HANDLE_VALUE)
            {
                return -1;
            }
            // 设置通信超时时间
            GetCommTimeouts(_HComm, ref ctoCommPort);
            ctoCommPort.ReadTotalTimeoutConstant = ReadTimeout;
            ctoCommPort.WriteTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutConstant = 0;
            SetCommTimeouts(_HComm, ref ctoCommPort);

            //设置串口参数
            GetCommState(_HComm, ref dcb);
            dcb.DCBlength = Marshal.SizeOf(dcb);
            dcb.BaudRate = BaudRate;
            dcb.flags = 0;
            dcb.ByteSize = ByteSize;
            dcb.StopBits = StopBits;
            dcb.Parity = Parity;

            //------------------------------
            SetDcbFlag(0, 1, dcb); //二进制方式 
            SetDcbFlag(1, (Parity == 0) ? 0 : 1, dcb);
            SetDcbFlag(2, 0, dcb); //不用CTS检测发送流控制
            SetDcbFlag(3, 0, dcb); //不用DSR检测发送流控制
            SetDcbFlag(4, 0, dcb); //禁止DTR流量控制
            SetDcbFlag(6, 0, dcb); //对DTR信号线不敏感
            SetDcbFlag(9, 1, dcb); //检测接收缓冲区
            SetDcbFlag(8, 0, dcb); //不做发送字符控制
            SetDcbFlag(10, 0, dcb); //是否用指定字符替换校验错的字符
            SetDcbFlag(11, 0, dcb); //保留NULL字符
            SetDcbFlag(12, 0, dcb); //允许RTS流量控制
            SetDcbFlag(14, 0, dcb); //发送错误后，继续进行下面的读写操作
            //--------------------------------
            dcb.wReserved = 0; //没有使用，必须为0      
            dcb.XonLim = 0; //指定在XOFF字符发送之前接收到缓冲区中可允许的最小字节数
            dcb.XoffLim = 0; //指定在XOFF字符发送之前缓冲区中可允许的最小可用字节数
            dcb.XonChar = 0; //发送和接收的XON字符 
            dcb.XoffChar = 0; //发送和接收的XOFF字符
            dcb.ErrorChar = 0; //代替接收到奇偶校验错误的字符 
            dcb.EofChar = 0; //用来表示数据的结束      
            dcb.EvtChar = 0; //事件字符，接收到此字符时，会产生一个事件        
            dcb.wReserved1 = 0; //没有使用 

            if (!SetCommState(_HComm, ref dcb))
            {
                return -2;
            }
            Opened = true;
            return 0;
        }

        ///<summary>关闭串口,结束通讯
        ///</summary>
        public void Close()
        {
            if (_HComm != INVALID_HANDLE_VALUE)
            {
                CloseHandle(_HComm);
            }
        }

        /// <summary>动态设置读取超时
        /// </summary>
        /// <param name="timeout"></param>
        public virtual void SetTimeOut(int timeout)
        {
            ReadTimeout = timeout;
            if (_HComm < 0) return;
            var ctoCommPort = new COMMTIMEOUTS();

            // 设置通信超时时间
            GetCommTimeouts(_HComm, ref ctoCommPort);
            ctoCommPort.ReadTotalTimeoutConstant = timeout;
            ctoCommPort.ReadTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutMultiplier = 0;
            ctoCommPort.WriteTotalTimeoutConstant = 0;
            SetCommTimeouts(_HComm, ref ctoCommPort);
        }

        ///<summary>读取串口返回的数据
        ///</summary>
        ///<param name="bytData"></param>
        ///<param name="numBytes">数据长度</param>
        public int Read(ref byte[] bytData, int numBytes)
        {
            if (_HComm != INVALID_HANDLE_VALUE)
            {
                COMSTAT stats;
                uint flags;
                ClearCommError(_HComm, out flags, out stats);

                var ovlCommPort = new OVERLAPPED();
                //ovlCommPort.hEvent = CreateEvent(IntPtr.Zero, false, true, "ReadEvent");

                int bytesRead = 0;
                try
                {
                    ReadFile(_HComm, bytData, numBytes, ref bytesRead, ref ovlCommPort);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                //if (!bReadStatus) //如果ReadFile函数返回FALSE
                //{
                //    if (GetLastError() == ERROR_IO_PENDING)
                //    //GetLastError()函数返回ERROR_IO_PENDING,表明串口正在进行读操作	
                //    {
                //        WaitForSingleObject((IntPtr)ovlCommPort.hEvent, (uint)ReadTimeout);
                //        //使用WaitForSingleObject函数等待，直到读操作完成或延时已达到2秒钟
                //        //当串口读操作进行完毕后，m_osRead的hEvent事件会变为有信号
                //        PurgeComm(hComm, PURGE_TXABORT |
                //            PURGE_RXABORT | PURGE_TXCLEAR | PURGE_RXCLEAR);
                //        return BytesRead;
                //    }
                //    return 0;
                //}
                //PurgeComm(hComm, PURGE_TXABORT |
                //          PURGE_RXABORT | PURGE_TXCLEAR | PURGE_RXCLEAR);
                return bytesRead;
            }
            return -1;
        }

        ///<summary>向串口写数据
        ///</summary>
        ///<param name="writeBytes">数据数组</param>
        ///<param name="intSize"></param>
        public int Write(byte[] writeBytes, int intSize)
        {
            ClearReceiveBuf();
            ClearSendBuf();
            if (_HComm != INVALID_HANDLE_VALUE)
            {
                var ovlCommPort = new OVERLAPPED();
                int bytesWritten = 0;

                //ovlCommPort.hEvent = CreateEvent(IntPtr.Zero, false, true, "WriteEvent");
                //WriteFile(hComm, WriteBytes, intSize, ref BytesWritten, ref ovlCommPort);
                WriteFile(_HComm, writeBytes, intSize, ref bytesWritten, ref ovlCommPort);
                //if (!bWriteStat)
                //{
                //    if (GetLastError() == ERROR_IO_PENDING)
                //    {
                //        WaitForSingleObject((IntPtr)ovlCommPort.hEvent, 100);
                //        return BytesWritten;
                //    }
                //    return 0;
                //}
                return bytesWritten;
            }
            return -1;
        }

        ///<summary>
        ///清除接收缓冲区
        ///</summary>
        ///<returns></returns>
        public void ClearReceiveBuf()
        {
            if (_HComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(_HComm, PURGE_RXABORT | PURGE_RXCLEAR);
            }
        }

        ///<summary>
        ///清除发送缓冲区
        ///</summary>
        public void ClearSendBuf()
        {
            if (_HComm != INVALID_HANDLE_VALUE)
            {
                PurgeComm(_HComm, PURGE_TXABORT | PURGE_TXCLEAR);
            }
        }

        #region "API相关定义"

        private const string DLLPATH = "kernel32.dll";

        ///<summary>
        /// WINAPI常量,写标志
        ///</summary>
        private const uint GENERIC_READ = 0x80000000;

        ///<summary>
        /// WINAPI常量,读标志
        ///</summary>
        private const uint GENERIC_WRITE = 0x40000000;

        ///<summary>
        /// WINAPI常量,打开已存在
        ///</summary>
        private const int OPEN_EXISTING = 3;

        private const uint FILE_FLAG_OVERLAPPED = 0x40000000;
        private const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;

        ///<summary>
        /// WINAPI常量,无效句柄
        ///</summary>
        private const int INVALID_HANDLE_VALUE = -1;

        private const int PURGE_RXABORT = 0x2;
        private const int PURGE_RXCLEAR = 0x8;
        private const int PURGE_TXABORT = 0x1;
        private const int PURGE_TXCLEAR = 0x4;

        private const uint ERROR_IO_PENDING = 997;

        ///<summary>
        ///打开串口
        ///</summary>
        ///<param name="lpFileName">要打开的串口名称</param>
        ///<param name="dwDesiredAccess">指定串口的访问方式，一般设置为可读可写方式</param>
        ///<param name="dwShareMode">指定串口的共享模式，串口不能共享，所以设置为0</param>
        ///<param name="lpSecurityAttributes">设置串口的安全属性，WIN9X下不支持，应设为NULL</param>
        ///<param name="dwCreationDisposition">对于串口通信，创建方式只能为OPEN_EXISTING</param>
        ///<param name="dwFlagsAndAttributes">指定串口属性与标志，设置为FILE_FLAG_OVERLAPPED(重叠I/O操作)，指定串口以异步方式通信</param>
        ///<param name="hTemplateFile">对于串口通信必须设置为NULL</param>
        [DllImport(DLLPATH)]
        private static extern int CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, int lpSecurityAttributes, int dwCreationDisposition, uint dwFlagsAndAttributes, int hTemplateFile);

        ///<summary>
        ///得到串口状态
        ///</summary>
        ///<param name="hFile">通信设备句柄</param>
        ///<param name="lpDCB">设备控制块DCB</param>
        [DllImport(DLLPATH)]
        private static extern bool GetCommState(int hFile, ref DCB lpDCB);

        ///<summary>
        ///建立串口设备控制块(嵌入版没有)
        ///</summary>
        ///<param name="lpDef">设备控制字符串</param>
        ///<param name="lpDCB">设备控制块</param>
        [DllImport(DLLPATH)]
        private static extern bool BuildCommDCB(string lpDef, ref DCB lpDCB);

        ///<summary>
        ///设置串口状态
        ///</summary>
        ///<param name="hFile">通信设备句柄</param>
        ///<param name="lpDCB">设备控制块</param>
        [DllImport(DLLPATH)]
        private static extern bool SetCommState(int hFile, ref DCB lpDCB);

        ///<summary>
        ///读取串口超时时间
        ///</summary>
        ///<param name="hFile">通信设备句柄</param>
        ///<param name="lpCommTimeouts">超时时间</param>
        [DllImport(DLLPATH)]
        private static extern bool GetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        ///<summary>
        ///设置串口超时时间
        ///</summary>
        ///<param name="hFile">通信设备句柄</param>
        ///<param name="lpCommTimeouts">超时时间</param>
        [DllImport(DLLPATH)]
        private static extern bool SetCommTimeouts(int hFile, ref COMMTIMEOUTS lpCommTimeouts);

        ///<summary>
        ///读取串口数据
        ///</summary>
        ///<param name="hFile">通信设备句柄</param>
        ///<param name="lpBuffer">数据缓冲区</param>
        ///<param name="nNumberOfBytesToRead">多少字节等待读取</param>
        ///<param name="lpNumberOfBytesRead">读取多少字节</param>
        ///<param name="lpOverlapped">溢出缓冲区</param>
        [DllImport(DLLPATH)]
        private static extern bool ReadFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, ref OVERLAPPED lpOverlapped);

        ///<summary>
        ///写串口数据
        ///</summary>
        ///<param name="hFile">通信设备句柄</param>
        ///<param name="lpBuffer">数据缓冲区</param>
        ///<param name="nNumberOfBytesToWrite">多少字节等待写入</param>
        ///<param name="lpNumberOfBytesWritten">已经写入多少字节</param>
        ///<param name="lpOverlapped">溢出缓冲区</param>
        [DllImport(DLLPATH)]
        private static extern bool WriteFile(int hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, ref OVERLAPPED lpOverlapped);

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool FlushFileBuffers(int hFile);

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool PurgeComm(int hFile, uint dwFlags);

        ///<summary>
        ///关闭串口
        ///</summary>
        ///<param name="hObject">通信设备句柄</param>
        [DllImport(DLLPATH)]
        private static extern bool CloseHandle(int hObject);

        ///<summary>
        ///得到串口最后一次返回的错误
        ///</summary>
        [DllImport(DLLPATH)]
        private static extern uint GetLastError();

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern bool GetOverlappedResult(IntPtr hFile,
                                                       [In] ref NativeOverlapped lpOverlapped,
                                                       out uint lpNumberOfBytesTransferred, bool bWait);

        [DllImport(DLLPATH, SetLastError = true)]
        private static extern Int32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);

        [DllImport(DLLPATH, SetLastError = true, EntryPoint = "CreateEventA")]
        private static extern IntPtr CreateEvent(IntPtr lpEventAttributes, bool bManualReset, bool bInitialState, string lpName);

        [DllImport("kernel32.dll")]
        private static extern bool ClearCommError(
            [In] int hFile,
            [Out, Optional] out uint lpErrors,
            [Out, Optional] out COMSTAT lpStat
            );

        #region Nested type: COMMTIMEOUTS

        ///<summary>
        ///串口超时时间结构体类型
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct COMMTIMEOUTS
        {
            public readonly int ReadIntervalTimeout;
            public int ReadTotalTimeoutMultiplier;
            public int ReadTotalTimeoutConstant;
            public int WriteTotalTimeoutMultiplier;
            public int WriteTotalTimeoutConstant;
        }

        #endregion

        #region Nested type: COMSTAT

        [StructLayout(LayoutKind.Sequential)]
        private struct COMSTAT
        {
            public const uint fCtsHold = 0x1;
            public const uint fDsrHold = 0x2;
            public const uint fRlsdHold = 0x4;
            public const uint fXoffHold = 0x8;
            public const uint fXoffSent = 0x10;
            public const uint fEof = 0x20;
            public const uint fTxim = 0x40;
            public readonly UInt32 Flags;
            public readonly UInt32 cbInQue;
            public readonly UInt32 cbOutQue;
        }

        #endregion

        #region Nested type: DCB

        ///<summary>
        ///设备控制块结构体类型
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DCB
        {
            ///<summary>
            /// DCB长度
            ///</summary>
            public int DCBlength;

            ///<summary>
            ///指定当前波特率
            ///</summary>
            public int BaudRate;

            ///<summary>
            ///标志位
            ///</summary>
            public uint flags;

            ///<summary>
            ///未使用,必须为0
            ///</summary>
            public ushort wReserved;

            ///<summary>
            ///指定在XON字符发送这前接收缓冲区中可允许的最小字节数
            ///</summary>
            public ushort XonLim;

            ///<summary>
            ///指定在XOFF字符发送这前接收缓冲区中可允许的最小字节数
            ///</summary>
            public ushort XoffLim;

            ///<summary>
            ///指定端口当前使用的数据位
            ///</summary>
            public byte ByteSize;

            ///<summary>
            ///指定端口当前使用的奇偶校验方法,可能为:EVENPARITY,MARKPARITY,NOPARITY,ODDPARITY 0-4=no,odd,even,mark,space 
            ///</summary>
            public byte Parity;

            ///<summary>
            ///指定端口当前使用的停止位数,可能为:ONESTOPBIT,ONE5STOPBITS,TWOSTOPBITS 0,1,2 = 1, 1.5, 2 
            ///</summary>
            public byte StopBits;

            ///<summary>
            ///指定用于发送和接收字符XON的值 Tx and Rx XON character 
            ///</summary>
            public byte XonChar;

            ///<summary>
            ///指定用于发送和接收字符XOFF值 Tx and Rx XOFF character 
            ///</summary>
            public byte XoffChar;

            ///<summary>
            ///本字符用来代替接收到的奇偶校验发生错误时的值
            ///</summary>
            public byte ErrorChar;

            ///<summary>
            ///当没有使用二进制模式时,本字符可用来指示数据的结束
            ///</summary>
            public byte EofChar;

            ///<summary>
            ///当接收到此字符时,会产生一个事件
            ///</summary>
            public byte EvtChar;

            ///<summary>
            ///未使用
            ///</summary>
            public ushort wReserved1;
        }

        #endregion

        #region Nested type: OVERLAPPED

        ///<summary>
        ///溢出缓冲区结构体类型
        ///</summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct OVERLAPPED
        {
            //[FieldOffset(0)]
            public readonly int Internal;
            //[FieldOffset(1)]
            public readonly int InternalHigh;
            //[FieldOffset(2)]
            public readonly int Offset;
            //[FieldOffset(3)]
            public readonly int OffsetHigh;
            //[FieldOffset(4)]
            public readonly IntPtr hEvent;
        }

        #endregion

        #endregion
    }
}