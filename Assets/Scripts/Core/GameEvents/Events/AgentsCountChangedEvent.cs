namespace Core.GameEvents.Events
{
    public class AgentsCountChangedEvent : EventBase
    {
        public int AgentsCount { get; private set; }

        public AgentsCountChangedEvent(int agentsCount)
        {
            AgentsCount = agentsCount;
        }
    }
}
