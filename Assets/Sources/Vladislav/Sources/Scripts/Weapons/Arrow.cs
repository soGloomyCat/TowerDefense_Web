using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    private Coroutine _coroutine;

    public void PrepairFly(Enemy enemy)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Fly(enemy));
    }

    private IEnumerator Fly(Enemy enemy)
    {
        if (enemy != null)
        {
            while (enemy != null && Vector3.Distance(transform.position, enemy.TargetPoint.position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, enemy.TargetPoint.position, _speed * Time.deltaTime);
                transform.LookAt(enemy.transform);
                yield return null;
            }

            if (enemy != null)
                enemy.TakeDamage(_damage);
        }
        else
        {
            float currentTime = 0;
            float lifeTime = 3f;

            while (currentTime <= lifeTime)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, _speed * Time.deltaTime);
                yield return null;
            }
        }

        Destroy(gameObject);
    }
}
