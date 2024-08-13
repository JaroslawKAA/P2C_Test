using System;
using System.Collections.Generic;
using Agents.Services;
using Core.GameEvents;
using Core.GameEvents.Events;
using Core.Patterns;
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

        LinkedPool<IPoolObject> agentPool;
        Dictionary<Guid, IPoolObject> agentsInstances = new();

        Transform cachedTransform;

        ILevelPointGenerator levelPointGenerator;


        // UNITY EVENTS
        void Awake()
        {
            cachedTransform = transform;
        
            agentPool = new LinkedPool<IPoolObject>(createFunc: OnPoolSpawn, actionOnGet: OnPoolGet, actionOnRelease: OnPoolRelease);

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
            IPoolObject agent = agentPool.Get();
            
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

        IPoolObject OnPoolSpawn()
        {
            IPoolObject agentInstance = Instantiate(agentPrefab);
            agentInstance.OnInstantiated();
            agentsInstances.Add(agentInstance.GUID, agentInstance);
            return agentInstance;
        }

        void OnPoolGet(IPoolObject agent)
        {
            agent.OnGetFromPool();
            agent.SetParent(cachedTransform);
        }

        void OnPoolRelease(IPoolObject agent)
        {
            agent.OnReleaseToPool();
            agent.SetParent(cachedTransform);
        }
    }
}