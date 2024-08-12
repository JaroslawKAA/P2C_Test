using System;

public interface IAgentService
{
    void SpawnAgent();
    void SpawnAgents(int count);
    void ReleaseRandomAgent();
    void ReleaseAgent(Guid agentGuid);
    void ReleaseAgents();
}
