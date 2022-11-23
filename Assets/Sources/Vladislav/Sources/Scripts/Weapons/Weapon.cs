using System.Collections;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected float Damage;
    [SerializeField] protected float Speed;

    private Coroutine _coroutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (this is MagicBall == false)
            {
                enemy.TakeDamage(Damage);
                Destroy(gameObject);
            }
            else if (this is MagicBall && MagicBall.Check(enemy))
            {
                enemy.TakeDamage(Damage);
            }
        }
    }

    public void PrepairFly(Enemy enemy, float damageMultiplier)
    {
        Damage *= damageMultiplier;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(Fly(enemy));
    }

    protected abstract IEnumerator Fly(Enemy enemy);
}
