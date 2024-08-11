using System;

public class SpawnedAgentEventListener : EventListener
{
    public SpawnedAgentEventListener(Action<EventBase> onEventInvoked) : base(onEventInvoked)
    {
    }
}
