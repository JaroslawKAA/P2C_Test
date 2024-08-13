namespace Core.GameEvents.Events
{
    public class AgentMessageEvent : EventBase
    {
        public string Message { get; private set; }

        public AgentMessageEvent(string message)
        {
            Message = message;
        }
    }
}
