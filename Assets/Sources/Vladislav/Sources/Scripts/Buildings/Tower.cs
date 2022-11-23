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
        _ultimateButton.gameObject.SetActive(false);
        _place.NeedSetIcon += SetIcon;
    }

    private void OnDisable()
    {
        _place.NeedSetIcon -= SetIcon;
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
}
