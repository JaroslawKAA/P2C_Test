using UnityEngine;

namespace Agents.States
{
    public class IdleState : AgentStateBase
    {
        AgentConfig agentConfig;
        
        float timer = 0f;
        float timeToWait;
        
        public IdleState(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
            agentConfig = AgentConfig.Instance;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            timer = 0f;
            timeToWait = Random.Range(agentConfig.waitingMinTime, agentConfig.waitingMaxTime);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            timer += Time.deltaTime;
            if (timer >= timeToWait)
                agentStateMachine.TransitionTo(AgentStateMachine.State.Walk);
        }
    }
}
