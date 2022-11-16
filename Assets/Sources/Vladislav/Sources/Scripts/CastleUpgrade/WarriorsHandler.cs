using System.Collections;
using System.Collections.Generic;
using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.Rooms;
using UnityEngine;

public class WarriorsHandler : MonoBehaviour
{
    private const int ArcheryIndex = 0;
    private const int WizardIndex = 1;
    private const int GuardianIndex = 2;

    [SerializeField] private List<GameObject> _warriors;
    [SerializeField] private Castle _castle;

    private List<GameObject> _activeWarriors;

    private void OnEnable()
    {
        _castle.RoomAdded += ExpendWarriors;
    }

    private void Awake()
    {
        _activeWarriors = new List<GameObject>();
    }

    public List<GameObject> GetActiveWarriors()
    {
        return _activeWarriors;
    }

    private void ExpendWarriors(IReadOnlyRoom roomType)
    {
        if (roomType is ArcheryRoom)
            _activeWarriors.Add(_warriors[ArcheryIndex]);
        else if (roomType is LibraryRoom)
            _activeWarriors.Add(_warriors[WizardIndex]);
        else if (roomType is EngineeringRoom)
            _activeWarriors.Add(_warriors[GuardianIndex]);
    }
}
