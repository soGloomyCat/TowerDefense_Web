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

        private int _maxValue = 500;

        public int Value => _money.Value;

        private void OnEnable()
        {
            _money.ValueChanged += OnValueChanged;

            OnValueChanged(0, _money.Value);
        }

        private void OnDisable()
        {
            _money.ValueChanged -= OnValueChanged;
        }

        public void SetMaxValue(int maxValue)
        {
            _maxValue = maxValue;
        }

        public void Deposit(int amount)
        {
            if (_money.Value + amount > _maxValue)
            {
                amount = _maxValue - amount;
            }

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

        private IEnumerator WithdrawLater(int amount)
        {
            yield return null;
            yield return null;
            yield return null;

            _money.TryWithdraw(amount);
        }

        private void OnValueChanged(int oldValue, int newValue)
        {
            if (newValue > _maxValue)
            {
                StartCoroutine(WithdrawLater(newValue - _maxValue));

                return;
            }

            ValueChanged?.Invoke(oldValue, newValue);
        }
    }
}