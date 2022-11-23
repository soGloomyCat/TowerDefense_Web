using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private List<Place> _places;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _warriorsView;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private Transform _trashBox;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AdHandler _adHandler;

    private void OnEnable()
    {
        _adHandler.AdFinished += Clean;
        _adHandler.RewardAdFinished += Clean;
    }

    private void OnDisable()
    {
        _adHandler.AdFinished -= Clean;
        _adHandler.RewardAdFinished -= Clean;
    }

    private void Clean()
    {
        foreach (var place in _places)
        {
            place.DestroyWarrior();
        }

        for (int i = 0; i < _container.childCount; i++)
        {
            Destroy(_container.GetChild(i).gameObject);
        }

        for (int i = 0; i < _warriorsView.childCount; i++)
        {
            Destroy(_warriorsView.GetChild(i).gameObject);
        }

        _enemyDetector.Clean();

        for (int i = 0; i < _trashBox.childCount; i++)
        {
            Destroy(_trashBox.GetChild(i).gameObject);
        }

        _enemySpawner.Clean();
    }
}
