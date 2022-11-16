using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    [SerializeField] private Place _place;
    [SerializeField] private UltimateButton _ultimateButton;

    private bool _isActive;

    public bool IsActive => _isActive;
    public bool IsEmpty => _place.IsEmpty;

    private void OnEnable()
    {
        _place.NeedSetIcon += SetIcon;
        _place.NeedClean += DeactivateIcon;
    }

    private void OnDisable()
    {
        _place.NeedSetIcon -= SetIcon;
        _place.NeedClean -= DeactivateIcon;
    }

    public void ActivatePlace()
    {
        _isActive = true;
        _place.ChangeStatus(_ultimateButton);
    }

    public void DeactivateFrame()
    {
        _place.DeactivateFrame();
    }

    public void ActivateFrame()
    {
        _place.ActivateFrame();
    }

    private void SetIcon(Sprite sprite)
    {
        _ultimateButton.gameObject.SetActive(true);
        _ultimateButton.Inizialize(sprite);
    }

    private void DeactivateIcon()
    {
        _ultimateButton.gameObject.SetActive(false);
    }
}
