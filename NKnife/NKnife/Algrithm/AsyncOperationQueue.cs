using System.Collections.Concurrent;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace NKnife.Algrithm
{
    public class AsyncOperationQueue
    {
        private static readonly object _Lock = new object();
        private readonly Timer _CheckOperationTmr = new Timer();
        private readonly Timer _CheckStatusTmr = new Timer();
        private readonly ConcurrentQueue<AsyncOperation> _OperationQueue = new ConcurrentQueue<AsyncOperation>();
        private readonly ManualResetEvent _Reset = new ManualResetEvent(false);

        private AsyncOperation _CurrentOperation;

        #region 单例

        private static AsyncOperationQueue _Instance;

        private AsyncOperationQueue()
        {
            _CheckStatusTmr.Interval = 500;
            _CheckStatusTmr.Elapsed += CheckStatusTmrElapsed;
            _CheckStatusTmr.Start();

            _CheckOperationTmr.Interval = 200;
            _CheckOperationTmr.Elapsed += CheckOperationTmrElapsed;
            _CheckOperationTmr.Start();
        }

        public static AsyncOperationQueue Instance()
        {
            lock (_Lock)
            {
                return _Instance ?? (_Instance = new AsyncOperationQueue());
            }
        }

        #endregion

        #region 内部循环

        /// <summary>检查是否有新的异步调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOperationTmrElapsed(object sender, ElapsedEventArgs e)
        {
            if (!_OperationQueue.TryDequeue(out _CurrentOperation))
            {
                return;
            }

            _CheckOperationTmr.Stop();
            _Reset.Reset();
            if (!_Reset.WaitOne(_CurrentOperation.OperationTimeout)) //没收到信号，超时
            {
                _CurrentOperation.OperationResultArrived(new OperationResult(1, "超时"));
                _CurrentOperation = null;
            }
            else //收到信号
            {
                _CurrentOperation.OperationResultArrived(new OperationResult(0, "成功"));
                _CurrentOperation = null;
            }
            _CheckOperationTmr.Start();
        }


        /// <summary>检查当前调用的结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckStatusTmrElapsed(object sender, ElapsedEventArgs e)
        {
            if (_CurrentOperation == null) return;
            if (_CurrentOperation.CheckStatusFunc(_CurrentOperation.Id)) //达到预想的状态了
            {
                _Reset.Set();
            }
        }

        #endregion

        /// <summary>加入异步调用（不一定立刻执行，得等当前异步调用完成后才开始）
        /// </summary>
        /// <param name="asyncOperation"></param>
        public void Enter(AsyncOperation asyncOperation)
        {
            _OperationQueue.Enqueue(asyncOperation);
        }

        /// <summary>检查是否有异步调用正在进行
        /// </summary>
        /// <returns></returns>
        public bool AnyOperation()
        {
            return _CurrentOperation != null;
        }
    }
}