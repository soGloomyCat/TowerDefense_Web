using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : Weapon
{
    [SerializeField] private TargetSearcher _searchCollider;
    [SerializeField] private int _chainCount;

    private Enemy _currentEnemy;
    private int _currentChainCount = 1;

    private void OnEnable()
    {
        _searchCollider.TargetFounded += ChangeTarget;
    }

    private void OnDisable()
    {
        _searchCollider.TargetFounded -= ChangeTarget;
    }

    protected override IEnumerator Fly(Enemy enemy)
    {
        _currentEnemy = enemy;

        while (_currentEnemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _currentEnemy.transform.position, Speed * Time.deltaTime);
            transform.LookAt(_currentEnemy.transform);

            if (Vector3.Distance(transform.position, _currentEnemy.transform.position) <= 0.1f)
                yield return OnWait();

            if (_currentChainCount > _chainCount)
                _currentEnemy = null;

            yield return null;
        }

        Destroy(gameObject);
    }

    private bool OnWait()
    {
        Enemy tempEnemy = _currentEnemy;
        _currentChainCount++;
        _searchCollider.Activate();

        while (_currentEnemy == tempEnemy)
            return false;

        return true;
    }

    private void ChangeTarget(Enemy enemy)
    {
        _currentEnemy = enemy;
    }
}
