using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel
{
    public class Castle : MonoBehaviour
    {
        private const float _UpgradePanelAnimationSpeed = 8;

        public event Action<RoomHolder> ClickedOnEmptyHolder = null;
        public event Action<IReadOnlyRoom> RoomAdded = null;
        public event Action<IReadOnlyRoom> RoomUpgraded = null;

        [SerializeField, HideInInspector] private List<RoomHolder> _roomHolders = new List<RoomHolder>();
        [SerializeField] private RectTransform _upgradePanel = null;
        [SerializeField] private Button _upgradeButton = null;
        [SerializeField] private Money _money = null;

        private RoomHolder _selectedHolder = null;
        private bool _isUpgradePanelEnabled = false;

        private void Awake()
        {
            _upgradePanel.localScale = Vector3.zero;
        }

        private void Start()
        {
            HideAllHolders();
        }

        private void OnEnable()
        {
            foreach (var roomHolder in _roomHolders)
            {
                roomHolder.Clicked += Select;
                roomHolder.RoomAdded += OnRoomAdded;
                roomHolder.RoomUpgraded += OnRoomUpgraded;
            }

            _upgradeButton.onClick.AddListener(UpgradeSelectedHolder);
        }

        private void OnDisable()
        {
            foreach (var roomHolder in _roomHolders)
            {
                roomHolder.Clicked -= Select;
                roomHolder.RoomAdded -= OnRoomAdded;
                roomHolder.RoomUpgraded -= OnRoomUpgraded;
            }

            _upgradeButton.onClick.RemoveListener(UpgradeSelectedHolder);
        }

        private void OnValidate()
        {
            _roomHolders = GetComponentsInChildren<RoomHolder>().ToList();
        }

        public void CancelSelection()
        {
            _selectedHolder = null;

            _isUpgradePanelEnabled = false;

            _upgradePanel.DOScale(Vector3.zero, _UpgradePanelAnimationSpeed).SetSpeedBased();
        }

        public void Select(RoomHolder holder)
        {
            if (holder.IsEmpty)
            {
                ClickedOnEmptyHolder?.Invoke(holder);

                return;
            }

            if (_selectedHolder == holder)
            {
                _isUpgradePanelEnabled = !_isUpgradePanelEnabled;

                _upgradePanel.DOScale(_isUpgradePanelEnabled ? Vector3.one : Vector3.zero, _UpgradePanelAnimationSpeed).SetSpeedBased();
            }
            else
            {
                _isUpgradePanelEnabled = true;

                _upgradePanel.DOScale(Vector3.one, _UpgradePanelAnimationSpeed).SetSpeedBased();
            }

            _selectedHolder = holder;

            _upgradeButton.gameObject.SetActive(_selectedHolder.Room.Level < _selectedHolder.Room.Information.UpgradePrices.Count);
        }

        public IEnumerable<T> GetRoomsOfType<T>() where T : IReadOnlyRoom
        {
            return _roomHolders.Select(x => x.Room).Where(x => x != null).Where(x => x is T).Cast<T>();
        }

        public void ShowEmptyHolders()
        {
            foreach (var roomHolder in _roomHolders)
            {
                if (!roomHolder.IsEmpty)
                {
                    continue;
                }

                roomHolder.ShowBuildOverlay();
            }
        }

        public void HideAllHolders()
        {
            foreach (var roomHolder in _roomHolders)
            {
                roomHolder.HideBuildOverlay();
            }
        }

        private void UpgradeSelectedHolder()
        {
            if (_selectedHolder == null)
            {
                return;
            }

            var price = _selectedHolder.Room.Information.UpgradePrices[_selectedHolder.Room.Level];

            if (price <= _money.Value)
            {
                _money.TryWithdraw(price);

                _selectedHolder.UpgradeRoom();
            }
        }

        private void OnRoomAdded(IReadOnlyRoom room)
        {
            RoomAdded?.Invoke(room);
        }

        private void OnRoomUpgraded(IReadOnlyRoom room)
        {
            RoomUpgraded?.Invoke(room);
        }
    }
}
