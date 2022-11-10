using UnityEngine;
using TowerDefense.Daniel.Interfaces;
using System;

namespace TowerDefense.Daniel.Rooms
{
    public class AttackRoom : MonoBehaviour, IRoom
    {
        public int Level => throw new NotImplementedException();

        public event Action<IReadOnlyRoom> Upgraded;

        public void FocusIn()
        {
            throw new System.NotImplementedException();
        }

        public void FocusOut()
        {
            throw new System.NotImplementedException();
        }

        public void Accept(IUnit unit)
        {
            throw new System.NotImplementedException();
        }

        public IRoom Instantiate(Vector3 position, Quaternion rotation, Transform parent)
        {
            return Instantiate(this, position, rotation, parent);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
