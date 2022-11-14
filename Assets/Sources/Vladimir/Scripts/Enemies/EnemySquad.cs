using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySquad : MonoBehaviour
{
    [SerializeField] private CastleTargets _castleTargets;

    //private List<Enemy> _enemies = new List<Enemy>();
    private SquadFormations _formations;
    //private Vector3 _currentTarget;
    private int _waveNumber;

    public event UnityAction<int> AllEnemiesKilled;

    private void Awake()
    {
        //_currentTarget = _castleTargets.Castle.position;
        _formations = new SquadFormations(_castleTargets.Castle);
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
        //_enemies.Add(enemy);
        _formations.Add(enemy);
        //enemy.SetTarget(_currentTarget);
        enemy.Dead += OnEnemyDead;
    }

    public void Clear()
    {
        /*
        foreach (Enemy enemy in _enemies)
            Destroy(enemy.gameObject);

        _enemies.Clear();
        */

        _formations.Clear();
    }

    public void StopAttack()
    {
        /*
        foreach (Enemy enemy in _enemies)
            enemy.StopAttack();
        */

        _formations.StopAttack();
    }

    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        //_enemies.Remove(enemy);
        _formations.Remove(enemy);

        //if (_enemies.Count == 0)
        if (_formations.Count == 0)
            AllEnemiesKilled?.Invoke(_waveNumber);
    }

    private void OnTargetChange(FakeTarget target)
    {
        _formations.OnTargetChange(target);

        //_currentTarget = target.position;
        /*
        foreach (Enemy enemy in _enemies)
            enemy?.SetTarget(_currentTarget);
        */
    }

    private class SquadFormations
    {
        private List<Enemy> _farEnemies = new List<Enemy>();
        private List<Enemy> _closeEnemies = new List<Enemy>();

        private Transform _castle;

        public SquadFormations(Transform castle)
        { 
            _castle = castle;
        }

        public int Count 
        {
            get
            {
                return _farEnemies.Count + _closeEnemies.Count;
            }
        }

        public void Add(Enemy enemy)
        {
            if (enemy is CloseEnemy)
            {
                _closeEnemies.Add(enemy);
                enemy.SetTarget(_castle.position);
            }
            else if (enemy is FarEnemy)
            { 
                _farEnemies.Add(enemy);
                enemy.SetTarget(_castle.position);
            }
        }

        public void Clear()
        {
            foreach (Enemy farEnemy in _farEnemies)
                Destroy(farEnemy.gameObject);

            foreach (Enemy closeEnemy in _closeEnemies)
                Destroy(closeEnemy.gameObject);

            _farEnemies.Clear();
            _closeEnemies.Clear();
        }

        public void StopAttack()
        {
            foreach (Enemy farEnemy in _farEnemies)
                farEnemy.StopAttack();

            foreach (Enemy closeEnemy in _closeEnemies)
                closeEnemy.StopAttack();
        }

        public void Remove(Enemy enemy)
        {
            if (_farEnemies.Contains(enemy))
                _farEnemies.Remove(enemy);
            else if (_closeEnemies.Contains(enemy))
                _closeEnemies.Remove(enemy);
        }

        public void OnTargetChange(FakeTarget fakeTarget)
        {
            if (fakeTarget.TryGetComponent(out CloseTarget closeTarget))
            {
                closeTarget.TargetDestroyed += OnCloseTargetDestroy;

                foreach (Enemy enemy in _closeEnemies)
                    enemy.SetTarget(closeTarget.transform.position);
            }
            else if (fakeTarget.TryGetComponent(out FarTarget farTarget))
            {
                farTarget.TargetDestroyed += OnFarTargetDestroy;

                foreach (Enemy enemy in _farEnemies)
                    enemy.SetTarget(farTarget.transform.position);
            }
            else if (fakeTarget.TryGetComponent(out BothTarget bothTarget))
            {
                bothTarget.TargetDestroyed += OnBothTargetDestroy;

                foreach (Enemy enemy in _farEnemies)
                    enemy.SetTarget(bothTarget.transform.position);

                foreach (Enemy enemy in _closeEnemies)
                    enemy.SetTarget(bothTarget.transform.position);
            }
        }

        private void OnCloseTargetDestroy(FakeTarget target)
        {
            target.TargetDestroyed += OnCloseTargetDestroy;

            foreach (Enemy enemy in _closeEnemies)
                enemy.SetTarget(_castle.position);
        }

        private void OnFarTargetDestroy(FakeTarget target)
        {
            target.TargetDestroyed -= OnFarTargetDestroy;

            foreach (Enemy enemy in _farEnemies)
                enemy.SetTarget(_castle.position);
        }

        private void OnBothTargetDestroy(FakeTarget target)
        {
            target.TargetDestroyed -= OnBothTargetDestroy;

            foreach (Enemy enemy in _farEnemies)
                enemy.SetTarget(_castle.position);

            foreach (Enemy enemy in _closeEnemies)
                enemy.SetTarget(_castle.position);
        }
    }
}
