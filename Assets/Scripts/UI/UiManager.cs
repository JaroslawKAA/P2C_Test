using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    }

    void UnsubscribeGameEvents()
    {
        EventManager.Instance.UnregisterListener<AgentsCountChangedEvent>(agentsCountChangedEventListener);
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

    void AddAgent() => EventManager.Instance.TriggerEvent(new AddAgentRequestEvent());
    void RemoveRandomAgent() => EventManager.Instance.TriggerEvent(new RemoveAgentRequestEvent());
    void ClearAllAgents() => EventManager.Instance.TriggerEvent(new ClearAllAgentsRequestEvent());

    void SpeedUp()
    {
        throw new NotImplementedException();
    }

    void SpeedDown()
    {
        throw new NotImplementedException();
    }

    void Pause()
    {
        throw new NotImplementedException();
    }
}