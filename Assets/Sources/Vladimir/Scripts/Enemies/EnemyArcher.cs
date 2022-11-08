using UnityEngine;

public class EnemyArcher : FarEnemy
{
    private void Attack()
    {
        AnimatedModel.Attack();
    }

    protected override void OnDestinationReach()
    {
        if (HasTargets)
        {
            Attack();
        }
        else
        {
            AnimatedModel.Idle();
        }
    }
}
