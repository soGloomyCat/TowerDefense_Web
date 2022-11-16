using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected float Speed;

    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(Damage);
    }

    public void PrepairFly(Enemy enemy)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Fly(enemy));
    }

    protected abstract IEnumerator Fly(Enemy enemy);
}
