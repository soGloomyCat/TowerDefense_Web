using UnityEngine;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel.Rooms
{
    public class AttackRoom : MonoBehaviour, IRoom
    {
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

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
