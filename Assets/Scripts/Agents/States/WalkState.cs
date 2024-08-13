using System;
using System.Text.RegularExpressions;
using Agents.Services;
using Core.GameEvents;
using Core.GameEvents.Events;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Agents.States
{
    public class WalkState : AgentStateBase
    {
        readonly AgentConfig agentConfig;
        readonly ILevelPointGenerator levelPointGenerator;

        Vector3 destinationPoint;
        Tweener movingTweener;
        
        string messageGuidPattern = "GUID";
        
        public WalkState(StateMachineBase stateMachineBase, MonoBehaviour context, ILevelPointGenerator levelPointGenerator)
            : base(stateMachineBase, context)
        {
            this.levelPointGenerator = levelPointGenerator;
            agentConfig = AgentConfig.Instance;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            float distanceToWalk = Random.Range(agentConfig.minDistanceToWalk, agentConfig.maxDistanceToWalk);
            destinationPoint = levelPointGenerator.GetRandomPoint(startPoint: agentStateMachine.CachedTransform.position,
                distanceFromStartPoint: distanceToWalk);

            float scaledSpeed = distanceToWalk / agentConfig.walkingSpeed;
            movingTweener = agentStateMachine.CachedTransform.DOMove(destinationPoint, scaledSpeed);
            movingTweener.onComplete += OnReachedDestination;
        }

        void OnReachedDestination()
        {
            TriggerReachedDestinationEvent();
            ToIdle();
        }

        void TriggerReachedDestinationEvent()
        {
            string agentMessage = agentConfig.reachedDestinationMessage;
            string stringGuid = agentStateMachine.Owner.GUID.ToString();
            agentMessage = Regex.Replace(agentMessage, messageGuidPattern, stringGuid);

            EventManager.TriggerEvent(new AgentMessageEvent(agentMessage));
        }

        void ToIdle()
        {
            agentStateMachine.TransitionTo(AgentStateMachine.State.Idle);
        }

        public override void OnExit()
        {
            base.OnExit();
            if (movingTweener != null)
            {
                movingTweener.Kill();
                movingTweener = null;
            }
        }
    }
}
