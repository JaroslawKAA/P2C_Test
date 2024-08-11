using System;

public class SpawnAgentEventListener : IEventListener
{
    Action onSpawnRequested;
    public SpawnAgentEventListener(Action onSpawnRequested) => this.onSpawnRequested = onSpawnRequested;
    public void OnEvent(EventBase eventInstance) => onSpawnRequested?.Invoke();
}
