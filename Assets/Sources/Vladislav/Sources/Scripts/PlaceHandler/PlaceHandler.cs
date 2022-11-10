using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHandler : MonoBehaviour
{
    [SerializeField] private List<Place> _places;
    [SerializeField] private WarriorsHandler _warriorsHandler;
    [SerializeField] private GridLayoutGroup _warriorsPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Camera _startCamera;
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private Canvas _prepairCanvas;
    [SerializeField] private Canvas _battleCanvas;

    private List<Place> _activePlaces;
    private List<GameObject> _activeView;

    private void OnEnable()
    {
        _startButton.onClick.AddListener(StartGame);
        _activeView = _warriorsHandler.GetActiveWarriors();
        _activePlaces = GetActivePlaces();
        _warriorsPanel.enabled = true;
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
        foreach (var place in _activePlaces)
        {
            if (place.IsEmpty == false)
                return true;
        }

        return false;
    }

    private void StartGame()
    {
        _startCamera.gameObject.SetActive(false);
        _mainCamera.gameObject.SetActive(true);
        _prepairCanvas.gameObject.SetActive(false);
        _battleCanvas.gameObject.SetActive(true);

        foreach (var place in _activePlaces)
        {
            if (place.IsEmpty == false)
                place.CreateWarrior(_enemyDetector);
        }
    }

    private List<Place> GetActivePlaces()
    {
        List<Place> tempPlaces = new List<Place>();

        foreach (var place in _places)
        {
            if (place.IsActive)
            {
                tempPlaces.Add(place);
            }
        }

        for (int i = 0; i < _activeView.Count; i++)
        {
            Instantiate(_activeView[i], _warriorsPanel.transform);
        }

        Invoke("Deactivate", 0.1f);
        return tempPlaces;
    }

    private void Deactivate()
    {
        _warriorsPanel.enabled = false;
    }
}
