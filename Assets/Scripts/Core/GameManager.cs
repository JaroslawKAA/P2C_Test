using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    // SERIALIZED
    [Title("Config")]
    [SerializeField] int agentsToSpawn = 10;
    
    [Title("Services")]
    [OdinSerialize] [Required] IAgentService agentService;
    [OdinSerialize] [Required] ITickService tickService;

    // UNITY EVENTS
    void Start()
    {
        SpawnAgents();
    }
    
    // METHODS
    [Button] void SpawnAgent() => agentService.SpawnAgent();
    [Button] void RealiseAgent(Agent agent) => agentService.ReleaseAgent(agent);
    [Button] void SpawnAgents() => agentService.SpawnAgents(agentsToSpawn);
    [Button] void ReleaseAgents() => agentService.ReleaseAgents();
}
