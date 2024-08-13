using System.Text.RegularExpressions;
using Agents.Services;
using Core.Extensions;
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
            Vector3 destinationPoint = GetDestinationPoint();

            float distanceToWalk = Vector3.Distance(agentStateMachine.CachedTransform.position, destinationPoint);
            LookToDestination(destinationPoint);
            MoveTo(distanceToWalk, destinationPoint);
        }

        void MoveTo(float distanceToWalk, Vector3 destinationPoint)
        {
            float moveDuration = distanceToWalk / agentConfig.walkingSpeed;
            movingTweener = agentStateMachine.CachedTransform.DOMove(destinationPoint, moveDuration);
            movingTweener.SetEase(Ease.Linear);
            movingTweener.onComplete += OnReachedDestination;
        }

        Vector3 GetDestinationPoint()
        {
            float distanceToWalk = Random.Range(agentConfig.minDistanceToWalk, agentConfig.maxDistanceToWalk);
            return levelPointGenerator.GetRandomPoint(startPoint: agentStateMachine.CachedTransform.position,
                distanceFromStartPoint: distanceToWalk);
        }

        void LookToDestination(Vector3 destinationPoint)
        {
            Vector3 directionToDestinationPoint = destinationPoint.WithY(0) - agentStateMachine.CachedTransform.position.WithY(0);
            Quaternion lookAtDestinationPointRotation = Quaternion.LookRotation(directionToDestinationPoint, Vector3.up);
            agentStateMachine.Owner.SetTransform(agentStateMachine.CachedTransform.position, lookAtDestinationPointRotation);
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
