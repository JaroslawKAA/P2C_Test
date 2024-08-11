using System;

public class AgentsCountChangedEventListener : EventListener
{
    public AgentsCountChangedEventListener(Action<EventBase> onEventInvoked) : base(onEventInvoked)
    {
    }
}
