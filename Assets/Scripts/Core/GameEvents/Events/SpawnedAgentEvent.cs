using System;

namespace Core.GameEvents.Events
{
    public class SpawnedAgentEvent : EventBase
    {
        public Guid SpawnedAgent { get; private set; }

        public SpawnedAgentEvent(Guid spawnedAgent)
        {
            SpawnedAgent = spawnedAgent;
        }
    }
}
