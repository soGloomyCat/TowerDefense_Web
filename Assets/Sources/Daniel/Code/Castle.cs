using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Newtonsoft.Json;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.Rooms;
using TowerDefense.Daniel.UI;

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

        [Serializable]
        private class CastleState
        {
            [field: SerializeField] public Dictionary<int, (string id, int level)> Rooms { get; private set; } = new Dictionary<int, (string id, int level)>();
        }

        private const string _CastleKey = "Castle";

        public event Action<RoomHolder> ClickedOnEmptyHolder = null;
        public event Action<Dictionary<int, (string id, int level)>> Loading = null;
        public event Action<IReadOnlyRoom> RoomAdded = null;
        public event Action<IReadOnlyRoom> RoomUpgraded = null;

        [SerializeField, HideInInspector] private List<RoomHolder> _roomHolders = new List<RoomHolder>();
        [SerializeField] private List<CastleUpgrade> _upgrades = new List<CastleUpgrade>();
        [SerializeField] private MeshFilter _meshFilter = null;
        [SerializeField] private UpgradePopup _upgradePopup = null;
        [SerializeField] private MoneyWrapper _money = null;
        [SerializeField] private int _baseMaxMoney = 500;

        private RoomHolder _selectedHolder = null;

        public IReadOnlyList<RoomHolder> RoomHolders => _roomHolders;
        public int MaxRoomsCount => _roomHolders.Count(x => x.IsAvailable);

        private void Start()
        {
            LoadState();

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

        private void LoadState()
        {
            var state = JsonConvert.DeserializeObject<CastleState>(PlayerPrefs.GetString(_CastleKey));

            /*foreach (var room in state.Rooms)
            {
                var holder = _roomHolders[room.Key];

                if (holder.TryBuildRoom())
                {
                    while (holder.Room.Level < room.Value.level)
                    {
                        holder.UpgradeRoom();
                    }
                }
            }*/

            Loading?.Invoke(state.Rooms);
        }

        private void SaveState()
        {
            var state = new CastleState();

            for (int i = 0; i < _roomHolders.Count; i++)
            {
                var holder = _roomHolders[i];

                if (holder.IsEmpty)
                {
                    continue;
                }

                state.Rooms.Add(i, (holder.Room.Information.InternalID, holder.Room.Level));
            }

            PlayerPrefs.SetString(_CastleKey, JsonConvert.SerializeObject(state));
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

            SaveState();

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

            SaveState();

            RoomUpgraded?.Invoke(room);
        }
    }
}
