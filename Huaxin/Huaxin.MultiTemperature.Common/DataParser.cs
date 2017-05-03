using System;
using System.Collections.Generic;
using Common.Logging;
using NKnife.Collections;

namespace Huaxin.MultiTemperature.Common
{
    public class DataParser
    {
        private static readonly ILog _logger = LogManager.GetLogger<DataParser>();

        public bool DataArrivedThreadStarted { get; set; }
        public Data Bag { get; set; } = new Data();
        public bool IsDebug { get; set; } = false;

        public void Parser(object state)
        {
            var queue = (SyncQueue<byte>) state;
            var isData = false;
            var complate = false;
            short length = -1;
            while (DataArrivedThreadStarted)
            {
                if (queue.Count <= 0)
                {
#if DEBUG
                    if (!IsDebug)
                        queue.AddEvent.WaitOne();
#endif
                }
                if (Bag.Head.Count < 4 && !isData)
                {
                    switch (Bag.Head.Count)
                    {
                        case 0:
                        {
                            byte q;
                            var has = queue.TryDequeue(out q);
                            if (has)
                                if (q == Data.Head1)
                                {
                                    Bag.Head.Add(q);
                                    _logger.Trace($"1>头部解析:{q}, QCount:{queue.Count}");
                                }
                                else
                                {
                                    _logger.Warn($"异常字符{q.ToHexString()}, QCount:{queue.Count}");
                                }
                            break;
                        }
                        case 1:
                        case 2:
                        case 3:
                        {
                            if (Bag.CheckHead(Bag.Head.Count))
                            {
                                byte q;
                                var has = queue.TryDequeue(out q);
                                if (has)
                                {
                                    Bag.Head.Add(q);
                                    _logger.Trace($"{Bag.Head.Count}>头部解析:{Bag.Head.ToHexString()}, QCount:{queue.Count}");
                                }
                            }
                            else
                            {
                                _logger.Warn($"异常字符{Bag.Head.ToHexString()}, QCount:{queue.Count}");
                                Bag.Head.Clear();
                            }
                            break;
                        }
                    }
                }
                else if (Bag.Head.Count == 4 && !isData) //HXYF
                {
                    if (Bag.CheckHead())
                    {
                        byte q;
                        var has = queue.TryDequeue(out q);
                        if (has)
                        {
                            Bag.Address = q;
                            isData = true;
                            _logger.Trace($"5>地址:{Bag.Address}, QCount:{queue.Count}");
                        }
                    }
                    else
                    {
                        Bag.Head.Clear();
                    }
                }
                else
                {
                    if (isData && length == -1)
                    {
                        byte q;
                        var has = queue.TryDequeue(out q);
                        if (has)
                        {
                            length = q;
                            Bag.Length = length;
                            _logger.Trace($"6>数据长度:{length},QCount:{queue.Count}");
                        }
                    }
                    else if (isData && length > 0)
                    {
                        byte q;
                        var has = queue.TryDequeue(out q);
                        if (has)
                        {
                            Bag.Content.Add(q);
                            length--;
                            if (length == 0)
                            {
                                Bag.CheckCRC();//TODO:做CRC验证
                                complate = true;
                                _logger.Trace($"8>当次解析完成,QCount:{queue.Count}");
                            }
                        }
                    }
                }
                if (complate)
                {
                    _logger.Info($"{Bag.ToFormateString()}");
                    OnCompleted(new HxBagEventArgs(Bag.Clone()));
                    Bag.Clear();
                    length = -1;
                    isData = false;
                    complate = false;
#if DEBUG
                    if (IsDebug && queue.Count <= 0)
                        DataArrivedThreadStarted = false;
#endif
                }
            }
        }

        public event EventHandler<HxBagEventArgs> Completed;

        protected virtual void OnCompleted(HxBagEventArgs e)
        {
            Completed?.Invoke(this, e);
        }

        public class HxBagEventArgs : EventArgs
        {
            public HxBagEventArgs(Data bag)
            {
                Bag = bag;
            }

            public Data Bag { get; set; }
        }

        public class Data
        {
            public const byte Head1 = 0x68;
            public const byte Head2 = 0x78;
            public const byte Head3 = 0x79;
            public const byte Head4 = 0x66;

            public List<byte> Head { get; set; } = new List<byte>(4);
            public byte Address { get; set; }
            public short Length { get; set; } = -1;
            public List<byte> Content { get; set; } = new List<byte>();

            public byte[] CRC { get; set; } = new byte[2];

            public void Clear()
            {
                Head.Clear();
                Address = 0;
                Length = -1;
                Content.Clear();
                CRC = new byte[2];
            }

            public void CheckCRC()
            {
                if (Content.Count > 2)
                {
                    CRC[0] = Content[Content.Count - 2];
                    CRC[1] = Content[Content.Count - 1];
                    Content.RemoveRange(Content.Count - 2, 2);
                }
            }

            public bool CheckHead(int index = 0)
            {
                switch (index)
                {
                    case 0:
                    case 4:
                        return Head[0] == Head1 && Head[1] == Head2 && Head[2] == Head3 && Head[3] == Head4;
                    case 1:
                        return Head[0] == Head1;
                    case 2:
                        return Head[0] == Head1 && Head[1] == Head2;
                    case 3:
                        return Head[0] == Head1 && Head[1] == Head2 && Head[2] == Head3;
                    default:
                        return false;
                }
            }

            public byte[] GetValue()
            {
                byte[] value = new byte[0];
                if (Content.Count > 0)
                {
                    value = new byte[Content.Count - 2];
                    Buffer.BlockCopy(Content.ToArray(), 2, value, 0, value.Length);
                }
                return value;
            }

            public string ToFormateString()
            {
                var method = string.Empty;
                var channel = string.Empty;
                if (Content.Count > 0)
                {
                    method = Content[0].ToHexString();
                    channel = Content[1].ToHexString();
                }
                return $"ADD:{Address.ToHexString()}," +
                       $"Length:{Length}, " +
                       $"Method:{method}," +
                       $"Channel:{channel}," +
                       $"Value:{GetValue().ToHexString()}," +
                       $"CRC:{CRC.ToHexString()}";
            }

            /// <summary>创建作为当前实例副本的新对象。</summary>
            /// <returns>作为此实例副本的新对象。</returns>
            public Data Clone()
            {
                var bag = new Data();
                bag.Content = new List<byte>(Content);
                bag.Address = Address;
                bag.CRC = new List<byte>(CRC).ToArray();
                bag.Head = new List<byte>(Head);
                bag.Length = bag.Length;
                return bag;
            }
        }
    }
}