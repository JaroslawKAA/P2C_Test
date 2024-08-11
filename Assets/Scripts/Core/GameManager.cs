using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class GameManager : SerializedMonoBehaviour
{
    [Title("Config")]
    [SerializeField] int agentsToSpawn = 10;
    
    [Title("Services")]
    [OdinSerialize] [Required] IAgentService agentService;
    [OdinSerialize] [Required] ITickService tickService;

    void Start()
    {
        for (int i = 0; i < agentsToSpawn; i++)
        {
            agentService.SpawnAgent();
        }
    }
}
