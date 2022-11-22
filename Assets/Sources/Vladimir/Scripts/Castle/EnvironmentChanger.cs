using UnityEngine;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour
{
    [SerializeField] private LevelsHandler _levelsHandler;

    public const string ENV_INDEX = "towerDefense/EnvIndex";

    private List<GameObject> _models = new List<GameObject>();
    private int _index;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _models.Add(transform.GetChild(i).gameObject);
        }

        _index = PlayerPrefs.GetInt(ENV_INDEX, 0);
    }

    private void OnEnable()
    {
        _levelsHandler.LevelChanged += OnLevelChange;
    }

    private void OnDisable()
    {
        _levelsHandler.LevelChanged -= OnLevelChange;
    }

    private void OnLevelChange(int levelNumber)
    { 
        EnableModel();
    }

    private void EnableModel()
    { 
        foreach (GameObject model in _models)
            model.SetActive(false);

        _models[_index].SetActive(true);

        if (++_index >= _models.Count)
            _index = 0;

        PlayerPrefs.SetInt(ENV_INDEX, _index);
    }
}
