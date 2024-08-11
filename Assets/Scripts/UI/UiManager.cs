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
    AgentsCountChangedEventListener agentsCountChangedEventListener;
    
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
        agentsCountChangedEventListener = new AgentsCountChangedEventListener(OnAgentsCountChanged);
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

    void AddAgent()
    {
        throw new NotImplementedException();
    }

    void RemoveRandomAgent()
    {
        throw new NotImplementedException();
    }
    
    void ClearAllAgents()
    {
        throw new NotImplementedException();
    }

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
        
    }

    
}
