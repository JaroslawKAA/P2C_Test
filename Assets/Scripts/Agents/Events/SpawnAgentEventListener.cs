using System;

public class SpawnAgentEventListener : EventListener
{
    public SpawnAgentEventListener(Action<EventBase> onEventInvoked) : base(onEventInvoked)
    {
    }
}
