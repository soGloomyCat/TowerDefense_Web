using UnityEngine;
using TowerDefense.Daniel.Extensions;

namespace TowerDefense.Daniel.UI
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class Panel : MonoBehaviour
    {
        [SerializeField] private bool _isCurrent = false;

        [SerializeField, HideInInspector] private static Panel _runtimeCurrent = null;
        private static Panel _current = null;

        private RectTransform _rectTransform = null;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_runtimeCurrent == this)
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
            if (_isCurrent)
            {
                if (_runtimeCurrent != null)
                {
                    _runtimeCurrent._isCurrent = false;
                }

                _runtimeCurrent = this;
            }
        }

        public void Show()
        {
            if (_current != null)
            {
                _current.Hide();
            }

            _rectTransform.sizeDelta = Vector2.zero;

            _current = this;
        }

        public void Hide()
        {
            _rectTransform.sizeDelta = _rectTransform.rect.size;
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

            if (panelsCount < 1 && _current != null)
            {
                _current.Hide();
            }
        }
#endif
    }
}
