using System;

public class ReleaseAgentEvent : EventBase
{
    public Guid ReleasedAgent { get; private set; }

    public ReleaseAgentEvent(Guid releasedAgent)
    {
        ReleasedAgent = releasedAgent;
    }
}
