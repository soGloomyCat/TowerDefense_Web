using System;
using UnityEngine;
using TowerDefense.Daniel.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TowerDefense.Daniel.Rooms
{
    public class StrategyRoom : Room
    {
        [SerializeField] private string _moneyCapacityName = "Казна";

        private int _maxLevel = 0;

        public int MoneyCapacity => Information.Stats.First(x => x.Name == _moneyCapacityName).Values[Level];
        protected override int MaxLevel => _maxLevel;

        public void Initialize(int maxLevel)
        {
            _maxLevel = maxLevel;
        }
    }
}
