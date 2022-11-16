using System;
using UnityEngine;
using UnityEngine.UI;

public class UltimateButton : MonoBehaviour
{
    [SerializeField] private Button _ultimateButton;
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private Reloader _reloader;

    public event Action ButtonClicked;

    private void OnDisable()
    {
        _ultimateButton.onClick.RemoveListener(OnButtonClicked);
        _reloader.TimerCompleted -= OnTimerComleted;
    }

    public void Inizialize(Sprite icon)
    {
        _abilityIcon.sprite = icon;
        _ultimateButton.onClick.AddListener(OnButtonClicked);
        _reloader.Inizialize(icon);
        _reloader.TimerCompleted += OnTimerComleted;
    }

    private void OnButtonClicked()
    {
        ButtonClicked?.Invoke();
        _ultimateButton.interactable = false;
        _reloader.Activate();
    }

    private void OnTimerComleted()
    {
        _ultimateButton.interactable = true;
    }
}
