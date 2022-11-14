using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using TowerDefense.Daniel.Models;
using TowerDefense.Daniel.Interfaces;
using static UnityEditor.Recorder.OutputPath;

namespace TowerDefense.Daniel.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UpgradePopup : MonoBehaviour, IPointerClickHandler
    {
        private const float _AnimationSpeed = 8;

        public event Action Closed = null;
        public event Action UpgradeRequested = null;

        [SerializeField] private StatView _statViewPrefab = null;
        [SerializeField] private TMP_Text _title = null;
        [SerializeField] private TMP_Text _description = null;
        [SerializeField] private RectTransform _statsContainer = null;
        [SerializeField] private Button _upgradeButton = null;
        [SerializeField] private TMP_Text _priceText = null;

        private RectTransform _rectTransform = null;
        private IReadOnlyRoom _currentRoom = null;
        private bool _isEnabled = false;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            _rectTransform.localScale = Vector3.zero;
        }

        public void Initialize(IReadOnlyRoom room)
        {
            _currentRoom = room;

            UpdateVisual();
        }

        private void OnEnable()
        {
            _upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);
        }

        private void OnDisable()
        {
            _upgradeButton.onClick.RemoveListener(OnUpgradeButtonClicked);
        }

        public void Show()
        {
            _isEnabled = true;

            _rectTransform.DOScale(Vector3.one, _AnimationSpeed).SetSpeedBased();
        }

        public void Toggle()
        {
            if (_isEnabled)
            {
                Hide();

                return;
            }

            Show();
        }

        public void Hide()
        {
            _isEnabled = false;

            _rectTransform.DOScale(Vector3.zero, _AnimationSpeed).SetSpeedBased();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Closed?.Invoke();
        }

        private void UpdateVisual()
        {
            _title.text =$"{_currentRoom.Information.Title} (Уровень {_currentRoom.Level + 1})";
            _description.text = _currentRoom.Information.Description;

            foreach (RectTransform statView in _statsContainer)
            {
                Destroy(statView.gameObject);
            }

            foreach (var stat in _currentRoom.Information.Stats)
            {
                var statView = Instantiate(_statViewPrefab, _statsContainer);

                statView.Initialize(_currentRoom.Level, stat);
            }

            _upgradeButton.gameObject.SetActive(_currentRoom.Level < _currentRoom.Information.UpgradePrices.Count);

            _priceText.text = _upgradeButton.gameObject.activeSelf ? $"{_currentRoom.Information.UpgradePrices[_currentRoom.Level]}$" : "";
        }

        private void OnUpgradeButtonClicked()
        {
            UpgradeRequested?.Invoke();

            UpdateVisual();
        }
    }
}