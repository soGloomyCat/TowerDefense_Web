using System;
using UnityEngine;
using TowerDefense.Daniel.Extensions;

namespace TowerDefense.Daniel.UI
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class Panel : MonoBehaviour
    {
        private const float _SizeAnimationSpeedMultiplier = 10;

#if UNITY_EDITOR
        public event Action<Panel> FieldChanged = null;
#endif
        public event Action<Panel> Showed = null;

        [SerializeField] private bool _isCurrent = false;

        private RectTransform _rectTransform = null;
        private Vector2 _targetSize = Vector2.zero;

        public bool IsActive => true;

        protected virtual void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        protected virtual void Start()
        {
            if (_isCurrent)
            {
                Show();

                return;
            }

            Hide();
        }

        protected virtual void OnValidate()
        {
            /*if (this == _runtimeCurrent && !_isCurrent)
            {
                _isCurrent = true;
            }
            
            if (this != _runtimeCurrent && _isCurrent)
            {
                if (_runtimeCurrent != null)
                {
                    _runtimeCurrent._isCurrent = false;
                }

                _runtimeCurrent = this;
            }*/

#if UNITY_EDITOR
            FieldChanged?.Invoke(this);
#endif
        }

        private void Update()
        {
            _rectTransform.sizeDelta = Vector2.Lerp(_rectTransform.sizeDelta, _targetSize, _SizeAnimationSpeedMultiplier * Time.deltaTime);
        }

        public void Show()
        {
            Showed?.Invoke(this);

            _targetSize = Vector2.zero;

#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            _rectTransform.sizeDelta = _targetSize;
#endif
        }

        public void Hide()
        {
            _targetSize = _rectTransform.rect.size;

#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            _rectTransform.sizeDelta = _targetSize;
#endif
        }

        protected bool GetIsCurrent(Panel panel)
        {
            return panel._isCurrent;
        }

        protected void SetIsCurrent(Panel panel, bool state)
        {
            panel._isCurrent = state;
        }
    }
}
