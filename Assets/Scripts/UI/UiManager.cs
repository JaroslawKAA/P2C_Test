using Core.GameEvents;
using Core.GameEvents.Events;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] TextRecord agentsCountText;
        [SerializeField] [Required] Button addAgentButton;
        [SerializeField] [Required] Button removeRandomAgentButton;
        [SerializeField] [Required] Button clearAllAgentsButton;
        [SerializeField] [Required] TextRecord gameSpeedText;
        [SerializeField] [Required] Button speedUpButton;
        [SerializeField] [Required] Button speedDownButton;
        [SerializeField] [Required] Button pauseButton;

        [PropertySpace]
        [SerializeField] [Required] TMP_Text messagesText;
    
        // PRIVATE
        EventListener agentsCountChangedEventListener;
        EventListener timeScaleChangedEventListener;
    
        // UNITY EVENTS
        void Awake()
        {
            SubscribeButtons();
            SubscribeGameEvents();
        }

        void OnDestroy()
        {
            UnsubscribeButtons();
            UnsubscribeGameEvents();
        }

        // METHODS
        void SubscribeGameEvents()
        {
            agentsCountChangedEventListener = new EventListener(OnAgentsCountChanged);
            EventManager.Instance.RegisterListener<AgentsCountChangedEvent>(agentsCountChangedEventListener);

            timeScaleChangedEventListener = new EventListener(OnTimeScaleChanged);
            EventManager.Instance.RegisterListener<TimeScaleChangedEvent>(timeScaleChangedEventListener);
        }

        void UnsubscribeGameEvents()
        {
            EventManager.Instance.UnregisterListener<AgentsCountChangedEvent>(agentsCountChangedEventListener);
            EventManager.Instance.UnregisterListener<TimeScaleChangedEvent>(timeScaleChangedEventListener);
        }

        void SubscribeButtons()
        {
            addAgentButton.onClick.AddListener(AddAgent);
            removeRandomAgentButton.onClick.AddListener(RemoveRandomAgent);
            clearAllAgentsButton.onClick.AddListener(ClearAllAgents);
            speedUpButton.onClick.AddListener(SpeedUp);
            speedDownButton.onClick.AddListener(SpeedDown);
            pauseButton.onClick.AddListener(Pause);
        }

        void UnsubscribeButtons()
        {
            addAgentButton.onClick.RemoveListener(AddAgent);
            removeRandomAgentButton.onClick.RemoveListener(RemoveRandomAgent);
            clearAllAgentsButton.onClick.RemoveListener(ClearAllAgents);
            speedUpButton.onClick.RemoveListener(SpeedUp);
            speedDownButton.onClick.RemoveListener(SpeedDown);
            pauseButton.onClick.RemoveListener(Pause);
        }

        void OnAgentsCountChanged(EventBase eventBase)
        {
            AgentsCountChangedEvent agentsCountChangedEvent = eventBase as AgentsCountChangedEvent;
            agentsCountText.SetOutput(agentsCountChangedEvent.AgentsCount.ToString());
        }
        
        void OnTimeScaleChanged(EventBase eventBase)
        {
            TimeScaleChangedEvent timeScaleChangedEvent = eventBase as TimeScaleChangedEvent;
            gameSpeedText.SetOutput(timeScaleChangedEvent.TimeScale.ToString());
        }

        void AddAgent() => EventManager.Instance.TriggerEvent(new AddAgentRequestEvent());
        void RemoveRandomAgent() => EventManager.Instance.TriggerEvent(new RemoveAgentRequestEvent());
        void ClearAllAgents() => EventManager.Instance.TriggerEvent(new ClearAllAgentsRequestEvent());
        void SpeedUp() => EventManager.Instance.TriggerEvent(new IncreaseTimeScaleEvent());
        void SpeedDown() => EventManager.Instance.TriggerEvent(new DecreaseTimeScaleEvent());
        void Pause() => EventManager.Instance.TriggerEvent(new PauseGameEvent());
    }
}