using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;

namespace NKnife.Socket.Common
{
    /// <summary>
    ///     Based on example from:
    ///     http://msdn2.microsoft.com/en-us/library/system.net.sockets.socketasynceventargs.socketasynceventargs.aspx
    ///     Represents a collection of reusable SocketAsyncEventArgs objects.
    /// </summary>
    internal sealed class SocketAsyncEventArgsPool : IEnumerable<SocketAsyncEventArgs>
    {
        /// <summary>
        ///     SocketAsyncEventArgs栈
        /// </summary>
        private readonly Stack<SocketAsyncEventArgs> _Pool;

        /// <summary>
        ///     初始化SocketAsyncEventArgs池
        /// </summary>
        /// <param name="capacity">最大可能使用的SocketAsyncEventArgs对象.</param>
        internal SocketAsyncEventArgsPool(Int32 capacity)
        {
            _Pool = new Stack<SocketAsyncEventArgs>(capacity);
        }

        /// <summary>
        ///     返回SocketAsyncEventArgs池中的 数量
        /// </summary>
        internal int Count
        {
            get { return _Pool.Count; }
        }

        public IEnumerator<SocketAsyncEventArgs> GetEnumerator()
        {
            return _Pool.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _Pool.GetEnumerator();
        }

        internal void Clear()
        {
            lock (_Pool)
            {
                _Pool.Clear();
            }
        }

        /// <summary>
        ///     弹出一个SocketAsyncEventArgs
        /// </summary>
        /// <returns>SocketAsyncEventArgs removed from the pool.</returns>
        internal SocketAsyncEventArgs Pop()
        {
            lock (_Pool)
            {
                return _Pool.Pop();
            }
        }

        /// <summary>
        ///     添加一个 SocketAsyncEventArgs
        /// </summary>
        /// <param name="item">SocketAsyncEventArgs instance to add to the pool.</param>
        internal void Push(SocketAsyncEventArgs item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(@"Items added to a SocketAsyncEventArgsPool cannot be null");
            }
            lock (_Pool)
            {
                _Pool.Push(item);
            }
        }
    }
}