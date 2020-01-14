using System;

namespace NKnife.Algrithm
{
    public class OperationResult : EventArgs
    {
        public int ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
        public OperationResult(int errorCode, string errorDescription)
        {
            ErrorCode = errorCode;
            ErrorDescription = errorDescription;
        }
    }
}