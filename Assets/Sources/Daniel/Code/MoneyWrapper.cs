using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace TowerDefense.Daniel
{
    public class MoneyWrapper : MonoBehaviour
    {
        public event Action<int, int> ValueChanged = null;

        [SerializeField] private Money _money = null;
        [SerializeField] private bool _isAllFree = false;

        public int Value => _money.Value;

        private void OnEnable()
        {
            _money.ValueChanged += OnValueChanged;
        }

        private void OnDisable()
        {
            _money.ValueChanged -= OnValueChanged;
        }

        public void Deposit(int amount)
        {
            _money.Deposit(amount);
        }

        public bool CanWithdraw(int amount)
        {
            return _isAllFree || _money.Value >= amount;
        }

        public bool TryWithdraw(int amount)
        {
            return _isAllFree || _money.TryWithdraw(amount);
        }

        private void OnValueChanged(int oldValue, int newValue)
        {
            ValueChanged?.Invoke(oldValue, newValue);
        }
    }
}