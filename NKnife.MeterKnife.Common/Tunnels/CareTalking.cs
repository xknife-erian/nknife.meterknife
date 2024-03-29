﻿using System;
using System.Text;
using NKnife.MeterKnife.Util.Protocol;
using NKnife.MeterKnife.Util.Protocol.Generic;
using NKnife.Util;

namespace NKnife.MeterKnife.Common.Tunnels
{
    /// <summary>
    ///     面向Care制定的协议的封装
    /// </summary>
    public class CareTalking : BytesProtocol
    {
        private string _scpi = string.Empty;

        public CareTalking()
        {
            ((IProtocol<byte[]>) this).Command = new byte[2];
            ((IProtocol<byte[]>) this).CommandParam = new byte[1];
        }

        public byte[] Source { get; set; }

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
            return $"GPIB:{GpibAddress}, Command:{MainCommand.ToHexString()}|{SubCommand.ToHexString()}, Content:{Scpi}".TrimEnd('\r', '\n');
        }

        #region 基本属性

        public string DUT { get; set; }

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
                if (string.IsNullOrWhiteSpace(_scpi) && ScpiBytes != null && ScpiBytes.Length > 0)
                    _scpi = Encoding.ASCII.GetString(ScpiBytes);
                return _scpi;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    if (value.Length > 1 && value[value.Length - 1] != '\n')
                        _scpi = $"{value}{'\n'}";
                    else
                        _scpi = value;
                }
                else
                {
                    _scpi = string.Empty;
                }
            }
        }

        #endregion
    }
}