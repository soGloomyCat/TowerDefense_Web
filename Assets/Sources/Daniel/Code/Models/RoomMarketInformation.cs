using AYellowpaper;
using TowerDefense.Daniel.Interfaces;
using UnityEngine;

namespace TowerDefense.Daniel.Models
{
    [CreateAssetMenu(fileName = "New RoomMarketInformation", menuName = "Tower Defense/Daniel/Room Market Information", order = 50)]
    public class RoomMarketInformation : ScriptableObjectWithID
    {
        [field: SerializeField] public InterfaceReference<IRoom> Prefab { get; private set; } = null;
        [field: SerializeField] public string Title { get; private set; } = "";
        [field: SerializeField] public Sprite Preview { get; private set; } = null;
        [field: SerializeField] public string Description { get; private set; } = "";
        [field: SerializeField] public int Price { get; private set; } = 0;
    }
}
