using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Place _place;

    public bool IsActive => gameObject.activeSelf;
    public bool IsEmpty => _place.IsEmpty;

    public void ActivatePlace()
    {
        _place.ChangeStatus();
    }

    public void DeactivateFrame()
    {
        _place.DeactivateFrame();
    }

    public void ActivateFrame()
    {
        _place.ActivateFrame();
    }
}
