using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.Rooms;
using UnityEngine;

public class WarriorsHandler : MonoBehaviour
{
    [SerializeField] private Castle _castle;
    [SerializeField] private Warrior _archer;
    [SerializeField] private Warrior _wizard;
    [SerializeField] private Warrior _guardian;

    private void OnEnable()
    {
        _castle.RoomUpgraded += UpgradeWarrior;
    }

    private void OnDisable()
    {
        _castle.RoomUpgraded -= UpgradeWarrior;
    }

    private void UpgradeWarrior(IReadOnlyRoom roomType)
    {
        if (roomType is ArcheryRoom)
            _archer.UpgradeParameters();
        else if (roomType is LibraryRoom)
            _wizard.UpgradeParameters();
        else if (roomType is EngineeringRoom)
            _guardian.UpgradeParameters();
    }
}
