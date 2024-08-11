using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentService : MonoBehaviour, IAgentService
{
    // PRIVATE
    Dictionary<Guid, Agent> agents = new();

    SpawnAgentEvent SpawnAgentEvent = new();
    ReleaseAgentEvent ReleaseAgentEvent = new();

    SpawnedAgentEventListener SpawnedAgentEventListener;

    // UNITY EVENT
    void Awake()
    {
        SpawnedAgentEventListener = new SpawnedAgentEventListener(OnAgentSpawned);
        EventManager.Instance.RegisterListener<SpawnedAgentEvent>(SpawnedAgentEventListener);
    }


    void OnDestroy()
    {
        EventManager.Instance.UnregisterListener<SpawnedAgentEvent>(SpawnedAgentEventListener);

        SpawnAgentEvent = null;
        SpawnedAgentEventListener = null;
    }

    // METHODS
    public void SpawnAgent() => EventManager.Instance.TriggerEvent(SpawnAgentEvent);
    
    public void ReleaseAgent(Agent agentInstance)
    {
        if (agents.ContainsKey(agentInstance.GUID))
        {
            ReleaseAgentEvent.ReleasedAgent = agentInstance;
            EventManager.Instance.TriggerEvent(ReleaseAgentEvent);

            agents.Remove(agentInstance.GUID);
        }
    }

    public void ReleaseAgents()
    {
        foreach (Agent agent in agents.Values.ToArray())
        {
            ReleaseAgent(agent);
        }
    }

    void RegisterAgent(Agent agentInstance)
    {
        agents.Add(agentInstance.GUID, agentInstance);
    }

    void OnAgentSpawned(Agent agentInstance) => RegisterAgent(agentInstance);
}