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

    Transform cachedTransform;


    // UNITY EVENTS
    void Awake()
    {
        cachedTransform = transform;
        
        agentPool = new LinkedPool<Agent>(createFunc: Pool_Spawn, actionOnGet: Pool_OnGet, actionOnRelease: Pool_OnRelease);

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

        cachedTransform = null;
    }


    // METHODS

    void Spawn()
    {
        Agent agent = agentPool.Get();
        agent.SetParent(cachedTransform);
        SpawnedAgentEvent.SpawnedAgent = agent;
        EventManager.Instance.TriggerEvent(SpawnedAgentEvent);
    }

    void OnAgentReleased(Agent agentToRelease)
    {
        agentPool.Release(agentToRelease);
    }

    Agent Pool_Spawn()
    {
        Agent agentInstance = Instantiate(agentPrefab);
        agentInstance.OnInstantiated();
        return agentInstance;
    }

    void Pool_OnGet(Agent agent) => agent.OnGet();
    void Pool_OnRelease(Agent agent)
    {
        agent.OnRelease();
        agent.SetParent(cachedTransform);
    }
}