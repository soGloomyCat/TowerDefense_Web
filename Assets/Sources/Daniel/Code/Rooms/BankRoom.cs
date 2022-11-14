using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel.Interfaces;
using System.Linq;

namespace TowerDefense.Daniel.Rooms
{
    public class BankRoom : Room
    {
        [SerializeField] private string _moneyCapacityName = "Макс. Казна";

        public int Capacity => Information.Stats.First(x => x.Name == _moneyCapacityName).Values[Level];
        protected override int MaxLevel => Information.Stats.First(x => x.Name == _moneyCapacityName).Values.Count - 1;
    }
}
