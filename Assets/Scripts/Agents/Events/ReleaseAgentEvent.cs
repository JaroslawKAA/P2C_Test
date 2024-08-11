public class ReleaseAgentEvent : EventBase
{
    public Agent ReleasedAgent { get; private set; }

    public ReleaseAgentEvent(Agent releasedAgent)
    {
        ReleasedAgent = releasedAgent;
    }
}
