using System;
using Agents.Services;
using Agents.States;
using Core.Patterns;
using UnityEngine;

namespace Agents
{
    public class Agent : MonoBehaviour, IPoolObject
    {
        // PRIVATE
        Transform cachedTransform;
        GameObject cachedGameObject;

        AgentStateMachine agentStateMachine;
    
        // PROPERTIES
        public Guid GUID { get; private set; }

        // UNITY EVENTS
        void Awake()
        {
            cachedTransform = transform;
            cachedGameObject = gameObject;
        }

        void OnEnable()
        {
            agentStateMachine?.TransitionTo(AgentStateMachine.State.Idle);
        }

        void Update()
        {
            agentStateMachine?.Update();
        }

        void OnDisable()
        {
            agentStateMachine?.TransitionTo(AgentStateMachine.State.None);
        }

        void OnDestroy()
        {
            cachedTransform = null;
            cachedGameObject = null;
        }

        // METHODS
        public void InitiateStateMachine(ILevelPointGenerator levelPointGenerator)
        {
            agentStateMachine = new AgentStateMachine(this, levelPointGenerator);
        }
        
        public void OnInstantiated()
        {
            GUID = Guid.NewGuid();
            cachedGameObject.SetActive(false);
        }

        public void OnGetFromPool()
        {
            cachedGameObject.SetActive(true);
        }

        public void OnReleaseToPool()
        {
            cachedGameObject.SetActive(false);
        }

        public void SetParent(Transform parent)
        {
            cachedTransform.SetParent(parent, worldPositionStays: true);
        }

        public void SetTransform(Vector3 position, Quaternion rotation)
        {
            cachedTransform.position = position;
            cachedTransform.rotation = rotation;
        }
    }
}
