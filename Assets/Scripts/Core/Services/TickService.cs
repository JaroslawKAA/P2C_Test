using System.Collections;
using Core.GameEvents;
using Core.GameEvents.Events;
using UnityEngine;

namespace Core.Services
{
    public class TickService : ITickService
    {
        float tickDelay;
        
        IEnumerator tickRoutine;
        
        public TickService(float tickDelay)
        {
            this.tickDelay = tickDelay;
            tickRoutine = TickRoutine();
            CoroutineHandle.Instance.StartCoroutine(tickRoutine);
        }

        ~TickService()
        {
            CoroutineHandle.Instance.StopCoroutine(tickRoutine);
        }

        IEnumerator TickRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(tickDelay);
                Tick();
            }
        }

        public void Tick()
        {
            Debug.Log("Tick");
            EventManager.TriggerEvent(new TickEvent());
        }

        public void SetTickDelay(float tickDelay) => this.tickDelay = tickDelay;
    }
}
