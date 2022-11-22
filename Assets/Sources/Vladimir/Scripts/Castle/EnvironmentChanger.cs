using UnityEngine;
using System.Collections.Generic;

public class EnvironmentChanger : MonoBehaviour
{
    public const string ENV_INDEX = "towerDefense/EnvIndex";

    private List<GameObject> _models = new List<GameObject>();
    private int _index;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
            _models.Add(child.gameObject);
        }

        _index = PlayerPrefs.GetInt(ENV_INDEX, 0);
        _models[_index].SetActive(true);
    }

    public void ChangeModel()
    { 
        foreach (GameObject model in _models)
            model.SetActive(false);

        if (++_index >= _models.Count)
            _index = 0;

        _models[_index].SetActive(true);
        PlayerPrefs.SetInt(ENV_INDEX, _index);
    }
}
