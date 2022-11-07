using UnityEngine;

namespace TowerDefense.Daniel.Interfaces
{
    public interface IRoom
    {
        void FocusIn();
        void FocusOut();
        void Accept(IUnit unit);

        IRoom Instantiate(Vector3 position, Quaternion rotation, Transform parent);
        void Destroy();
    }
}