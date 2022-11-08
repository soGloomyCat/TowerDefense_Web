using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHandler : MonoBehaviour
{
    [SerializeField] private List<Place> _places;
    [SerializeField] private Button _startButton;
    [SerializeField] private Camera _startCamera;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private Canvas _battleCanvas;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
    }

    private void Update()
    {
        if (CheckPlaces())
            _startButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _startButton.onClick.RemoveListener(StartGame);
    }

    private bool CheckPlaces()
    {
        foreach (var place in _places)
        {
            if (place.IsEmpty)
                return false;
        }

        return true;
    }

    private void StartGame()
    {
        _startCamera.gameObject.SetActive(false);
        _mainCamera.gameObject.SetActive(true);

        foreach (var place in _places)
        {
            place.CreateWarrior(_enemyDetector);
        }

        _battleCanvas.gameObject.SetActive(true);
    }
}
