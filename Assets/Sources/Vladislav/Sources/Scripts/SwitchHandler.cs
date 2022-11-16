using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchHandler : MonoBehaviour
{
    [SerializeField] private Button _prepairButton;
    [SerializeField] private PlaceHandler _placeHandler;
    [SerializeField] private Camera _castleCamera;
    [SerializeField] private Canvas _castleCanvas;
    [SerializeField] private Camera _prepairCamera;
    [SerializeField] private Canvas _prepairCanvas;
    [SerializeField] private Camera _battleCamera;
    [SerializeField] private Canvas _battleCanvas;
    [SerializeField] private List<Button> _finalButtons;

    private void OnEnable()
    {
        _prepairButton.onClick.AddListener(SwitchCamera);
        _placeHandler.BattleStarted += OnBattleStarted;

        foreach (var finalButton in _finalButtons)
        {
            finalButton.onClick.AddListener(Return);
        }
    }

    private void OnDisable()
    {
        _prepairButton.onClick.RemoveListener(SwitchCamera);
        _placeHandler.BattleStarted -= OnBattleStarted;

        foreach (var finalButton in _finalButtons)
        {
            finalButton.onClick.RemoveListener(Return);
        }
    }

    private void SwitchCamera()
    {
        _castleCamera.gameObject.SetActive(false);
        _castleCanvas.gameObject.SetActive(false);
        _prepairCamera.gameObject.SetActive(true);
        _prepairCanvas.gameObject.SetActive(true);
    }

    private void Return()
    {
        _battleCamera.gameObject.SetActive(false);
        _battleCanvas.gameObject.SetActive(false);
        _castleCamera.gameObject.SetActive(true);
        _castleCanvas.gameObject.SetActive(true);
    }

    private void OnBattleStarted()
    {
        _prepairCamera.gameObject.SetActive(false);
        _prepairCanvas.gameObject.SetActive(false);
        _battleCamera.gameObject.SetActive(true);
        _battleCanvas.gameObject.SetActive(true);
    }
}
