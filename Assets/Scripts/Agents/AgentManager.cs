using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Pool;

namespace Agents
{
    public class AgentManager : MonoBehaviour
    {
        // SERIALIZED
        [Title("Depend")]
        [SerializeField] [Required] Agent agentPrefab;

        // PRIVATE
        EventListener SpawnAgentEventListener;
        EventListener ReleaseAgentEventListener;

        LinkedPool<Agent> agentPool;
        Dictionary<Guid, Agent> agentsInstances = new();

        Transform cachedTransform;


        // UNITY EVENTS
        void Awake()
        {
            cachedTransform = transform;
        
            agentPool = new LinkedPool<Agent>(createFunc: Pool_Spawn, actionOnGet: Pool_OnGet, actionOnRelease: Pool_OnRelease);

            SpawnAgentEventListener = new EventListener(Spawn);
            ReleaseAgentEventListener = new EventListener(OnAgentReleased);

            EventManager.Instance.RegisterListener<SpawnAgentEvent>(SpawnAgentEventListener);
            EventManager.Instance.RegisterListener<ReleaseAgentEvent>(ReleaseAgentEventListener);
        }

        void OnDestroy()
        {
            agentPool.Clear();
            agentPool = null;
        
            EventManager.Instance.UnregisterListener<SpawnAgentEvent>(SpawnAgentEventListener);
            EventManager.Instance.UnregisterListener<ReleaseAgentEvent>(ReleaseAgentEventListener);

            SpawnAgentEventListener = null;
            ReleaseAgentEventListener = null;

            cachedTransform = null;
        }


        // METHODS
        void Spawn(EventBase eventBase)
        {
            Agent agent = agentPool.Get();
            EventManager.Instance.TriggerEvent(new SpawnedAgentEvent(agent.GUID));
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