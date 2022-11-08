using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    private const string AttackTrigger = "RangeAttack";

    [SerializeField] private Arrow _arrowPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private Transform _point;
    [SerializeField] private Animator _animator;

    private EnemyDetector _enemyDetector;

    private void OnDisable()
    {
        _enemyDetector.EnemyFounded -= Attack;
    }

    public void Inizialize(EnemyDetector enemyDetector)
    {
        _enemyDetector = enemyDetector;
        _enemyDetector.EnemyFounded += Attack;
    }

    private void Attack(Enemy enemy)
    {
        Vector3 tempDirection = enemy.transform.position + transform.position;
        transform.rotation = Quaternion.LookRotation(tempDirection, Vector3.up);
        _animator.SetTrigger(AttackTrigger);
        Arrow tempArrow = Instantiate(_arrowPrefab);
        tempArrow.transform.position = _spawnPosition.position;
        tempArrow.PrepairFly(enemy);
    }
}
