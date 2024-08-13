using UnityEngine;
using UnityEngine.AI;

namespace Agents.Services
{
    public class NavMeshLevelPointGenerator : ILevelPointGenerator
    {
        public Vector3 GetRandomPoint(Vector3 startPoint, float distanceFromStartPoint)
        {
            Vector3 randomDirection = Random.insideUnitSphere * distanceFromStartPoint;
            randomDirection += startPoint;

            Vector3 finalPosition = startPoint;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, distanceFromStartPoint, 1)) 
                finalPosition = hit.position;

            return finalPosition;
        }
    }
}