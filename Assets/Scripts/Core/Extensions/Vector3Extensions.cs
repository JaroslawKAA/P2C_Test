using UnityEngine;

namespace Core.Extensions
{
    public static class Vector3Extensions
    {
        public static Vector3 WithY(this Vector3 vector3, float y)
        {
            return new Vector3(vector3.x, y, vector3.z);
        }
    }
}
