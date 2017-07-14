﻿using NKnife.Interface;

namespace MeterKnife.Interfaces.Plugin
{
    public interface IPluginManager : IEnvironmentItem
    {
        void RegistPlugIns(params IPlugIn[] plugIn);
    }
}