using System;

public abstract class EventListener
{
    Action<EventBase> onEventInvoked;

    protected EventListener(Action<EventBase> onEventInvoked) => this.onEventInvoked = onEventInvoked;

    public virtual void OnEvent(EventBase eventInstance) => onEventInvoked?.Invoke(eventInstance);
}
