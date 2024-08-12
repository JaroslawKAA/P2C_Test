public interface IAgentService
{
    void SpawnAgent();
    void SpawnAgents(int count);
    void ReleaseRandomAgent();
    void ReleaseAgent(Agent agentInstance);
    void ReleaseAgents();
}
