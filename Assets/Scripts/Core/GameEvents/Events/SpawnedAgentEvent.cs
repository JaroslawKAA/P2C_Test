using System;

public class SpawnedAgentEvent : EventBase
{
    public Guid SpawnedAgent { get; private set; }

    public SpawnedAgentEvent(Guid spawnedAgent)
    {
        SpawnedAgent = spawnedAgent;
    }
}
