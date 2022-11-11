using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySquad : MonoBehaviour
{
    [SerializeField] private CastleTargets _castleTargets;

    private List<Enemy> _enemies = new List<Enemy>();
    private Vector3 _currentTarget;
    private int _waveNumber;

    public event UnityAction<int> AllEnemiesKilled;

    private void Awake()
    {
        _currentTarget = _castleTargets.Castle.position;
    }

    private void OnEnable()
    {
        _castleTargets.TargetChanged += OnTargetChange;
    }

    private void OnDisable()
    {
        _castleTargets.TargetChanged -= OnTargetChange;
    }

    public void OnWaveStart(int waveNumber)
    { 
        _waveNumber = waveNumber;
    }

    public void Add(Enemy enemy)
    {
        _enemies.Add(enemy);
        enemy.SetTarget(_currentTarget);
        enemy.Dead += OnEnemyDead;
    }

    public void Clear()
    {
        foreach (Enemy enemy in _enemies)
            Destroy(enemy.gameObject);

        _enemies.Clear();
    }

    public void StopAttack()
    {
        foreach (Enemy enemy in _enemies)
            enemy.StopAttack();
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
            AllEnemiesKilled?.Invoke(_waveNumber);
    }

    private void OnTargetChange(Transform target)
    {
        _currentTarget = target.position;

        foreach (Enemy enemy in _enemies)
            enemy?.SetTarget(_currentTarget);
    }
}
