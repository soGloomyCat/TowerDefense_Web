using System;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel.Models;

namespace TowerDefense.Daniel.UI
{
    public class Market : MonoBehaviour
    {
        public event Action<MarketItem> ItemSelected = null;

        [SerializeField] private List<RoomMarketInformation> _rooms = new List<RoomMarketInformation>();
        [SerializeField] private MarketItem _itemPrefab = null;
        [SerializeField] private RectTransform _itemsContainer = null;

        private List<MarketItem> _items = new List<MarketItem>();

        private void Awake()
        {
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

        private void OnMarketItemClicked(MarketItem item)
        {
            ItemSelected?.Invoke(item);
        }
    }
}
