using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltimateHandler : MonoBehaviour
{
    private const float Cooldown = 3f;

    [SerializeField] private Button _button;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private Arrow _arrowPrefab;

    private float _currentTime;

    private void OnEnable()
    {
        _button.onClick.AddListener(ApplyUltimate);
    }

    private void Awake()
    {
        _button.interactable = false;
        _currentTime = 0;
    }

    private void FixedUpdate()
    {
        _currentTime += Time.fixedDeltaTime;

        if (_currentTime >= Cooldown && _button.interactable == false)
            _button.interactable = true;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ApplyUltimate);
    }

    private void ApplyUltimate()
    {
        _currentTime = 0;
        _button.interactable = false;

        foreach (var spawnPoint in _spawnPoints)
        {
            Arrow tempArrow = Instantiate(_arrowPrefab, spawnPoint);
            tempArrow.PrepairFly(null);
        }
    }
}
