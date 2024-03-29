﻿namespace NKnife.MeterKnife.Common.Scpi.Parser
{
    /**
     * Interface to define a handler for a SCPI command. Implementations of this
     * interface are passed to the {@link #addHandler addHandler} method. Refer to the
     * {@code SCPIParser} class documentation for an example.
     */
    public interface IScpiCommandHandler
    {

        string Handle(string[] args);
    }
}
