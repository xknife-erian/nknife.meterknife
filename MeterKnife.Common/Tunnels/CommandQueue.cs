using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NKnife.Base;
using NKnife.Collections;
using ScpiKnife;

namespace MeterKnife.Common.Tunnels
{
    /// <summary>
    /// 面向指定的CarePort的命令队列
    /// </summary>
    public class CommandQueue : SyncQueue<CommandQueue.CareItem>
    {
        private readonly AutoResetEvent _AutoResetEvent = new AutoResetEvent(false);

        public CommandQueue()
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
        /// 命令封装
        /// </summary>
        public class CareItem
        {
            public CareItem()
            {
                IsCare = false;
                GpibAddress = 0;
            }

            /// <summary>
            /// 是否是Care的专属协议
            /// </summary>
            public bool IsCare { get; set; }
            /// <summary>
            /// 仪器的SCPI指令
            /// </summary>
            public ScpiCommand ScpiCommand { get; set; }
            /// <summary>
            /// 命令指向的仪器GPIB地址
            /// </summary>
            public short GpibAddress { get; set; }
            /// <summary>
            /// 当是Care的专属协议时的主命令字与子命令字
            /// </summary>
            public Pair<byte,byte> Heads { get; set; }
            /// <summary>
            /// 当是Care的专属协议时的协议主体内容
            /// </summary>
            public byte[] Content { get; set; }
        }

    }
}
