using System;
using System.Collections.Generic;
using System.Linq;
using Core.GameEvents;
using Core.GameEvents.Events;
using Sirenix.OdinInspector;

namespace Core.Services
{
    public class AgentService : IAgentService
    {
        // PRIVATE
        LinkedList<Guid> agents = new();

        Random random = new();

        EventListener spawnedAgentEventListener;

        // CTORs
        public AgentService()
        {
            spawnedAgentEventListener = new EventListener(OnAgentSpawned);
            EventManager.RegisterListener<SpawnedAgentEvent>(spawnedAgentEventListener);
        }

        ~AgentService()
        {
            EventManager.UnregisterListener<SpawnedAgentEvent>(spawnedAgentEventListener);

            spawnedAgentEventListener = null;
        }

        // METHODS
        [Button]
        public void SpawnAgent() => EventManager.TriggerEvent(new SpawnAgentEvent());

        [Button]
        public void ReleaseAgent(Guid agentGuid)
        {
            if (agents.Contains(agentGuid))
            {
                EventManager.TriggerEvent(new ReleaseAgentEvent(agentGuid));

                agents.Remove(agentGuid);
                EventManager.TriggerEvent(new AgentsCountChangedEvent(agents.Count));
            }
        }

        [Button]
        public void ReleaseRandomAgent()
        {
            if(agents.Count > 0)
            {
                Guid randomAgent = agents.ElementAt(random.Next(agents.Count));
                agents.Remove(randomAgent);
                EventManager.TriggerEvent(new ReleaseAgentEvent(randomAgent));
                EventManager.TriggerEvent(new AgentsCountChangedEvent(agents.Count));
            }
        }

        [Button]
        public void SpawnAgents(int count)
        {
            for (int i = 0; i < count; i++) SpawnAgent();
        }

        [Button]
        public void ReleaseAgents()
        {
            while (agents.Count > 0)
            {
                EventManager.TriggerEvent(new ReleaseAgentEvent(agents.Last.Value));
                agents.RemoveLast();
                EventManager.TriggerEvent(new AgentsCountChangedEvent(agents.Count));
            }
        }

        void RegisterAgent(Guid agentGuid)
        {
            agents.AddFirst(agentGuid);
        }

        void OnAgentSpawned(EventBase eventBase)
        {
            SpawnedAgentEvent spawnedAgentEvent = eventBase as SpawnedAgentEvent;
            RegisterAgent(spawnedAgentEvent.SpawnedAgent);
            
            EventManager.TriggerEvent(new AgentsCountChangedEvent(agents.Count));
        }
    }
}