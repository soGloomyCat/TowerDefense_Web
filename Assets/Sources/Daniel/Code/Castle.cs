using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel
{
    public class Castle : MonoBehaviour
    {
        public event Action<RoomHolder> ClickedOnEmptyHolder = null;
        public event Action<IReadOnlyRoom> RoomAdded = null;
        public event Action<IReadOnlyRoom> RoomUpgraded = null;

        [SerializeField, HideInInspector] private List<RoomHolder> _roomHolders = new List<RoomHolder>();

        private RoomHolder _selectedHolder = null;

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
        }

        private void OnDisable()
        {
            foreach (var roomHolder in _roomHolders)
            {
                roomHolder.Clicked -= Select;
                roomHolder.RoomAdded -= OnRoomAdded;
                roomHolder.RoomUpgraded -= OnRoomUpgraded;
            }
        }

        private void OnValidate()
        {
            _roomHolders = GetComponentsInChildren<RoomHolder>().ToList();
        }

        public void Select(RoomHolder holder)
        {
            if (holder.IsEmpty)
            {
                ClickedOnEmptyHolder?.Invoke(holder);

                return;
            }

            _selectedHolder = holder;
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
