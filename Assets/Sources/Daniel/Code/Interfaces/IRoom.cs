namespace TowerDefense.Daniel.Interfaces
{
    public interface IRoom
    {
        void FocusIn();
        void FocusOut();
        void Accept(IUnit unit);
        void Destroy();
    }
}