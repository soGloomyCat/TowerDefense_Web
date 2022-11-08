using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Button _spawnButton;

    private void OnEnable()
    {
        _spawnButton.onClick.AddListener(Spawn);
    }

    private void OnDisable()
    {
        _spawnButton.onClick.RemoveListener(Spawn);
    }

    private void Spawn()
    {
        int randomIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);
        Enemy tempEnemy = Instantiate(_enemyPrefab, _spawnPoints[randomIndex]);
        tempEnemy.Inizialize(_target);
    }
}
