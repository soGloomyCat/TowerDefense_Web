using System.Collections;
using UnityEngine;

public class MagicBall : Weapon
{
    [SerializeField] private TargetSearcher _searchCollider;
    [SerializeField] private int _chainCount;

    private static Enemy _currentEnemy;
    private int _currentChainCount = 1;

    private void OnEnable()
    {
        _searchCollider.TargetFounded += ChangeTarget;
    }

    private void OnDisable()
    {
        _searchCollider.TargetFounded -= ChangeTarget;
    }

    public static bool Check(Enemy enemy)
    {
        if (_currentEnemy == enemy)
            return true;

        return false;
    }

    protected override IEnumerator Fly(Enemy enemy)
    {
        _currentEnemy = enemy;
        Vector3 tempDirection = new Vector3(_currentEnemy.transform.position.x, _currentEnemy.transform.position.y + 1f, _currentEnemy.transform.position.z);

        while (_currentEnemy != null)
        {
            tempDirection = new Vector3(_currentEnemy.transform.position.x, _currentEnemy.transform.position.y + 1f, _currentEnemy.transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, tempDirection, Speed * Time.deltaTime);
            transform.LookAt(tempDirection);

            if (Vector3.Distance(transform.position, tempDirection) <= 0.1f)
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
