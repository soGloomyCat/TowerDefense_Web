using System;
using System.Collections.Generic;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.Rooms;
using TowerDefense.Daniel.UI;
using UnityEngine;

namespace TowerDefense.Daniel
{
    [RequireComponent(typeof(Panel))]
    [DefaultExecutionOrder(1)]
    public class BuildingMode : MonoBehaviour
    {
        [SerializeField] private Panel _mainPanel = null;
        //[SerializeField] private InputPanel _inputPanel = null;
        [SerializeField] private Castle _castle = null;
        [SerializeField] private Market _market = null;

        private Panel _linkedPanel = null;
        private MarketItem _currentItem = null;

        private void Awake()
        {
            _linkedPanel = GetComponent<Panel>();
        }

        private void Start()
        {
            _market.UpdateVisual(_castle.MaxRoomsCount);
        }

        private void Update()
        {
            if (!_mainPanel.IsActive && Input.GetKeyDown(KeyCode.Escape))
            {
                Deactivate();
            }
        }

        private void OnEnable()
        {
            _market.ItemSelected += OnMarketItemSelected;
            _castle.ClickedOnEmptyHolder += OnEmptyHolderClicked;
            _castle.RoomUpgraded += OnRoomUpgraded;
        }

        private void OnDisable()
        {
            _market.ItemSelected -= OnMarketItemSelected;
            _castle.ClickedOnEmptyHolder -= OnEmptyHolderClicked;
            _castle.RoomUpgraded -= OnRoomUpgraded;
        }

        public void Activate()
        {
            _linkedPanel.Show();
            //_inputPanel.gameObject.SetActive(false);
            _castle.ShowEmptyHolders(_currentItem.Information.Prefab);
        }

        public void Deactivate()
        {
            _mainPanel.Show();
            //_inputPanel.gameObject.SetActive(true);
            _castle.HideAllHolders();
        }

        private void OnMarketItemSelected(MarketItem item)
        {
            _currentItem = item;

            Activate();
        }

        private void OnEmptyHolderClicked(RoomHolder holder)
        {
            if (_currentItem == null)
            {
                return;
            }

            if (_currentItem.Information.CurrentPrice > 0 && !_market.TryWithdraw(_currentItem.Information.CurrentPrice))
            {
                Deactivate();

                return;
            }

            if (holder.TryBuildRoom(_currentItem.Information.Prefab))
            {
                _currentItem.Information.Buy();
                _currentItem.UpdateVisual();

                _currentItem = null;

                Deactivate();
            }
        }

        private void OnRoomUpgraded(IReadOnlyRoom room)
        {
            if (room is StrategyRoom)
            {
                _market.UpdateVisual(_castle.MaxRoomsCount);
            }
        }
    }
}
