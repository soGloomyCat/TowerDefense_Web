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

    private void OnEnable()
    {
        _castle.RoomAdded += PrepairActivateTowers;
        _battleButton.onClick.AddListener(DeactivateEmptyFrame);
        _prepairButton.onClick.AddListener(ActivateFrames);
    }

    private void OnDisable()
    {
        _castle.RoomAdded -= PrepairActivateTowers;
        _battleButton.onClick.RemoveListener(DeactivateEmptyFrame);
        _prepairButton.onClick.RemoveListener(ActivateFrames);
    }

    private void PrepairActivateTowers(IReadOnlyRoom roomType)
    {
        if (roomType is StrategyRoom)
            ActivateTowers();
    }

    private void ActivateTowers()
    {
        int openCount = 2;
        int currentCount = 0;

        foreach (var tower in _towers)
        {
            if (tower.IsActive == false)
            {
                tower.gameObject.SetActive(true);
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

    private void ActivateFrames()
    {
        foreach (var tower in _towers)
        {
            tower.ActivateFrame();
        }
    }
}
