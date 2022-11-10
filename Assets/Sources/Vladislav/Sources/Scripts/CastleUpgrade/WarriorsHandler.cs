using System.Collections;
using System.Collections.Generic;
using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.Rooms;
using UnityEngine;

public class WarriorsHandler : MonoBehaviour
{
    [SerializeField] private List<GameObject> _warriors;
    [SerializeField] private Castle _castle;

    private int _currentWarriorsIndex;
    private List<GameObject> _activeWarriors;

    private void OnEnable()
    {
        _castle.RoomAdded += ExpendWarriors;
    }

    private void Awake()
    {
        _currentWarriorsIndex = 0;
        _activeWarriors = new List<GameObject>();
        _activeWarriors.Add(_warriors[_currentWarriorsIndex]);
    }

    public List<GameObject> GetActiveWarriors()
    {
        return _activeWarriors;
    }

    private void ExpendWarriors(IReadOnlyRoom roomType)
    {
        if (roomType is ForgeRoom)
        {
            _currentWarriorsIndex++;
            _activeWarriors.Add(_warriors[_currentWarriorsIndex]);
        }
    }
}
