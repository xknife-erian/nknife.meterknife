using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using MeterKnife.Util.Utility;
using MeterKnife.Util.Zip;

namespace MeterKnife.Util.Tunnel.Generic
{
    /// <summary>
    ///     一个最常用的 字符数组 => 字符串 转换器。
    /// </summary>
    public class LengthHeadDecoder : StringDatagramDecoder
    {
        private static readonly ILog _logger = LogManager.GetLogger<LengthHeadDecoder>();

        public LengthHeadDecoder()
        {
            NeedReverse = false;
            EnabelCompress = false;
            Encoding = Encoding.UTF8;
        }

        /// <summary>
        ///     长度头的数组是否需要反转
        /// </summary>
        public bool NeedReverse { get; set; }

        /// <summary>
        ///     是否启用Gzip压缩
        /// </summary>
        public bool EnabelCompress { get; set; }

        public Encoding Encoding { get; set; }

        /// <summary>
        ///     解码。将字节数组解析成字符串。
        /// </summary>
        /// <param name="data">需解码的字节数组.</param>
        /// <param name="finishedIndex">已完成解码的数组的长度.</param>
        /// <returns></returns>
        public override string[] Execute(byte[] data, out int finishedIndex)
        {
            finishedIndex = 0;
            var results = new List<string>();
            try
            {
                bool inComplete = true; //解析未完成标记
                while (inComplete)
                {
                    if (results.Count > 1)
                        _logger.Trace(string.Format("粘包处理,总长度:{0},已解析:{1},得到结果:{2}", data.Length, finishedIndex, results.Count));
                    int start = finishedIndex; //finishedIndex不等于0时，代表有粘包
                    inComplete = ExecuteSubMethod(data, start, ref results, ref finishedIndex);
                }
                return results.ToArray();
            }
            catch (Exception e)
            {
                _logger.Warn("解码转换异常", e);
                return new string[0];
            }
        }

        private bool ExecuteSubMethod(byte[] data, int start, ref List<string> results, ref int finishedIndex)
        {
            if (UtilityCollection.IsNullOrEmpty(data))
                return false;
            if (data.Length <= 4)
                return false;
            var protocol = new byte[] {};
            try
            {
                var lengthHead = new byte[4];
                Buffer.BlockCopy(data, start, lengthHead, 0, 4);
                int protocolLength = GetLengthHead(lengthHead);
                if (start + 4 + protocolLength > data.Length) //这时又出现了半包现象
                {
                    _logger.Trace(string.Format("处理粘包时出现半包:起点:{0},计算得到的长度:{1},源数据长度:{2}", start, protocolLength, data.Length));
                    return false;
                }

                protocol = new byte[protocolLength];
                Buffer.BlockCopy(data, start + 4, protocol, 0, protocolLength);
            }
            catch (Exception e)
            {
                _logger.Error("解码异常", e);
            }

            if (!UtilityCollection.IsNullOrEmpty(protocol))
            {
                string tidyString = TidyString(EnabelCompress ? CompressHelper.Decompress(protocol) : protocol);
                results.Add(tidyString);
            }
            finishedIndex = start + 4 + protocol.Length;

            return data.Length > finishedIndex;
        }

        protected virtual int GetLengthHead(byte[] lenArray)
        {
            if (NeedReverse)
                Array.Reverse(lenArray);
            int protocolLength = BitConverter.ToInt32(lenArray, 0);
            return protocolLength;
        }

        protected virtual string TidyString(byte[] protocol)
        {
            if (CompressHelper.IsCompressed(protocol)) //采用Gzip进行了压缩
            {
                byte[] decompress = CompressHelper.Decompress(protocol);
                return Encoding.GetString(decompress);
                //return UtilityString.TidyUTF8(decompress);
            }
            return Encoding.GetString(protocol);
            //return UtilityString.TidyUTF8(protocol);
        }
    }
}