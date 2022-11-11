using UnityEngine;
using TowerDefense.Daniel.Interfaces;
using System;
using TowerDefense.Daniel.Models;

namespace TowerDefense.Daniel
{
    public abstract class Room : MonoBehaviour, IReadOnlyRoom
    {
        [SerializeField] private RoomMarketInformation _information = null;

        public event Action<IReadOnlyRoom> Upgraded = null;

        public int Level { get; private set; } = 0;
        public RoomMarketInformation Information => _information;

        public void FocusIn()
        {

        }

        public void FocusOut()
        {

        }

        public void Accept(IUnit unit)
        {

        }

        public void Upgrade()
        {
            Level = Mathf.Clamp(Level + 1, 0, MaxLevel);
        }

        protected virtual int MaxLevel { get; } = 0;
    }
}
