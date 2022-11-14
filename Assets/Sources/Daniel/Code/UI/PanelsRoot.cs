using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel.Extensions;
using System.Linq;

namespace TowerDefense.Daniel.UI
{
    [RequireComponent(typeof(RectTransform))]
    [DisallowMultipleComponent]
    [ExecuteAlways]
    public class PanelsRoot : Panel
    {
        [SerializeField, HideInInspector] private List<Panel> _children = new List<Panel>();

        private Panel _runtimeCurrent = null;
        private Panel _current = null;
        private bool _isEnabled = false;

        public Panel CurrentPanel => _current;

        /*protected override void Awake()
        {
            base.Awake();

            _isEnabled = false;
        }*/

        /*protected override void Start()
        {
            base.Start();
        }*/

        private void OnEnable()
        {
            _isEnabled = true;

            foreach (var child in _children)
            {
#if UNITY_EDITOR
                child.FieldChanged += OnChildFieldChanged;
#endif
                child.Showed += OnChildShowed;
            }

#if UNITY_EDITOR
            UnityEditor.Selection.selectionChanged += OnSelectionChanged;
#endif
        }

        private void OnDisable()
        {
            foreach (var child in _children)
            {
#if UNITY_EDITOR
                child.FieldChanged -= OnChildFieldChanged;
#endif
                child.Showed -= OnChildShowed;
            }

#if UNITY_EDITOR
            UnityEditor.Selection.selectionChanged -= OnSelectionChanged;
#endif
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (_isEnabled)
            {
                OnDisable();
            }

            _children.Clear();
            _children.AddRange(transform.Cast<Transform>().Select(x => x.GetComponent<Panel>()).Where(x => x != null));

            if (_isEnabled)
            {
                OnEnable();
            }
        }

        private void OnChildFieldChanged(Panel child)
        {
            if (child == _runtimeCurrent && !GetIsCurrent(child))
            {
                SetIsCurrent(child, true);
            }
            
            if (child != _runtimeCurrent && GetIsCurrent(child))
            {
                if (_runtimeCurrent != null)
                {
                    SetIsCurrent(_runtimeCurrent, false);
                }

                _runtimeCurrent = child;
            }
        }

        private void OnChildShowed(Panel child)
        {
            if (_current != null)
            {
                _current.Hide();
            }

            _current = child;
        }

#if UNITY_EDITOR
        private void OnSelectionChanged()
        {
            if (UnityEditor.EditorApplication.isPlaying)
            {
                return;
            }

            if (this == null)
            {
                UnityEditor.Selection.selectionChanged -= OnSelectionChanged;

                return;
            }

            //var panels = UnityEditor.Selection.GetFiltered<Panel>(UnityEditor.SelectionMode.ExcludePrefab | UnityEditor.SelectionMode.Assets);
            var transforms = UnityEditor.Selection.GetTransforms(UnityEditor.SelectionMode.ExcludePrefab | UnityEditor.SelectionMode.Assets);

            var panelsCount = 0;
            foreach (var transform in transforms)
            {
                if (transform.IsChildOf(transform) && transform.TryGetComponentInParent<Panel>(out var panel))
                {
                    panel.Show();

                    panelsCount++;
                }

                if (this.transform == transform)
                {
                    panelsCount--;
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
