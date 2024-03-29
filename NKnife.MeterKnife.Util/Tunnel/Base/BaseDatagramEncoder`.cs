﻿namespace NKnife.MeterKnife.Util.Tunnel.Base
{
    public abstract class BaseDatagramEncoder<TData> : IDatagramEncoder<TData>
    {
        public abstract byte[] Execute(TData data);
    }
}