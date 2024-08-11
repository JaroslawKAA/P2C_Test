using UnityEngine;
using UnityEngine.Pool;

public class AgentManager : MonoBehaviour
{
    // SERIALIZED
    [SerializeField] Agent agentPrefab;

    // PRIVATE
    LinkedPool<Agent> agentPool;
    

    // UNITY EVENTS
    void Awake()
    {
        agentPool = new LinkedPool<Agent>(createFunc: Pool_Spawn);
    }

    void OnDestroy()
    {
        agentPool.Clear();
        agentPool = null;
    }

    // METHODS
    Agent Spawn() => agentPool.Get();
    
    Agent Pool_Spawn()
    {
        Agent agentInstance = Instantiate(agentPrefab);
        return agentInstance;
    }
}