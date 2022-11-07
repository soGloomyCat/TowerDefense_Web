using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using TowerDefense.Daniel.Models;

namespace TowerDefense.Daniel.UI
{
    public class MarketItem : MonoBehaviour, IPointerClickHandler
    {
        public event Action<MarketItem> Clicked = null;

        [SerializeField] private TMP_Text _title = null;
        [SerializeField] private Image _preview = null;
        [SerializeField] private TMP_Text _description = null;
        [SerializeField] private TMP_Text _price = null;

        public RoomMarketInformation Information { get; private set; }

        public void Initialize(RoomMarketInformation information)
        {
            Information = information;

            UpdateVisual();
        }

        private void UpdateVisual()
        {
            _title.text = Information.Title;
            _preview.sprite = Information.Preview;
            _description.text = Information.Description;
            _price.text = $"{Information.Price}$";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }
    }
}
