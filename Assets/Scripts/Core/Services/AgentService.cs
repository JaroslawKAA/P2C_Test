using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentService : MonoBehaviour, IAgentService
{
    // PRIVATE
    Dictionary<Guid, Agent> agents = new();

    SpawnedAgentEventListener spawnedAgentEventListener;

    // UNITY EVENT
    void Awake()
    {
        spawnedAgentEventListener = new SpawnedAgentEventListener(OnAgentSpawned);
        EventManager.Instance.RegisterListener<SpawnedAgentEvent>(spawnedAgentEventListener);
    }

    void OnDestroy()
    {
        EventManager.Instance.UnregisterListener<SpawnedAgentEvent>(spawnedAgentEventListener);

        spawnedAgentEventListener = null;
    }

    // METHODS
    public void SpawnAgent() => EventManager.Instance.TriggerEvent(new SpawnAgentEvent());

    public void ReleaseAgent(Agent agentInstance)
    {
        if (agents.ContainsKey(agentInstance.GUID))
        {
            EventManager.Instance.TriggerEvent(new ReleaseAgentEvent(agentInstance));

            agents.Remove(agentInstance.GUID);
        }
    }

    public void SpawnAgents(int count)
    {
        for (int i = 0; i < count; i++) SpawnAgent();
    }

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