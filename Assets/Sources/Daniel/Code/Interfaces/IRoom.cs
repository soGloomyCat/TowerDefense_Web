using UnityEngine;

namespace TowerDefense.Daniel.Interfaces
{
    public interface IRoom : IReadOnlyRoom
    {
        IRoom Instantiate(Vector3 position, Quaternion rotation, Transform parent);
        void Destroy();
    }
}