using System;
using TowerDefense.Daniel.Models;

namespace TowerDefense.Daniel.Interfaces
{
    public interface IReadOnlyRoom
    {
        event Action<IReadOnlyRoom> Upgraded;
        int Level { get; }
        RoomMarketInformation Information { get; }

        void FocusIn();
        void FocusOut();
        void Accept(IUnit unit);
    }
}