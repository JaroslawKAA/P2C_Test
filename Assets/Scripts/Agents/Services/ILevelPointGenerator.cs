using UnityEngine;

namespace Agents.Services
{
    public interface ILevelPointGenerator
    {
        Vector3 GetRandomPoint(Vector3 startPoint, float distanceFromStartPoint);
    }
}
