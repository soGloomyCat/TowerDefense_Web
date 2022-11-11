using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel.Rooms
{
    public class BankRoom : MonoBehaviour, IRoom
    {
        [SerializeField] private Money _money = null;
        [SerializeField] private List<int> _capacities = new List<int>();

        public event Action<IReadOnlyRoom> Upgraded = null;

        public int Level { get; private set; } = 0;
        public int Capacity => _capacities[Level];

        private void OnEnable()
        {
            _money.ValueChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            _money.ValueChanged -= OnMoneyChanged;
        }

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

        public void Upgrade()
        {
            Level = Mathf.Clamp(Level + 1, 0, _capacities.Count - 1);
        }

        public IRoom Instantiate(Vector3 position, Quaternion rotation, Transform parent)
        {
            return Instantiate(this, position, rotation, parent);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void OnMoneyChanged(int oldValue, int newValue)
        {
            if (newValue > Capacity)
            {
                StartCoroutine(WithdrawOnNextFrame(newValue - Capacity));
            }
        }

        private IEnumerator WithdrawOnNextFrame(int amount)
        {
            yield return null;

            _money.TryWithdraw(amount);
        }
    }
}
