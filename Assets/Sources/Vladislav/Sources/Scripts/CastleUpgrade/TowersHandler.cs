using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TowerDefense.Daniel;
using TowerDefense.Daniel.Rooms;
using System;
using TowerDefense.Daniel.Interfaces;
using UnityEngine.UI;

public class TowersHandler : MonoBehaviour
{
    [SerializeField] private List<Tower> _towers;
    [SerializeField] private Castle _castle;
    [SerializeField] private Button _prepairButton;
    [SerializeField] private Button _battleButton;
    [SerializeField] private List<Button> _finalButtons;

    private void OnEnable()
    {
        _castle.RoomAdded += PrepairActivateTowers;
        _castle.RoomUpgraded += PrepairActivateTowers;
        _battleButton.onClick.AddListener(DeactivateEmptyFrame);
        _prepairButton.onClick.AddListener(ActivateTowers);

        foreach (var finalButton in _finalButtons)
        {
            finalButton.onClick.AddListener(DeactivateTowers);
        }
    }

    private void OnDisable()
    {
        _castle.RoomAdded -= PrepairActivateTowers;
        _battleButton.onClick.RemoveListener(DeactivateEmptyFrame);
        _prepairButton.onClick.RemoveListener(ActivateTowers);

        foreach (var finalButton in _finalButtons)
        {
            finalButton.onClick.RemoveListener(DeactivateTowers);
        }
    }

    private void PrepairActivateTowers(IReadOnlyRoom roomType)
    {
        if (roomType is ForgeRoom)
            ChangeTowersStatus();
    }

    private void ChangeTowersStatus()
    {
        int openCount = 2;
        int currentCount = 0;

        foreach (var tower in _towers)
        {
            if (tower.IsActive == false)
            {
                currentCount++;
                tower.ActivatePlace();
            }

            if (currentCount >= openCount)
                break;
        }

        if (_prepairButton.gameObject.activeSelf == false)
            _prepairButton.gameObject.SetActive(true);
    }

    private void DeactivateEmptyFrame()
    {
        foreach (var tower in _towers)
        {
            if (tower.IsEmpty)
                tower.DeactivateFrame();
        }
    }

    private void ActivateTowers()
    {
        foreach (var tower in _towers)
        {
            if (tower.IsActive)
            {
                tower.gameObject.SetActive(true);
                tower.ActivateFrame();
            }
        }
    }

    private void DeactivateTowers()
    {
        foreach (var tower in _towers)
        {
            if (tower.IsActive)
            {
                tower.gameObject.SetActive(false);
            }
        }
    }
}
