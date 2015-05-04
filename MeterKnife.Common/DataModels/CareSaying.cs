using System;
using System.Text;
using NKnife.Converts;
using NKnife.Protocol;
using NKnife.Protocol.Generic;

namespace MeterKnife.Common.DataModels
{
    /// <summary>
    ///     面向Care制定的协议的封装
    /// </summary>
    public partial class CareSaying : BytesProtocol
    {
        private string _Scpi = string.Empty;
        private byte[] _ScpiBytes = null;

        public CareSaying()
        {
            ((IProtocol<byte[]>) this).Command = new byte[2];
            ((IProtocol<byte[]>) this).CommandParam = new byte[1];
        }

        public byte[] Generate()
        {
            var bs = new byte[] {0x08, (byte) GpibAddress, (byte) (_ScpiBytes.Length + 2), MainCommand, SubCommand};
            var result = new byte[bs.Length + _ScpiBytes.Length];
            Buffer.BlockCopy(bs, 0, result, 0, bs.Length);
            Buffer.BlockCopy(_ScpiBytes, 0, result, bs.Length, _ScpiBytes.Length);
            return result;
        }

        #region 基本属性

        /// <summary>
        ///     主命令字
        /// </summary>
        public byte MainCommand
        {
            get { return ((IProtocol<byte[]>) this).Command[0]; }
            set { ((IProtocol<byte[]>) this).Command[0] = value; }
        }

        /// <summary>
        ///     子命令
        /// </summary>
        public byte SubCommand
        {
            get { return ((IProtocol<byte[]>)this).Command[1]; }
            set { ((IProtocol<byte[]>)this).Command[1] = value; }
        }

        /// <summary>
        ///     GPIB地址
        /// </summary>
        public short GpibAddress
        {
            get { return UtilityConvert.ConvertTo<short>(((IProtocol<byte[]>)this).CommandParam[0]); }
            set { ((IProtocol<byte[]>)this).CommandParam[0] = UtilityConvert.ConvertTo<byte>(value); }
        }

        /// <summary>
        ///     内容的长度
        /// </summary>
        public short Length
        {
            get { return (short) (_ScpiBytes.Length); }
        }

        /// <summary>
        ///     内容
        /// </summary>
        public string Content
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Scpi) && _ScpiBytes != null && _ScpiBytes.Length > 0)
                    _Scpi = Encoding.ASCII.GetString(_ScpiBytes);
                return _Scpi;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    _Scpi = value[value.Length - 1] != '\n' ? string.Format("{0}{1}", value, '\n') : value;
                    _ScpiBytes = Encoding.ASCII.GetBytes(_Scpi);
                }
                else
                {
                    _Scpi = string.Empty;
                    _ScpiBytes = new byte[0];
                }
            }
        }

        #endregion
    }
}