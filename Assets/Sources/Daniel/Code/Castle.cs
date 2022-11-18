using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.Rooms;
using TowerDefense.Daniel.UI;
using System.Collections;

namespace TowerDefense.Daniel
{
    public class Castle : MonoBehaviour
    {
        [Serializable]
        private class CastleUpgrade
        {
            [field: SerializeField] public Mesh Mesh { get; private set; } = null;
            [field: SerializeField] public List<RoomHolder> Holders { get; private set; } = new List<RoomHolder>();
        }

        public event Action<RoomHolder> ClickedOnEmptyHolder = null;
        public event Action<IReadOnlyRoom> RoomAdded = null;
        public event Action<IReadOnlyRoom> RoomUpgraded = null;

        [SerializeField, HideInInspector] private List<RoomHolder> _roomHolders = new List<RoomHolder>();
        [SerializeField] private List<CastleUpgrade> _upgrades = new List<CastleUpgrade>();
        [SerializeField] private MeshFilter _meshFilter = null;
        [SerializeField] private UpgradePopup _upgradePopup = null;
        [SerializeField] private MoneyWrapper _money = null;
        [SerializeField] private int _baseMaxMoney = 500;

        private RoomHolder _selectedHolder = null;

        public int MaxRoomsCount => _roomHolders.Count(x => x.IsAvailable);

        private void Start()
        {
            HideAllHolders();

            UpdateVisual();
        }

        private void OnEnable()
        {
            foreach (var roomHolder in _roomHolders)
            {
                roomHolder.Clicked += Select;
                roomHolder.RoomAdded += OnRoomAdded;
                roomHolder.RoomUpgraded += OnRoomUpgraded;
            }

            _upgradePopup.Closed += CancelSelection;
            _upgradePopup.UpgradeRequested += UpgradeSelectedHolder;
        }

        private void OnDisable()
        {
            foreach (var roomHolder in _roomHolders)
            {
                roomHolder.Clicked -= Select;
                roomHolder.RoomAdded -= OnRoomAdded;
                roomHolder.RoomUpgraded -= OnRoomUpgraded;
            }

            _upgradePopup.Closed -= CancelSelection;
            _upgradePopup.UpgradeRequested -= UpgradeSelectedHolder;
        }

        private void OnValidate()
        {
            _roomHolders = GetComponentsInChildren<RoomHolder>().ToList();
        }

        public void CancelSelection()
        {
            _selectedHolder = null;

            _upgradePopup.Hide();
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
                _upgradePopup.Toggle();
            }
            else if (holder != null)
            {
                _upgradePopup.Show();

                _upgradePopup.Initialize(holder.Room);
            }
            else
            {
                _upgradePopup.Hide();
            }

            _selectedHolder = holder;
        }

        public IEnumerable<T> GetRoomsOfType<T>() where T : IReadOnlyRoom
        {
            return _roomHolders.Select(x => x.Room).Where(x => x != null).Where(x => x is T).Cast<T>();
        }

        public void ShowEmptyHolders(Room roomPrefab)
        {
            foreach (var roomHolder in _roomHolders)
            {
                if (!roomHolder.CanBuild(roomPrefab))
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

            if (_money.CanWithdraw(price))
            {
                _money.TryWithdraw(price);

                _selectedHolder.UpgradeRoom();
            }
        }

        private void UpdateVisual()
        {
            var room = GetRoomsOfType<StrategyRoom>().FirstOrDefault();

            var level = 0;
            if (room != null)
            {
                level = room.Level;
            }

            for (int i = 0; i < _upgrades.Count; i++)
            {
                if (i <= level)
                {
                    var upgrade = _upgrades[i];
                    foreach (var holder in upgrade.Holders)
                    {
                        holder.Activate();
                    }

                    if (i == level)
                    {
                        _meshFilter.mesh = upgrade.Mesh;
                    }
                }
            }
        }

        private void UpdateMoneyCapacity()
        {
            var sum = 0;

            foreach (var holder in _roomHolders)
            {
                if (holder.Room is StrategyRoom strategyRoom)
                {
                    sum += strategyRoom.MoneyCapacity;
                }
                if (holder.Room is BankRoom bankRoom)
                {
                    sum += bankRoom.Capacity;
                }
            }

            _money.SetMaxValue(_baseMaxMoney + sum);
        }

        private void OnRoomAdded(IReadOnlyRoom room)
        {
            if (room is StrategyRoom strategyRoom)
            {
                strategyRoom.Initialize(_upgrades.Count - 1);

                //UpdateVisual();
                UpdateMoneyCapacity();
            }
            if (room is BankRoom bankRoom)
            {
                UpdateMoneyCapacity();
            }

            RoomAdded?.Invoke(room);
        }

        private void OnRoomUpgraded(IReadOnlyRoom room)
        {
            if (room is StrategyRoom strategyRoom)
            {
                UpdateVisual();
                UpdateMoneyCapacity();
            }
            if (room is BankRoom bankRoom)
            {
                UpdateMoneyCapacity();
            }

            RoomUpgraded?.Invoke(room);
        }
    }
}
