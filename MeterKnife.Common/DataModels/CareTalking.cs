using System;
using System.Text;
using MeterKnife.Util.Protocol;
using MeterKnife.Util.Protocol.Generic;
using NKnife.Util;

namespace MeterKnife.Common.DataModels
{
    /// <summary>
    ///     面向Care制定的协议的封装
    /// </summary>
    public class CareTalking : BytesProtocol
    {
        private string _Scpi = string.Empty;

        public CareTalking()
        {
            ((IProtocol<byte[]>) this).Command = new byte[2];
            ((IProtocol<byte[]>) this).CommandParam = new byte[1];
        }

        public byte[] Generate()
        {
            var bs = new byte[] {0x08, (byte) GpibAddress, (byte) (ScpiBytes.Length + 2), MainCommand, SubCommand};
            var result = new byte[bs.Length + ScpiBytes.Length];
            Buffer.BlockCopy(bs, 0, result, 0, bs.Length);
            Buffer.BlockCopy(ScpiBytes, 0, result, bs.Length, ScpiBytes.Length);
            return result;
        }

        public override string ToString()
        {
            return $"Command:{MainCommand.ToHexString()} {SubCommand.ToHexString()},GPIB:{GpibAddress},Content:{Scpi}";
        }

        #region 基本属性

        /// <summary>
        ///     主命令字
        /// </summary>
        public byte MainCommand
        {
            get => ((IProtocol<byte[]>) this).Command[0];
            set => ((IProtocol<byte[]>) this).Command[0] = value;
        }

        /// <summary>
        ///     子命令
        /// </summary>
        public byte SubCommand
        {
            get => ((IProtocol<byte[]>) this).Command[1];
            set => ((IProtocol<byte[]>) this).Command[1] = value;
        }

        /// <summary>
        ///     GPIB地址
        /// </summary>
        public short GpibAddress
        {
            get => UtilConvert.ConvertTo<short>(((IProtocol<byte[]>) this).CommandParam[0]);
            set => ((IProtocol<byte[]>) this).CommandParam[0] = UtilConvert.ConvertTo<byte>(value);
        }

        /// <summary>
        ///     内容的长度
        /// </summary>
        public short Length => (short) ScpiBytes.Length;

        public byte[] ScpiBytes { get; set; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Scpi
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_Scpi) && ScpiBytes != null && ScpiBytes.Length > 0)
                    _Scpi = Encoding.ASCII.GetString(ScpiBytes);
                return _Scpi;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length > 1 && value[value.Length - 1] != '\n')
                        _Scpi = $"{value}{'\n'}";
                    else
                        _Scpi = value;
                }
                else
                {
                    _Scpi = string.Empty;
                }
            }
        }

        #endregion
    }
}