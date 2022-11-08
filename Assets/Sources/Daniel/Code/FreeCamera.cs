using UnityEngine;
using DG.Tweening;
using Cinemachine;

namespace TowerDefense.Daniel
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public class FreeCamera : MonoBehaviour
    {
        [SerializeField] private InputPanel _viewInput = null;

        [SerializeField] private float _minZoom = 30;
        [SerializeField] private float _maxZoom = 60;

        //[SerializeField] private float _moveSpeed = 1;
        //[SerializeField] private float _zoomSpeed = 1;

        //[SerializeField] private float _transitionsSpeed = 0.4f;

        private CinemachineVirtualCamera _virtualCamera = null;
        private Tween _movingTween = null;
        private Camera _mainCamera = null;
        private Vector3 _currentPoint = Vector3.zero;
        private float _currentSize = 0;

        public Vector3 TargetPoint { get; set; } = Vector3.zero;
        public float TargetSize { get; set; } = 0;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
            _mainCamera = Camera.main;

            _currentPoint = transform.position;
            _currentSize = _virtualCamera.m_Lens.FieldOfView;

            TargetPoint = transform.position;
            TargetSize = _virtualCamera.m_Lens.FieldOfView;
        }

        private void OnEnable()
        {
            _viewInput.MoveStarted += OnMoveStarted;
            _viewInput.Moved += OnMoved;
            _viewInput.MoveFinished += OnMoveFinished;
            _viewInput.Zoomed += OnZoomed;
        }
        private void OnDisable()
        {
            _viewInput.MoveStarted -= OnMoveStarted;
            _viewInput.Moved -= OnMoved;
            _viewInput.MoveFinished -= OnMoveFinished;
            _viewInput.Zoomed -= OnZoomed;
        }

        private void Update()
        {
            /*var vert = Input.GetAxis("Vertical");
            var hor = Input.GetAxis("Horizontal");
            OnMoved(new Vector2(-hor, -vert), CurrentState.Change);*/

            //var before = _mainCamera.ScreenToWorldPoint(Input.mousePosition);

            //if (!Input.GetMouseButton(0))
            {
                _currentSize = _virtualCamera.m_Lens.FieldOfView;
            }
            var delta = Input.mouseScrollDelta.y * 1f;
            OnZoomed(delta, delta, Input.mousePosition);

            //TargetPoint += before - _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        private void LateUpdate()
        {
            var newPosition = Vector3.Lerp(transform.position, TargetPoint, Time.deltaTime * 500);
            var newSize = Mathf.Lerp(_virtualCamera.m_Lens.FieldOfView, TargetSize, Time.deltaTime * 500);

            transform.position = newPosition;
            _virtualCamera.m_Lens.FieldOfView = newSize;
        }

        private void OnMoveStarted()
        {
            _movingTween?.Pause().Kill();

            _currentPoint = transform.position;
            _currentSize = _virtualCamera.m_Lens.FieldOfView;
        }

        private void OnMoved(Vector2 offset)
        {
            TargetPoint = _currentPoint - new Vector3(offset.x, offset.y, 0) / (Screen.height / TargetSize * 6);
        }
        
        private void OnMoveFinished(Vector2 offset)
        {
            offset /= Screen.height / TargetSize;
            //offset *= TargetSize / 3;

            _movingTween = DOTween.To(() => TargetPoint, value => TargetPoint = value, TargetPoint - (Vector3)offset, 1.0f).SetEase(Ease.OutCubic);
        }

        private void OnZoomed(float delta, float dist, Vector2 point)
        {
            if (Mathf.Abs(delta) <= 0)
            {
                return;
            }

            var camSize = _virtualCamera.m_Lens.FieldOfView;

            _virtualCamera.m_Lens.FieldOfView = _currentSize;
            var before = _mainCamera.ScreenToWorldPoint(point);

            TargetSize = Mathf.Clamp(_currentSize + dist, _minZoom, _maxZoom);

            _virtualCamera.m_Lens.FieldOfView = TargetSize;
            TargetPoint += before - _mainCamera.ScreenToWorldPoint(point);

            _virtualCamera.m_Lens.FieldOfView = camSize;
        }

        public void StopMoving()
        {
            _movingTween?.Pause().Kill();
        }
    }
}