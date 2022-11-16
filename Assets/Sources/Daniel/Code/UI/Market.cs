using System;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel.Models;

namespace TowerDefense.Daniel.UI
{
    [RequireComponent(typeof(Panel))]
    public class Market : MonoBehaviour
    {
        public event Action<MarketItem> ItemSelected = null;

        [SerializeField] private List<RoomMarketInformation> _rooms = new List<RoomMarketInformation>();
        [SerializeField] private Money _money = null;
        [SerializeField] private MarketItem _itemPrefab = null;
        [SerializeField] private RectTransform _itemsContainer = null;

        private Panel _panel = null;
        private List<MarketItem> _items = new List<MarketItem>();

        private void Awake()
        {
            _panel = GetComponent<Panel>();

            foreach (var room in _rooms)
            {
                var item = Instantiate(_itemPrefab, _itemsContainer);
                item.Initialize(room);

                _items.Add(item);
            }
        }

        private void OnEnable()
        {
            foreach (var item in _items)
            {
                item.Clicked += OnMarketItemClicked;
            }
        }

        private void OnDisable()
        {
            foreach (var item in _items)
            {
                item.Clicked -= OnMarketItemClicked;
            }
        }

        public bool TryWithdraw(int amount)
        {
            return _money.TryWithdraw(amount);
        }

        private void OnMarketItemClicked(MarketItem item)
        {
            if (item.Information.CurrentPrice > 0 && _money.Value < item.Information.CurrentPrice)
            {
                return;
            }

            ItemSelected?.Invoke(item);
        }
    }
}
