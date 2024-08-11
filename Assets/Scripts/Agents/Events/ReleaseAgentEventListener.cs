using System;

public class ReleaseAgentEventListener : IEventListener
{
    Action<Agent> onAgentRelease;

    public ReleaseAgentEventListener(Action<Agent> onAgentRelease)
    {
        this.onAgentRelease = onAgentRelease;
    }
    
    public void OnEvent(EventBase eventInstance)
    {
        ReleaseAgentEvent releaseAgentEvent = eventInstance as ReleaseAgentEvent;
        onAgentRelease?.Invoke(releaseAgentEvent.ReleasedAgent);
    }
}