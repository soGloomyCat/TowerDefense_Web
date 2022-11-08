using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

namespace TowerDefense.Daniel
{
    public class InputPanel : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action MoveStarted = null;
        public event Action<Vector2> Moved = null;
        public event Action<Vector2> MoveFinished = null;
        public event Action<float, float, Vector2> Zoomed = null;

        private float _inputMultiplier = 1;
        private float _lastPointersDistance = 0;
        private RectTransform _transform = null;
        private Vector2 _basePointerPosition = Vector2.zero;
        private FixedSizedQueue<Vector2> _movingAcceleration = new FixedSizedQueue<Vector2>(5);

        private void Awake()
        {
            _transform = GetComponent<RectTransform>();
        }

        public void OnInitializePotentialDrag(PointerEventData eventData)
        {
            if (Input.touchCount > 1)
            {
                eventData.Use();
                return;
            }

            eventData.useDragThreshold = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Input.touchCount <= 1)
            {
                _basePointerPosition = eventData.position;
                _lastPointersDistance = 0;

                MoveStarted?.Invoke();
            }
            else if (Input.touchCount == 2)
            {
                _basePointerPosition = (Input.touches[0].position + Input.touches[1].position) / 2;
                _lastPointersDistance = Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                MoveStarted?.Invoke();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            var offset = (eventData.position - _basePointerPosition) * _inputMultiplier;

            Moved?.Invoke(offset);

            if (Input.touchCount <= 1)
            {
                var delta = eventData.delta;
                _movingAcceleration.Enqueue(delta);

                Moved?.Invoke(eventData.position - _basePointerPosition);
            }
            else if (Input.touchCount == 2)
            {
                var moveDelta = eventData.delta;
                _movingAcceleration.Enqueue(moveDelta);

                var dist = _lastPointersDistance - Vector2.Distance(Input.touches[0].position, Input.touches[1].position);

                Moved?.Invoke((Input.touches[0].position + Input.touches[1].position) / 2 - _basePointerPosition);

                var zoomDelta = Vector2.Distance(Input.touches[0].position, Input.touches[1].position) /
                               Vector2.Distance(Input.touches[0].position - Input.touches[0].deltaPosition, Input.touches[1].position - Input.touches[1].deltaPosition) - 1;

                Zoomed?.Invoke(zoomDelta, dist / 24, (Input.touches[0].position + Input.touches[1].position) / 2);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            int curTouches = Input.touchCount - 1;
            if (curTouches <= 0)
            {
                var moveDelta = Vector2.zero;

                if (eventData.IsPointerMoving())
                {
                    moveDelta = eventData.delta;

                    int count = _movingAcceleration.Count;
                    for (int i = 0; i < count; i++)
                    {
                        _movingAcceleration.TryDequeue(out var moving);
                        moveDelta += moving;
                    }
                }

                MoveFinished?.Invoke(moveDelta);
            }
            else if (curTouches == 1)
            {
                //(Input.touches[0].position - Input.touches[1].position) / 2
                _basePointerPosition = Input.touches.First(x => x.fingerId != eventData.pointerId).position;

                OnBeginDrag(eventData);
            }
        }

        public Bounds GetBounds()
        {
            return new Bounds(_transform.anchoredPosition, _transform.rect.size);
        }
    }
}
