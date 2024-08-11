public class SpawnedAgentEvent : EventBase
{
    public Agent SpawnedAgent { get; private set; }

    public SpawnedAgentEvent(Agent spawnedAgent)
    {
        SpawnedAgent = spawnedAgent;
    }
}
