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
        EventListener agentReachedDestinationListener;

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
            EventManager.RegisterListener<AgentsCountChangedEvent>(agentsCountChangedEventListener);

            timeScaleChangedEventListener = new EventListener(OnTimeScaleChanged);
            EventManager.RegisterListener<TimeScaleChangedEvent>(timeScaleChangedEventListener);

            agentReachedDestinationListener = new EventListener(OnAgentReachedDestination);
            EventManager.RegisterListener<AgentMessageEvent>(agentReachedDestinationListener);
        }

        void UnsubscribeGameEvents()
        {
            EventManager.UnregisterListener<AgentsCountChangedEvent>(agentsCountChangedEventListener);
            EventManager.UnregisterListener<TimeScaleChangedEvent>(timeScaleChangedEventListener);
            EventManager.UnregisterListener<AgentMessageEvent>(agentReachedDestinationListener);

            agentsCountChangedEventListener = null;
            timeScaleChangedEventListener = null;
            agentReachedDestinationListener = null;
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
        
        void OnAgentReachedDestination(EventBase eventBase)
        {
            AgentMessageEvent agentMessageEvent = eventBase as AgentMessageEvent;
            messagesText.text = agentMessageEvent.Message;
        }
        
        void AddAgent() => EventManager.TriggerEvent(new AddAgentRequestEvent());
        void RemoveRandomAgent() => EventManager.TriggerEvent(new RemoveAgentRequestEvent());
        void ClearAllAgents() => EventManager.TriggerEvent(new ClearAllAgentsRequestEvent());
        void SpeedUp() => EventManager.TriggerEvent(new IncreaseTimeScaleEvent());
        void SpeedDown() => EventManager.TriggerEvent(new DecreaseTimeScaleEvent());
        void Pause() => EventManager.TriggerEvent(new PauseGameEvent());
    }
}