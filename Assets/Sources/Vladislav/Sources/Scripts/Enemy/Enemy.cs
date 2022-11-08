using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    private const string WalkTrigger = "IsWalk";

    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _health;
    [SerializeField] private Transform _targetPoint;

    private Rigidbody _rigidbody;
    private Transform _target;
    private Coroutine _coroutine;

    public Transform TargetPoint => _targetPoint;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Inizialize(Transform target)
    {
        _target = target;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Move());
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
            Destroy(gameObject);
    }

    private IEnumerator Move()
    {
        _animator.SetBool(WalkTrigger, true);

        while (CheckPosition())
        {
            _rigidbody.AddForce(Vector3.back * _speed);
            yield return null;
        }

        _animator.SetBool(WalkTrigger, false);
    }

    private bool CheckPosition()
    {
        if (Vector3.Distance(transform.position, _target.position) > 1)
            return true;

        return false;
    }
}
