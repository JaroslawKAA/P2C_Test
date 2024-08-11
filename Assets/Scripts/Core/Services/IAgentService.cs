public interface IAgentService
{
    void SpawnAgent();
    void SpawnAgents(int count);
    void ReleaseAgent(Agent agentInstance);
    void ReleaseAgents();
}
