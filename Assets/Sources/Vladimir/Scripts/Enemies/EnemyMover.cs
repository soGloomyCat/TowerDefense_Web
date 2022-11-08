using UnityEngine;
using UnityEngine.Events;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private bool _isEnabled;
    private Vector3 _localTargetPosition;

    public event UnityAction DestinationReached;

    private void Update()
    {
        if (_isEnabled)
            Movement();
    }

    public void Move(Vector3 localTargetPosition)
    {
        _localTargetPosition = localTargetPosition;
        _isEnabled = true;
    }

    public void StopMove()
    {
        _isEnabled = false;
    }

    private void Movement()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, _localTargetPosition, Time.deltaTime * _speed);

        if (Vector3.Distance(transform.localPosition, _localTargetPosition) < 0.001f)
        {
            StopMove();
            DestinationReached?.Invoke();
        }
    }
}
