using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;

    private Rigidbody _rigidbody;
    private Vector3 _direction;
    private float _sideDirection;
    private float _forwardDirection;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _sideDirection = Input.GetAxis("Horizontal");
        _forwardDirection = Input.GetAxis("Vertical");
        _direction = new Vector3(_sideDirection * _speed, 0, _forwardDirection * _speed);

        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(transform.position,
                                   transform.up,
                                   +Input.GetAxis("Mouse X") * _rotateSpeed * Time.deltaTime);
        }

        if (Input.anyKey == false)
            _rigidbody.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        _rigidbody.AddRelativeForce(_direction);
    }
}
