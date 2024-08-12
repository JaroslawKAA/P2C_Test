namespace Core.GameEvents.Events
{
    public class TimeScaleChangedEvent : EventBase
    {
        public float TimeScale { get; private set; }

        public TimeScaleChangedEvent(float timeScale)
        {
            TimeScale = timeScale;
        }
    }
}
