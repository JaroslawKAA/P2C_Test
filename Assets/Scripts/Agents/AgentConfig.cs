using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace Agents
{
    [GlobalConfig("Assets/Resources/Configs")]
    [CreateAssetMenu(fileName = "AgentConfig", menuName = "Configs")]
    public class AgentConfig : GlobalConfig<AgentConfig>
    {
        [Title("Walk State")]
        public string reachedDestinationMessage = "Agent GUID arrived";
        
        public float walkingSpeed = 1f;
        public float minDistanceToWalk = 2f;
        public float maxDistanceToWalk = 10f;
        
        [Title("Idle State")]
        public float waitingMinTime = .25f;
        public float waitingMaxTime = 1f;
    }
}
