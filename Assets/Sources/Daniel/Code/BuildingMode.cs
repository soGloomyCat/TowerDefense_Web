using System.Collections;
using System.Collections.Generic;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.UI;
using UnityEngine;

namespace TowerDefense.Daniel
{
    [RequireComponent(typeof(Panel))]
    public class BuildingMode : MonoBehaviour
    {
        [SerializeField] private Panel _mainPanel = null;
        [SerializeField] private Castle _castle = null;
        [SerializeField] private Market _market = null;

        private Panel _linkedPanel = null;
        private MarketItem _currentItem = null;

        private void Awake()
        {
            _linkedPanel = GetComponent<Panel>();
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
            _castle.ShowEmptyHolders();
        }

        public void Deactivate()
        {
            _mainPanel.Show();
            _castle.HideAllHolders();
        }

        private void OnMarketItemSelected(MarketItem item)
        {
            _currentItem = item;

            Activate();
        }

        private void OnEmptyHolderClicked(RoomHolder holder)
        {
            holder.BuildRoom(_currentItem.Information.Prefab.Value);

            _currentItem = null;

            Deactivate();
        }
    }
}
