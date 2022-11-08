using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Place : MonoBehaviour, IDropHandler
{
    [SerializeField] private Transform _spawnPoint;

    private Item _currentItem;

    public bool IsEmpty => _currentItem == null;

    public void OnDrop(PointerEventData eventData)
    {
        if (IsEmpty)
        {
            Transform item = eventData.pointerDrag.transform;
            item.SetParent(transform);
            item.localPosition = Vector3.zero;
            _currentItem = item.GetComponent<Item>();
        }
    }

    public void CreateWarrior(EnemyDetector enemyDetector)
    {
        Warrior tempWarrior = Instantiate(_currentItem.GetWarrior(), _spawnPoint);
        tempWarrior.Inizialize(enemyDetector);
    }
}
