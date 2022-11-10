using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _lifeTime;

    private float _currentTime;
    private Coroutine _coroutine;

    private void Awake()
    {
        _currentTime = 0;
    }

    private void Update()
    {
        if (_currentTime >= _lifeTime)
            Destroy(gameObject);

        _currentTime += Time.deltaTime;
    }

    public void PrepairFly(Enemy enemy)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        if (enemy != null)
            _coroutine = StartCoroutine(Fly(enemy));
        else
            _coroutine = StartCoroutine(ActivateUltimate());
    }

    private IEnumerator ActivateUltimate()
    {
        while (_currentTime < _lifeTime)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + Vector3.down, _speed * Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator Fly(Enemy enemy)
    {
        while (enemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, _speed * Time.deltaTime);
            transform.LookAt(enemy.transform);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_damage);

        Destroy(gameObject);
    }
}
