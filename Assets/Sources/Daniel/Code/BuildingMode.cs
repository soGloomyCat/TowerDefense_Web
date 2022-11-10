using System;
using System.Collections.Generic;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.UI;
using UnityEngine;
//using static UnityEditor.Progress;

namespace TowerDefense.Daniel
{
    [RequireComponent(typeof(Panel))]
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
        }

        private void OnDisable()
        {
            _market.ItemSelected -= OnMarketItemSelected;
            _castle.ClickedOnEmptyHolder -= OnEmptyHolderClicked;
        }

        public void Activate()
        {
            _linkedPanel.Show();
            //_inputPanel.gameObject.SetActive(false);
            _castle.ShowEmptyHolders();
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

            _currentItem.Information.Buy();
            _currentItem.UpdateVisual();

            holder.BuildRoom(_currentItem.Information.Prefab.Value);

            _currentItem = null;

            Deactivate();
        }
    }
}
