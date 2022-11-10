using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private Warrior _prefab;

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private RectTransform _position;

    private void Awake()
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponentInParent<CanvasGroup>();
        _position = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Transform slotTransform = _position.parent;
        slotTransform.SetAsLastSibling();
        _canvasGroup.blocksRaycasts = false;
    }

    public Warrior GetWarrior()
    {
        return _prefab;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _position.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;

        //if (transform.parent.TryGetComponent(out Place place))
        //    _canvasGroup.blocksRaycasts = false;
        //else
        _canvasGroup.blocksRaycasts = true;
    }
}
