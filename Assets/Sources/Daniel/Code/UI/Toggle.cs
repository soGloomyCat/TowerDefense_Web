using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense.Daniel.UI
{
    [RequireComponent(typeof(Button))]
    [ExecuteAlways]
    public class Toggle : MonoBehaviour
    {
        public event Action Toggled = null;

        [SerializeField] private Image _targetImage = null;
        [SerializeField] private Sprite _spriteEnabled = null;
        [SerializeField] private Sprite _spriteDisabled = null;
        [SerializeField] private bool _enabled = false;

        private Button _button = null;

        public bool Enabled => _enabled;

        private void Awake()
        {
            _button = GetComponent<Button>();

            UpdateVisual();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnValidate()
        {
            UpdateVisual();
        }

        private void OnButtonClicked()
        {
            _enabled = !_enabled;

            UpdateVisual();

            Toggled?.Invoke();
        }
        
        private void UpdateVisual()
        {
            _targetImage.sprite = _enabled ? _spriteEnabled : _spriteDisabled;
        }
    }
}