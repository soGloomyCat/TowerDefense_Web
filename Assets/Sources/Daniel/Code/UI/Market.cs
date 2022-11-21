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
        [SerializeField] private MoneyWrapper _money = null;
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

        public MarketItem GetItem(string id)
        {
            return _items.Find(x => x.Information != null && x.Information.InternalID == id);
        }

        public void UpdateVisual(int maxItemsCount)
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var item = _items[i];

                item.gameObject.SetActive(i < maxItemsCount);
                item.UpdateVisual();
            }
        }

        public bool TryWithdraw(int amount)
        {
            return _money.TryWithdraw(amount);
        }

        private void OnMarketItemClicked(MarketItem item)
        {
            if (item.Information.CurrentPrice > 0 && !_money.CanWithdraw(item.Information.CurrentPrice))
            {
                return;
            }

            ItemSelected?.Invoke(item);
        }
    }
}
