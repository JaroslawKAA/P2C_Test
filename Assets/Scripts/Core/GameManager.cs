using Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    // SERIALIZED
    [Title("Config")]
    [SerializeField] int agentsToSpawn = 10;

    // PRIVATE
    IAgentService agentService;
    ITickService tickService;

    // UNITY EVENTS
    void Start()
    {
        agentService = new AgentService();
        tickService = new TickService();
        
        SpawnAgents();
    }

    // METHODS
    [Button] void SpawnAgent() => agentService.SpawnAgent();
    [Button] void RealiseAgent(Agent agent) => agentService.ReleaseAgent(agent);
    [Button] void SpawnAgents() => agentService.SpawnAgents(agentsToSpawn);
    [Button] void ReleaseAgents() => agentService.ReleaseAgents();
}