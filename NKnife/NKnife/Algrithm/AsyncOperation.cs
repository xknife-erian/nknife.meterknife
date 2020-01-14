using System;

namespace NKnife.Algrithm
{
    public class AsyncOperation : IAsyncOperation
    {
        public Guid Id { get; private set; }
        public Func<Guid,bool> CheckStatusFunc { get; private set; }
        public Action<OperationResult> OperationResultArrived { get; private set; }
        public int OperationTimeout { get; private set; }
        public AsyncOperation(Func<Guid, bool> checkStatusFunc, Action<OperationResult> operationResultArrived, int operationTimeout)
        {
            Id = Guid.NewGuid();
            CheckStatusFunc = checkStatusFunc;
            OperationResultArrived = operationResultArrived;
            OperationTimeout = operationTimeout;
        }
    }
}
