using Core.GameEvents;
using Core.GameEvents.Events;
using Core.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        // SERIALIZED
        [Title("Config")]
        [SerializeField] float timeScaleAdd = .25f;
        [SerializeField] float tickDelay = .1f;

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
            tickService = new TickService(tickDelay);
            timeScaleService = new TimeScaleService();
        
            addAgentRequestEventListener = new EventListener(OnAddAgentRequest);
            EventManager.RegisterListener<AddAgentRequestEvent>(addAgentRequestEventListener);

            removeAgentRequestEventListener = new EventListener(OnRemoveAgentRequest);
            EventManager.RegisterListener<RemoveAgentRequestEvent>(removeAgentRequestEventListener);

            clearAllAgentsRequestEventListener = new EventListener(OnClearAllAgentRequest);
            EventManager.RegisterListener<ClearAllAgentsRequestEvent>(clearAllAgentsRequestEventListener);

            increaseTimeScaleEventListener = new EventListener(OnIncreaseTimeScaleRequest);
            EventManager.RegisterListener<IncreaseTimeScaleEvent>(increaseTimeScaleEventListener);

            decreaseTimeScaleEventListener = new EventListener(OnDecreaseTimeScaleRequest);
            EventManager.RegisterListener<DecreaseTimeScaleEvent>(decreaseTimeScaleEventListener);

            pauseGameEventListener = new EventListener(OnPauseRequest);
            EventManager.RegisterListener<PauseGameEvent>(pauseGameEventListener);
        }
        
        void OnDestroy()
        {
            EventManager.UnregisterListener<AddAgentRequestEvent>(addAgentRequestEventListener);
            EventManager.UnregisterListener<RemoveAgentRequestEvent>(removeAgentRequestEventListener);
            EventManager.UnregisterListener<ClearAllAgentsRequestEvent>(clearAllAgentsRequestEventListener);
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