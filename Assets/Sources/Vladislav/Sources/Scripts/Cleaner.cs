using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cleaner : MonoBehaviour
{
    [SerializeField] private List<Place> _places;
    [SerializeField] private Transform _container;
    [SerializeField] private List<Button> _finalButton;
    [SerializeField] private Transform _warriorsView;

    private void OnEnable()
    {
        foreach (var button in _finalButton)
        {
            button.onClick.AddListener(Clean);
        }
    }

    private void OnDisable()
    {
        foreach (var button in _finalButton)
        {
            button.onClick.RemoveListener(Clean);
        }
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
    }
}
