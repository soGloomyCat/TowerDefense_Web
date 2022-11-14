using System;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel.Models
{
    [CreateAssetMenu(fileName = "New RoomMarketInformation", menuName = "Tower Defense/Daniel/Room Market Information", order = 50)]
    public class RoomMarketInformation : ScriptableObjectWithID
    {
        [Serializable]
        public class Stat
        {
            [field: SerializeField] public string Name { get; private set; } = "<Untitled>";
            [field: SerializeField] public List<int> Values { get; private set; } = new List<int>();
        }

        [field: SerializeField] public Room Prefab { get; private set; } = null;
        [field: SerializeField] public string Title { get; private set; } = "";
        [field: SerializeField] public Sprite Preview { get; private set; } = null;
        [field: SerializeField] public string Description { get; private set; } = "";
        [field: SerializeField] public List<int> BuyingPrices { get; private set; } = new List<int>();
        [field: SerializeField] public List<int> UpgradePrices { get; private set; } = new List<int>();
        [field: SerializeField] public List<Stat> Stats { get; private set; } = new List<Stat>();

        private int _currentAmount = 0;

        public int CurrentPrice => _currentAmount < BuyingPrices.Count ? BuyingPrices[_currentAmount] : -1;

        public void Buy()
        {
            _currentAmount++;
        }

        protected override void AfterEnabled()
        {
            _currentAmount = 0;
        }
    }
}
