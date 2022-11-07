using UnityEngine;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel.Rooms
{
    public class ForgeRoom : MonoBehaviour, IRoom
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
