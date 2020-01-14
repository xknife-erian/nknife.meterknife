using System;

namespace NKnife.Algrithm
{
    public interface IAsyncOperation
    {
        Guid Id { get; }
        Func<Guid, bool> CheckStatusFunc { get; }
        Action<OperationResult> OperationResultArrived { get; }
        int OperationTimeout { get; }
    }
}