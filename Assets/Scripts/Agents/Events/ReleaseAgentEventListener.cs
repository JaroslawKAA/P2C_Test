using System;

public class ReleaseAgentEventListener : EventListener
{
    public ReleaseAgentEventListener(Action<EventBase> onEventInvoked) : base(onEventInvoked)
    {
    }
}