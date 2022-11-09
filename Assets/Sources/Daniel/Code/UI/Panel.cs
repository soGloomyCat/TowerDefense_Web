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

        [SerializeField] private bool _isCurrent = false;

        private static Panel _runtimeCurrent = null;
        private static Panel _current = null;

        private RectTransform _rectTransform = null;
        private Vector2 _targetSize = Vector2.zero;

        public Panel CurrentPanel => _current;
        public bool IsActive => this == _current;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_isCurrent)
            {
                Show();

                return;
            }

            Hide();
        }

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            UnityEditor.Selection.selectionChanged += OnSelectionChanged;
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            UnityEditor.Selection.selectionChanged -= OnSelectionChanged;
#endif
        }

        private void OnValidate()
        {
            if (this == _runtimeCurrent && !_isCurrent)
            {
                _isCurrent = true;
            }

            if (_isCurrent)
            {
                if (_runtimeCurrent != null)
                {
                    _runtimeCurrent._isCurrent = false;
                }

                _runtimeCurrent = this;
            }
        }

        private void Update()
        {
            _rectTransform.sizeDelta = Vector2.Lerp(_rectTransform.sizeDelta, _targetSize, _SizeAnimationSpeedMultiplier * Time.deltaTime);
        }

        public void Show()
        {
            if (_current != null)
            {
                _current.Hide();
            }

            _targetSize = Vector2.zero;

            _current = this;

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

#if UNITY_EDITOR
        private void OnSelectionChanged()
        {
            //var panels = UnityEditor.Selection.GetFiltered<Panel>(UnityEditor.SelectionMode.ExcludePrefab | UnityEditor.SelectionMode.Assets);
            var transforms = UnityEditor.Selection.GetTransforms(UnityEditor.SelectionMode.ExcludePrefab | UnityEditor.SelectionMode.Assets);

            var panelsCount = 0;
            foreach (var transform in transforms)
            {
                if (transform.TryGetComponentInParent<Panel>(out var panel))
                {
                    panel.Show();

                    panelsCount++;
                }
            }

            if (panelsCount < 1)
            {
                if (_runtimeCurrent != null)
                {
                    _runtimeCurrent.Show();
                }
                else if (_current != null)
                {
                    _current.Hide();
                }
            }
        }
#endif
    }
}
