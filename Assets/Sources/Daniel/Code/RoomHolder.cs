using System;
using UnityEngine;
using UnityEngine.EventSystems;
using TowerDefense.Daniel.Interfaces;

namespace TowerDefense.Daniel
{
    public class RoomHolder : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer _buildOverlay = null;

        public event Action<RoomHolder> Clicked = null;

        private IRoom _room = null;

        public bool IsEmpty => _room == null;

        private void Awake()
        {
            _room = GetComponentInChildren<IRoom>();
        }

        public void BuildRoom(IRoom roomPrefab)
        {
            _room = roomPrefab.Instantiate(transform.position, transform.rotation, transform);
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
    }
}
