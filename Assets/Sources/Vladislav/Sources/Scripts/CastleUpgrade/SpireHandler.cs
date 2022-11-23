using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.Rooms;
using UnityEngine;
using UnityEngine.UI;

public class SpireHandler : MonoBehaviour
{
    [SerializeField] private Castle _castle;
    [SerializeField] private Button _castlePowerButton;

    private void OnEnable()
    {
        _castle.RoomAdded += PrepairActivateTowers;
    }

    private void OnDisable()
    {
        _castle.RoomAdded -= PrepairActivateTowers;
    }

    private void PrepairActivateTowers(IReadOnlyRoom roomType)
    {
        if (roomType is TowerRoom)
            _castlePowerButton.gameObject.SetActive(true);
    }
}
