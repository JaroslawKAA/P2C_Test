using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Core.Services
{
    public class AgentService : IAgentService
    {
        // PRIVATE
        Dictionary<Guid, Agent> agents = new();

        Random random = new();

        EventListener spawnedAgentEventListener;

        // CTORs
        public AgentService()
        {
            spawnedAgentEventListener = new EventListener(OnAgentSpawned);
            EventManager.Instance.RegisterListener<SpawnedAgentEvent>(spawnedAgentEventListener);
        }

        ~AgentService()
        {
            EventManager.Instance.UnregisterListener<SpawnedAgentEvent>(spawnedAgentEventListener);

            spawnedAgentEventListener = null;
        }

        // METHODS
        [Button]
        public void SpawnAgent() => EventManager.Instance.TriggerEvent(new SpawnAgentEvent());

        [Button]
        public void ReleaseAgent(Agent agentInstance)
        {
            if (agents.ContainsKey(agentInstance.GUID))
            {
                EventManager.Instance.TriggerEvent(new ReleaseAgentEvent(agentInstance));

                agents.Remove(agentInstance.GUID);
                EventManager.Instance.TriggerEvent(new AgentsCountChangedEvent(agents.Count));
            }
        }

        [Button]
        public void ReleaseRandomAgent()
        {
            if(agents.Count > 0)
            {
                Agent randomAgent = agents.ElementAt(random.Next(agents.Count)).Value;
                agents.Remove(randomAgent.GUID);
                EventManager.Instance.TriggerEvent(new ReleaseAgentEvent(randomAgent));
                EventManager.Instance.TriggerEvent(new AgentsCountChangedEvent(agents.Count));
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
            foreach (Agent agent in agents.Values.ToArray()) ReleaseAgent(agent);
        }

        void RegisterAgent(Agent agentInstance)
        {
            agents.Add(agentInstance.GUID, agentInstance);
        }

        void OnAgentSpawned(EventBase eventBase)
        {
            SpawnedAgentEvent spawnedAgentEvent = eventBase as SpawnedAgentEvent;
            RegisterAgent(spawnedAgentEvent.SpawnedAgent);
            
            EventManager.Instance.TriggerEvent(new AgentsCountChangedEvent(agents.Count));
        }
    }
}