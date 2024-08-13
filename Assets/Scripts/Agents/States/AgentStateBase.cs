using UnityEngine;

namespace Agents.States
{
    public abstract class AgentStateBase : StateBase
    {
        protected AgentStateMachine agentStateMachine;
        
        protected AgentStateBase(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
            agentStateMachine = stateMachineBase as AgentStateMachine;
        }
    }
}
