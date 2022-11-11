using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel.Rooms
{
    public class BankRoom : Room
    {
        [SerializeField] private Money _money = null;
        [SerializeField] private List<int> _capacities = new List<int>();

        public int Capacity => _capacities[Level];
        protected override int MaxLevel => _capacities.Count - 1;

        private void OnEnable()
        {
            _money.ValueChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            _money.ValueChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int oldValue, int newValue)
        {
            if (newValue > Capacity)
            {
                StartCoroutine(WithdrawLater(newValue - Capacity));
            }
        }

        private IEnumerator WithdrawLater(int amount)
        {
            yield return null;
            yield return null;
            yield return null;

            _money.TryWithdraw(amount);
        }
    }
}
