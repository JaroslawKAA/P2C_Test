using UnityEngine;

namespace Agents.States
{
    public class IdleState : AgentStateBase
    {
        float waitingMinTime = .5f;
        float waitingMaxTime = 2f;

        float timer = 0f;
        float timeToWait;
        
        public IdleState(StateMachineBase stateMachineBase, MonoBehaviour context) : base(stateMachineBase, context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();
            timer = 0f;
            timeToWait = Random.Range(waitingMinTime, waitingMaxTime);
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
