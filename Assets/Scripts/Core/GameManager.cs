using Core.GameEvents;
using Core.GameEvents.Events;
using Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    public class GameManager : SerializedMonoBehaviour
    {
        // SERIALIZED
        [Title("Config")]
        [SerializeField] float timeScaleAdd = .25f;

        // PRIVATE
        IAgentService agentService;
        ITickService tickService;
        ITimeScaleService timeScaleService;

        EventListener addAgentRequestEventListener;
        EventListener removeAgentRequestEventListener;
        EventListener clearAllAgentsRequestEventListener;
        EventListener increaseTimeScaleEventListener;
        EventListener decreaseTimeScaleEventListener;
        EventListener pauseGameEventListener;

        // UNITY EVENTS
        void Start()
        {
            agentService = new AgentService();
            tickService = new TickService();
            timeScaleService = new TimeScaleService();
        
            addAgentRequestEventListener = new EventListener(OnAddAgentRequest);
            EventManager.Instance.RegisterListener<AddAgentRequestEvent>(addAgentRequestEventListener);

            removeAgentRequestEventListener = new EventListener(OnRemoveAgentRequest);
            EventManager.Instance.RegisterListener<RemoveAgentRequestEvent>(removeAgentRequestEventListener);

            clearAllAgentsRequestEventListener = new EventListener(OnClearAllAgentRequest);
            EventManager.Instance.RegisterListener<ClearAllAgentsRequestEvent>(clearAllAgentsRequestEventListener);

            increaseTimeScaleEventListener = new EventListener(OnIncreaseTimeScaleRequest);
            EventManager.Instance.RegisterListener<IncreaseTimeScaleEvent>(increaseTimeScaleEventListener);

            decreaseTimeScaleEventListener = new EventListener(OnDecreaseTimeScaleRequest);
            EventManager.Instance.RegisterListener<DecreaseTimeScaleEvent>(decreaseTimeScaleEventListener);

            pauseGameEventListener = new EventListener(OnPauseRequest);
            EventManager.Instance.RegisterListener<PauseGameEvent>(pauseGameEventListener);
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
        void OnIncreaseTimeScaleRequest(EventBase obj) => timeScaleService.IncreaseTimeScale(timeScaleAdd);
        void OnDecreaseTimeScaleRequest(EventBase obj) => timeScaleService.DecreaseTimeScale(timeScaleAdd);
        void OnPauseRequest(EventBase obj) => timeScaleService.SetTimeScale(0f);
    }
}