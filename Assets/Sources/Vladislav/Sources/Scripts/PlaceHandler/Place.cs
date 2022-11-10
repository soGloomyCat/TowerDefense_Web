using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Place : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject _frame;
    [SerializeField] private Transform _spawnPoint;

    private Item _currentItem;
    private Transform _currentIcon;
    private Warrior _currentWarrior;
    private bool _isActive = false;

    public event Action Clicked;

    public bool IsEmpty => _currentItem == null;
    public bool IsActive => _isActive;

    public void OnDrop(PointerEventData eventData)
    {
        if (IsEmpty)
        {
            Transform item = eventData.pointerDrag.transform;
            _currentIcon = Instantiate(item, transform);
            _currentIcon.localPosition = Vector3.zero;
            _currentIcon.localScale = new Vector3(0.05f, 0.04f, 0.015f);
            _currentIcon.localRotation = Quaternion.Euler(0, 0, 180);
            _currentItem = _currentIcon.GetComponent<Item>();
            DeactivateFrame();
            Clicked?.Invoke();
        }
    }

    public void CreateWarrior(EnemyDetector enemyDetector)
    {
        _currentWarrior = Instantiate(_currentItem.GetWarrior(), _spawnPoint);
        _currentWarrior.transform.localRotation = Quaternion.Euler(90, 0, 0);
        _currentWarrior.Inizialize(enemyDetector);
        Destroy(_currentIcon.gameObject);
    }

    public void ChangeStatus()
    {
        _isActive = true;
    }

    public void DeactivateFrame()
    {
        _frame.SetActive(false);
    }

    public void ActivateFrame()
    {
        _frame.SetActive(true);
    }

    public void DestroyWarrior()
    {
        if (_currentWarrior != null)
        {
            Destroy(_currentWarrior.gameObject);
            _currentWarrior = null;
        }
    }
}
