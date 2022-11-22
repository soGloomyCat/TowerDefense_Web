using System;
using System.Collections.Generic;
using UnityEngine;
using Lean.Localization;
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
        private int _maxItemsCount = 0;

        private void Awake()
        {
            foreach (var room in _rooms)
            {
                var item = Instantiate(_itemPrefab, _itemsContainer);
                item.Initialize(room);

                _items.Add(item);
            }
            _maxItemsCount = _items.Count;
        }

        private void Start()
        {
            foreach (var item in _items)
            {
                item.UpdateVisual();
            }
        }

        private void OnEnable()
        {
            foreach (var item in _items)
            {
                item.Clicked += OnMarketItemClicked;
            }
            LeanLocalization.OnLocalizationChanged += UpdateVisual;
        }

        private void OnDisable()
        {
            foreach (var item in _items)
            {
                item.Clicked -= OnMarketItemClicked;
            }
            LeanLocalization.OnLocalizationChanged -= UpdateVisual;
        }

        public MarketItem GetItem(string id)
        {
            return _items.Find(x => x.Information != null && x.Information.InternalID == id);
        }

        public void UpdateVisual(int maxItemsCount)
        {
            _maxItemsCount = maxItemsCount;
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

        private void UpdateVisual()
        {
            UpdateVisual(_maxItemsCount);
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
