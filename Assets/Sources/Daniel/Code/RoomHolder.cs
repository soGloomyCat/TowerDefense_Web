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
        [SerializeField] private Transform _unvailableBackground = null;
        [SerializeField] private Transform _availableBackground = null;
        [SerializeField] private ParticleSystem _buildParticles = null;
        [SerializeField] private Room _concreteRoomType = null;

        private Room _room = null;
        private bool _isAvailable = false;

        public IReadOnlyRoom Room => _room;
        public bool IsEmpty => _room == null;
        public bool AllowAnyType => _concreteRoomType == null;
        public bool IsAvailable => _isAvailable;

        private void Awake()
        {
            _room = GetComponentInChildren<Room>();

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

        public void Activate()
        {
            _isAvailable = true;

            UpdateBackground();
        }

        public bool CanBuild(Room roomPrefab)
        {
            return IsAvailable && IsEmpty && ((AllowAnyType && !roomPrefab.NeedConcreteHolder) || (!AllowAnyType && roomPrefab.GetType() == _concreteRoomType.GetType()));
        }

        public bool TryBuildRoom(Room roomPrefab)
        {
            //if ((!AllowAnyType || roomPrefab.NeedConcreteHolder) && (AllowAnyType || roomPrefab.GetType() != _concreteRoomType.GetType()))
            if (!CanBuild(roomPrefab))
            {
                return false;
            }

            if (_room != null)
            {
                _room.Upgraded -= OnRoomUpgraded;
            }

            _room = Instantiate(roomPrefab, transform.position, transform.rotation, transform);

            RoomAdded?.Invoke(_room);

            _room.Upgraded += OnRoomUpgraded;

            UpdateBackground();

            _buildParticles.Play();

            return true;
        }

        public void UpgradeRoom()
        {
            _room.Upgrade();
        }

        public void DestroyRoom()
        {
            Destroy(_room.gameObject);

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
            _unvailableBackground.gameObject.SetActive(!_isAvailable && _room == null);
            _availableBackground.gameObject.SetActive(_isAvailable && _room == null);
        }

        private void OnRoomUpgraded(IReadOnlyRoom room)
        {
            RoomUpgraded?.Invoke(room);
        }
    }
}
