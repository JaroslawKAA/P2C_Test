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
    [Button]
    void SpawnAgents()
    {
        for (int i = 0; i < agentsToSpawn; i++)
        {
            agentService.SpawnAgent();
        }
    }

    [Button]
    void ReleaseAgents()
    {
        agentService.ReleaseAgents();
    }
}
