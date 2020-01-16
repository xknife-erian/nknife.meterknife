using System;
using System.Collections.Generic;
using Common.Logging;
using MeterKnife.Util.Tunnel.Generic;
using NKnife.Util;

namespace MeterKnife.Common.Tunnels.CareOne
{
    /// <summary>
    /// 基于CareOne的协议规则的解码器。
    /// 实现将接收到的字节数组进行分解成单条协议数据的能力。
    /// </summary>
    public class CareOneDatagramDecoder : BytesDatagramDecoder
    {
        private static readonly ILog _logger = LogManager.GetLogger<CareOneDatagramDecoder>();

        public const byte LEAD = 0x09;

        public override byte[][] Execute(byte[] data, out int finishedIndex)
        {
            finishedIndex = 0;
            var css = new List<byte[]>();
            bool hasData = true; //是否有数据未解析完成
            while (hasData)
            {
                if (data.Length > 0 && data[finishedIndex] == LEAD)
                {
                    int length;
                    byte[] cs;
                    bool parseSuccess = Single(data, finishedIndex, out length, out cs); //提取单条数据
                    if (!parseSuccess)
                    {
                        hasData = false;
                        finishedIndex = data.Length; //当解析失败时，丢弃数据
                        continue;
                    }
                    css.Add(cs);
                    finishedIndex = finishedIndex + length;
                }
                else
                {
                    finishedIndex++;
                }
                if (finishedIndex >= data.Length)
                {
                    hasData = false;
                }
            }
            return css.ToArray();
        }

        /// <summary>
        /// 单条数据的提取
        /// </summary>
        /// <param name="data">整体数据流</param>
        /// <param name="index">开始提取的位置</param>
        /// <param name="length">提取完成的位置</param>
        /// <param name="cs">提取出的内容</param>
        /// <returns>是否提取成功</returns>
        protected virtual bool Single(byte[] data, int index, out int length, out byte[] cs)
        {
            try
            {
                var sl = UtilConvert.ConvertTo<short>(data[index + 2]);
                length = 3 + sl;
                cs = new byte[length];
                Buffer.BlockCopy(data, index, cs, 0, length);
                return true;
            }
            catch (Exception e)
            {
                _logger.Warn(string.Format("解析单条数据时异常:{0}", e.Message), e);
                _logger.Warn(data.ToHexString());
                length = 0;
                cs = new byte[0];
                return false;
            }
        }
    }
}