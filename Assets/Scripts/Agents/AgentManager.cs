using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

public class AgentManager : MonoBehaviour
{
    // SERIALIZED
    [Title("Depend")]
    [SerializeField] [Required] Agent agentPrefab;

    // PRIVATE
    SpawnedAgentEvent SpawnedAgentEvent = new();

    SpawnAgentEventListener SpawnAgentEventListener;
    ReleaseAgentEventListener ReleaseAgentEventListener;

    LinkedPool<Agent> agentPool;


    // UNITY EVENTS
    void Awake()
    {
        agentPool = new LinkedPool<Agent>(createFunc: Pool_Spawn);

        SpawnAgentEventListener = new SpawnAgentEventListener(Spawn);
        ReleaseAgentEventListener = new ReleaseAgentEventListener(OnAgentReleased);

        EventManager.Instance.RegisterListener<SpawnAgentEvent>(SpawnAgentEventListener);
        EventManager.Instance.RegisterListener<ReleaseAgentEvent>(ReleaseAgentEventListener);
    }

    void OnDestroy()
    {
        agentPool.Clear();
        agentPool = null;
        
        EventManager.Instance.UnregisterListener<SpawnAgentEvent>(SpawnAgentEventListener);
        EventManager.Instance.UnregisterListener<ReleaseAgentEvent>(ReleaseAgentEventListener);

        SpawnAgentEventListener = null;
        ReleaseAgentEventListener = null;
    }

    // METHODS
    void Spawn()
    {
        Agent agent = agentPool.Get();
        SpawnedAgentEvent.SpawnedAgent = agent;
        EventManager.Instance.TriggerEvent(SpawnedAgentEvent);
    }

    Agent Pool_Spawn()
    {
        Agent agentInstance = Instantiate(agentPrefab);
        agentInstance.OnInstantiated();
        return agentInstance;
    }

    void OnAgentReleased(Agent agentToRelease)
    {
        agentPool.Release(agentToRelease);
    }
}