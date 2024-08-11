using System;

public class SpawnedAgentEventListener : IEventListener
{
    Action<Agent> agentSpawnedAction;
    
    public SpawnedAgentEventListener(Action<Agent> agentSpawnedAction)
    {
        this.agentSpawnedAction = agentSpawnedAction;
    }
    
    public void OnEvent(EventBase eventInstance)
    {
        SpawnedAgentEvent spawnedAgentEvent = eventInstance as SpawnedAgentEvent;
        agentSpawnedAction?.Invoke(spawnedAgentEvent.SpawnedAgent);
    }
}
