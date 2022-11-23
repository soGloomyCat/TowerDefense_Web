public class WallCreator : UltimateAbility
{
    public override void Use(CastleTargets castle)
    {
        base.Use(castle);

        castle.EnableAnotherTarget();
    }
}
