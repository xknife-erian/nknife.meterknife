﻿using NKnife.MeterKnife.Util.Tunnel.Base;

namespace NKnife.MeterKnife.Util.Tunnel.Generic
{
    public abstract class StringDatagramEncoder : BaseDatagramEncoder<string>
    {
        public abstract override byte[] Execute(string replay);
    }
}