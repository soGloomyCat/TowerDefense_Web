using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Daniel.Rooms
{
    public class MiningRoom : Room
    {
        [SerializeField] private Money _money = null;
        [SerializeField] private List<int> _goldPerTick = new List<int>();
        [SerializeField] private float _delay = 1;

        private Timer _timer = null;

        protected override int MaxLevel => _goldPerTick.Count - 1;

        private void Awake()
        {
            _timer = new Timer(_delay);
        }

        private void OnEnable()
        {
            _timer.Ticked += OnTimerTicked;
        }

        private void OnDisable()
        { 
            _timer.Ticked -= OnTimerTicked;
        }

        private void Update()
        {
            _timer.Update(Time.deltaTime);
        }

        private void OnTimerTicked()
        {
            _money.Deposit(_goldPerTick[Level]);
        }
    }
}
