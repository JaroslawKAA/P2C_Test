using System;
using Agents.Services;
using UnityEngine;

namespace Agents.States
{
    public class AgentStateMachine : StateMachineBase
    {
        public enum State
        {
            None,
            Idle,
            Walk,
        }
        
        public Transform CachedTransform { get; private set; }
        public Agent Owner { get; private set; }
        
        IdleState IdleState { get; }
        WalkState WalkState { get; }

        public AgentStateMachine(MonoBehaviour context, ILevelPointGenerator levelPointGenerator)
        {
            IdleState = new IdleState(this, context);
            WalkState = new WalkState(this, context, levelPointGenerator);

            CachedTransform = context.transform;
            Owner = context as Agent;
        }

        public void TransitionTo(State state)
        {
            switch (state)
            {
                case State.None:
                    TransitionTo(null);
                    break;
                case State.Idle:
                    TransitionTo(IdleState);
                    break;
                case State.Walk:
                    TransitionTo(WalkState);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}
