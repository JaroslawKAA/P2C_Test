using Core.Services;
using Sirenix.OdinInspector;

public class GameManager : SerializedMonoBehaviour
{
    // SERIALIZED

    // PRIVATE
    IAgentService agentService;
    ITickService tickService;

    EventListener addAgentRequestEventListener;
    EventListener removeAgentRequestEventListener;
    EventListener clearAllAgentsRequestEventListener;

    // UNITY EVENTS
    void Start()
    {
        agentService = new AgentService();
        tickService = new TickService();
        
        addAgentRequestEventListener = new EventListener(OnAddAgentRequest);
        EventManager.Instance.RegisterListener<AddAgentRequestEvent>(addAgentRequestEventListener);

        removeAgentRequestEventListener = new EventListener(OnRemoveAgentRequest);
        EventManager.Instance.RegisterListener<RemoveAgentRequestEvent>(removeAgentRequestEventListener);

        clearAllAgentsRequestEventListener = new EventListener(OnClearAllAgentRequest);
        EventManager.Instance.RegisterListener<ClearAllAgentsRequestEvent>(clearAllAgentsRequestEventListener);
    }

    void OnDestroy()
    {
        EventManager.Instance.UnregisterListener<AddAgentRequestEvent>(addAgentRequestEventListener);
        EventManager.Instance.UnregisterListener<RemoveAgentRequestEvent>(removeAgentRequestEventListener);
        EventManager.Instance.UnregisterListener<ClearAllAgentsRequestEvent>(clearAllAgentsRequestEventListener);
    }

    // METHODS
    void OnAddAgentRequest(EventBase obj) => agentService.SpawnAgent();
    void OnRemoveAgentRequest(EventBase obj) => agentService.ReleaseRandomAgent();
    void OnClearAllAgentRequest(EventBase obj) => agentService.ReleaseAgents();
}