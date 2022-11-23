using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Daniel.UI
{
    [RequireComponent(typeof(Toggle))]
    public class AudioToggle : MonoBehaviour
    {
        [SerializeField] private MusicPlayer _musicPlayer = null;

        private Toggle _toggle = null;

        private void Awake()
        {
            _toggle = GetComponent<Toggle>();

            OnToggled();
        }

        private void OnEnable()
        {
            _toggle.Toggled += OnToggled;
        }

        private void OnDisable()
        {
            _toggle.Toggled -= OnToggled;
        }

        private void OnToggled()
        {
            _musicPlayer.enabled = _toggle.Enabled;
        }
    }
}