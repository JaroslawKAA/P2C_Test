using System;
using System.Collections.Generic;
using Agents.Services;
using Core.GameEvents;
using Core.GameEvents.Events;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace Agents
{
    public class AgentManager : MonoBehaviour
    {
        // SERIALIZED
        [Title("Config")]
        [SerializeField] float agentPositionDistance = 10f;
        
        [Title("Depend")]
        [SerializeField] [Required] Agent agentPrefab;

        // PRIVATE
        EventListener SpawnAgentEventListener;
        EventListener ReleaseAgentEventListener;

        LinkedPool<Agent> agentPool;
        Dictionary<Guid, Agent> agentsInstances = new();

        Transform cachedTransform;

        ILevelPointGenerator levelPointGenerator;


        // UNITY EVENTS
        void Awake()
        {
            cachedTransform = transform;
        
            agentPool = new LinkedPool<Agent>(createFunc: Pool_Spawn, actionOnGet: Pool_OnGet, actionOnRelease: Pool_OnRelease);

            levelPointGenerator = new NavMeshLevelPointGenerator();

            SpawnAgentEventListener = new EventListener(Spawn);
            ReleaseAgentEventListener = new EventListener(OnAgentReleased);

            EventManager.RegisterListener<SpawnAgentEvent>(SpawnAgentEventListener);
            EventManager.RegisterListener<ReleaseAgentEvent>(ReleaseAgentEventListener);
        }

        void OnDestroy()
        {
            agentPool.Clear();
            agentPool = null;
        
            EventManager.UnregisterListener<SpawnAgentEvent>(SpawnAgentEventListener);
            EventManager.UnregisterListener<ReleaseAgentEvent>(ReleaseAgentEventListener);

            SpawnAgentEventListener = null;
            ReleaseAgentEventListener = null;

            cachedTransform = null;
        }


        // METHODS
        void Spawn(EventBase eventBase)
        {
            Agent agent = agentPool.Get();
            
            Vector3 randomPosition = levelPointGenerator.GetRandomPoint(cachedTransform.position, agentPositionDistance);
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(1f, 360f), 0f);
            agent.SetTransform(randomPosition, randomRotation);
            
            EventManager.TriggerEvent(new SpawnedAgentEvent(agent.GUID));
        }

        void OnAgentReleased(EventBase eventBase)
        {
            ReleaseAgentEvent releaseAgentEvent = eventBase as ReleaseAgentEvent;
            agentPool.Release(agentsInstances[releaseAgentEvent.ReleasedAgent]);
        }

        Agent Pool_Spawn()
        {
            Agent agentInstance = Instantiate(agentPrefab);
            agentInstance.OnInstantiated();
            agentsInstances.Add(agentInstance.GUID, agentInstance);
            return agentInstance;
        }

        void Pool_OnGet(Agent agent)
        {
            agent.OnGet();
            agent.SetParent(cachedTransform);
        }

        void Pool_OnRelease(Agent agent)
        {
            agent.OnRelease();
            agent.SetParent(cachedTransform);
        }
    }
}