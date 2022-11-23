using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Place : MonoBehaviour, IDropHandler
{
    [SerializeField] private GameObject _frame;
    [SerializeField] private Transform _spawnPoint;

    private UltimateButton _ultimateButton;
    private Item _currentItem;
    private Transform _currentIcon;
    private Warrior _currentWarrior;
    private bool _isActive = false;

    public event Action Clicked;
    public event Action<Sprite> NeedSetIcon;

    public bool IsEmpty => _currentItem == null;
    public bool IsActive => _isActive;

    public void OnDrop(PointerEventData eventData)
    {
        if (IsEmpty)
        {
            Transform item = eventData.pointerDrag.transform;
            _currentIcon = Instantiate(item, transform);
            _currentIcon.localPosition = Vector3.zero;
            _currentIcon.localScale = new Vector3(0.025f, 0.025f, 0.025f);
            _currentIcon.localRotation = Quaternion.Euler(0, 0, 180);
            _currentItem = _currentIcon.GetComponent<Item>();
            DeactivateFrame();
            Clicked?.Invoke();
        }
    }

    public Warrior CreateWarrior(Holder holder)
    {
        _currentWarrior = Instantiate(_currentItem.GetWarrior(), _spawnPoint);
        _currentWarrior.Inizialize(_ultimateButton, holder);
        Destroy(_currentIcon.gameObject);
        NeedSetIcon?.Invoke(_currentWarrior.GetUltimateIcon());
        return _currentWarrior;
    }

    public void ChangeStatus(UltimateButton ultimateButton)
    {
        _ultimateButton = ultimateButton;
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
