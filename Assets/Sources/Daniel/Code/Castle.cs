using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TowerDefense.Daniel
{
    public class Castle : MonoBehaviour
    {
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
            }
        }

        private void OnDisable()
        {
            foreach (var roomHolder in _roomHolders)
            {
                roomHolder.Clicked -= Select;
            }
        }

        private void OnValidate()
        {
            _roomHolders = GetComponentsInChildren<RoomHolder>().ToList();
        }

        public void Select(RoomHolder holder)
        {
            _selectedHolder = holder;
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
    }
}
