using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private const string AttackTrigger = "RangeAttack";

    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _cooldown;

    private EnemyDetector _enemyDetector;
    private bool _canAttack;
    private float _currentTime;

    private void Update()
    {
        if (_currentTime >= _cooldown)
        {
            _currentTime = 0;
            _canAttack = true;
        }

        _currentTime += Time.deltaTime;
    }

    private void OnDisable()
    {
        _enemyDetector.EnemyFounded -= Attack;
    }

    public void Inizialize(EnemyDetector enemyDetector)
    {
        _enemyDetector = enemyDetector;
        _enemyDetector.EnemyFounded += Attack;
        _canAttack = true;
        _currentTime = _cooldown;
    }

    private void Attack(Enemy enemy)
    {
        if (_canAttack)
        {
            _canAttack = false;
            Vector3 tempDirection = enemy.transform.position + transform.position;
            transform.rotation = Quaternion.LookRotation(tempDirection, Vector3.up);
            _animator.SetTrigger(AttackTrigger);
            Arrow tempArrow = Instantiate(_arrowPrefab);
            tempArrow.transform.position = _spawnPosition.position;
            tempArrow.PrepairFly(enemy);
        }
    }
}
