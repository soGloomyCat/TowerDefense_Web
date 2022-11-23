public class WallCreator : UltimateAbility
{
    public override void Use(CastleTargets castle)
    {
        castle.EnableAnotherTarget();
    }
}
