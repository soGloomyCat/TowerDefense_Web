using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private const string TutorialName = "Tutorial";

    [SerializeField] private Tutorial _tutorial;

    private bool _hasTutorialDone;

    private void OnEnable()
    {
        _hasTutorialDone = PlayerPrefs.GetInt(TutorialName, 0) == 1;
        _tutorial.Done += SaveProgressData;
    }

    private void OnDisable()
    {
        _tutorial.Done -= SaveProgressData;
    }

    public void LoadProgressData()
    {
        if (_hasTutorialDone)
            _tutorial.gameObject.SetActive(false);
    }

    private void SaveProgressData()
    {
        PlayerPrefs.SetInt(TutorialName, 1);
    }
}
