using System.Threading;
using NKnife.Base;
using NKnife.Collections;

namespace ScpiKnife
{
    /// <summary>
    ///     面向指定的CarePort的命令队列
    /// </summary>
    public class ScpiCommandQueue : SyncQueue<ScpiCommandQueue.Item>
    {
        private readonly AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);

        public ScpiCommandQueue()
        {
            _AutoResetEvent.Set();
        }

        public void Sleep(int interval)
        {
            _AutoResetEvent.WaitOne(interval);
        }

        public void Reset()
        {
            _AutoResetEvent.Reset();
        }

        /// <summary>
        ///     命令封装
        /// </summary>
        public class Item
        {
            public Item()
            {
                IsCare = false;
                Interval = 10;
                GpibAddress = 0;
            }

            /// <summary>
            ///     是否是Care的专属协议
            /// </summary>
            public bool IsCare { get; set; }

            /// <summary>
            ///     仪器的SCPI指令
            /// </summary>
            public ScpiCommand ScpiCommand { get; set; }

            /// <summary>
            ///     命令指向的仪器GPIB地址
            /// </summary>
            public short GpibAddress { get; set; }

            /// <summary>
            ///     当是Care的专属协议时的主命令字与子命令字
            /// </summary>
            public Pair<byte, byte> Heads { get; set; }

            /// <summary>
            ///     当是Care的专属协议时的协议主体内容
            /// </summary>
            public byte[] Content { get; set; }

            /// <summary>
            ///     当是Care的专属协议时的等候周期
            /// </summary>
            public int Interval { get; set; }

            public static Item NullCommand()
            {
                var ci = new Item {GpibAddress = -1};
                return ci;
            }

            public Item Clone()
            {
                var item = new Item();
                item.Content = Content;
                item.GpibAddress = GpibAddress;
                item.Heads = Heads;
                item.Interval = Interval;
                item.IsCare = IsCare;
                item.ScpiCommand = ScpiCommand;
                return item;
            }
        }
    }
}