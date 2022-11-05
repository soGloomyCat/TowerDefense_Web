using UnityEngine;

namespace TowerDefense.Daniel.Extensions
{
    public static class ComponentExtensions
    {
        public static bool TryGetComponentInParent<T>(this Component target, out T result)
        {
            result = target.GetComponentInParent<T>();

            return result != null;
        }
    }
}