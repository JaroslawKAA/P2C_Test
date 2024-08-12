using Core.GameEvents;
using Core.GameEvents.Events;
using UnityEngine;

namespace Core.Services
{
    public class TimeScaleService : ITimeScaleService
    {
        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
            EventManager.TriggerEvent(new TimeScaleChangedEvent(Time.timeScale));
        }

        public float GetTimeScale() => Time.timeScale;
        
        public void IncreaseTimeScale(float timeScaleAdd)
        {
            Time.timeScale += timeScaleAdd;
            EventManager.TriggerEvent(new TimeScaleChangedEvent(Time.timeScale));
        }

        public void DecreaseTimeScale(float timeScaleSubtract)
        {
            Time.timeScale -= timeScaleSubtract;
            EventManager.TriggerEvent(new TimeScaleChangedEvent(Time.timeScale));
        }
    }
}
