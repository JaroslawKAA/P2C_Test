using Sirenix.Utilities;
using UnityEngine;

namespace Agents
{
    [GlobalConfig("Assets/Resources/Configs")]
    [CreateAssetMenu(fileName = "AgentConfig", menuName = "Configs")]
    public class AgentConfig : GlobalConfig<AgentConfig>
    {
        public string reachedDestinationMessage = "Agent <GUID> arrived";
        
        public float walkingSpeed = 1f;
        public float minDistanceToWalk = 2f;
        public float maxDistanceToWalk = 10f;
    }
}
