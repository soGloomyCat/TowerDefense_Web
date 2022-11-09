using System;

namespace TowerDefense.Daniel.Interfaces
{
    public interface IReadOnlyRoom
    {
        event Action<IReadOnlyRoom> Upgraded;
        int Level { get; }

        void FocusIn();
        void FocusOut();
        void Accept(IUnit unit);
    }
}