using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel
{
    public class RoomHolder : MonoBehaviour, IPointerClickHandler
    {
        public event Action<RoomHolder> Clicked = null;
        public event Action<IReadOnlyRoom> RoomAdded = null;
        public event Action<IReadOnlyRoom> RoomUpgraded = null;

        [SerializeField] private SpriteRenderer _buildOverlay = null;
        [SerializeField] private Transform _background = null;

        private IRoom _room = null;

        public IReadOnlyRoom Room => _room;
        public bool IsEmpty => _room == null;

        private void Awake()
        {
            _room = GetComponentInChildren<IRoom>();

            UpdateBackground();
        }

        private void OnEnable()
        {
            if (_room != null)
            {
                _room.Upgraded += OnRoomUpgraded;
            }

        }

        private void OnDisable()
        {
            if (_room != null)
            {
                _room.Upgraded -= OnRoomUpgraded;
            }
        }

        public void BuildRoom(IRoom roomPrefab)
        {
            if (_room != null)
            {
                _room.Upgraded -= OnRoomUpgraded;
            }

            _room = roomPrefab.Instantiate(transform.position, transform.rotation, transform);

            RoomAdded?.Invoke(_room);

            _room.Upgraded += OnRoomUpgraded;

            UpdateBackground();
        }

        public void DestroyRoom()
        {
            _room.Destroy();

            _room = null;
        }

        public void ShowBuildOverlay()
        {
            _buildOverlay.gameObject.SetActive(true);
        }

        public void HideBuildOverlay()
        {
            _buildOverlay.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked?.Invoke(this);
        }

        private void UpdateBackground()
        {
            _background.gameObject.SetActive(_room == null);
        }

        private void OnRoomUpgraded(IReadOnlyRoom room)
        {
            RoomUpgraded?.Invoke(room);
        }
    }
}
