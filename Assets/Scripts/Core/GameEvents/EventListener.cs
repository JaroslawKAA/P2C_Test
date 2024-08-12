using System;

namespace Core.GameEvents
{
    public class EventListener
    {
        Action<EventBase> onEventInvoked;

        public EventListener(Action<EventBase> onEventInvoked) => this.onEventInvoked = onEventInvoked;

        public virtual void OnEvent(EventBase eventInstance) => onEventInvoked?.Invoke(eventInstance);
    }
}
