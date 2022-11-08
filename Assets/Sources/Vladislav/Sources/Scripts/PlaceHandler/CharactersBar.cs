using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersBar : MonoBehaviour
{
    [SerializeField] private Item _viewPrefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _count;

    private void Awake()
    {
        for (int i = 0; i < _count; i++)
        {
            Instantiate(_viewPrefab, _container);
        }
    }
}
