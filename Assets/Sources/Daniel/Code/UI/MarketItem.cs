using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Lean.Localization;
using TowerDefense.Daniel.Models;

namespace TowerDefense.Daniel.UI
{
    public class MarketItem : MonoBehaviour, IPointerClickHandler
    {
        public event Action<MarketItem> Clicked = null;

        [SerializeField] private Vector2Int _previewSize = new Vector2Int(240, 160);
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

        public void UpdateVisual()
        {
            if (Information == null || Information.CurrentPrice < 0)
            {
                gameObject.SetActive(false);

                return;
            }

            _title.text = LeanLocalization.GetTranslationText(Information.Title);
            _preview.sprite = Information.Prefab.GetPreview(_previewSize); //Information.Preview;
            _description.text = LeanLocalization.GetTranslationText($"{Information.Title}_Description");//Information.Description;

            _price.text = Information.CurrentPrice < 0 ? "Недоступно" :
                          $"{Information.CurrentPrice}$";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (Information.CurrentPrice < 0)
            {
                return;
            }

            Clicked?.Invoke(this);
        }
    }
}
