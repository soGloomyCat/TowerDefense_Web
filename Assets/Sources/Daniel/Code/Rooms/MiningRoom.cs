using System;
using UnityEngine;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel.Rooms
{
    public class MiningRoom : MonoBehaviour, IRoom
    {
        [SerializeField] private Money _money = null;
        [SerializeField] private int _goldPerTick = 1;
        [SerializeField] private float _delay = 1;

        public event Action<IReadOnlyRoom> Upgraded = null;

        private Timer _timer = null;

        public int Level => 1;

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

        private void OnTimerTicked()
        {
            _money.Deposit(_goldPerTick);
        }
    }
}
