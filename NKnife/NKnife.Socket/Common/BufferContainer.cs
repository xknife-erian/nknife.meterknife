using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace SocketKnife.Common
{
    /// <summary>
    ///     一个数据包容器, 将SocketAsyncEventArgs的缓存预先打开备用。
    /// </summary>
    public sealed class BufferContainer
    {
        private readonly int _BufferSize;
        private readonly int _NumSize;
        private byte[] _Buffer;
        private int _CurrentIndex;
        private Stack<int> _FreeIndexPool;

        public BufferContainer(int numsize, int buffersize)
        {
            _NumSize = numsize;
            _BufferSize = buffersize;
        }

        public void Initialize()
        {
            _Buffer = new byte[_NumSize];
            _FreeIndexPool = new Stack<int>(_NumSize/_BufferSize);
        }

        internal void FreeBuffer(SocketAsyncEventArgs args)
        {
            _FreeIndexPool.Push(args.Offset);
            args.SetBuffer(null, 0, 0);
        }

        internal Boolean SetBuffer(SocketAsyncEventArgs args)
        {
            if (_FreeIndexPool.Count > 0)
            {
                args.SetBuffer(_Buffer, _FreeIndexPool.Pop(), _BufferSize);
            }
            else
            {
                if ((_NumSize - _BufferSize) < _CurrentIndex)
                    return false;
                args.SetBuffer(_Buffer, _CurrentIndex, _BufferSize);
                _CurrentIndex += _BufferSize;
            }
            return true;
        }
    }
}