using UnityEngine;

public class EnemyWarrior : CloseEnemy
{
    private void Attack()
    {
        AnimatedModel.Attack();
    }

    protected override void OnDestinationReach()
    {
        Attack();
    }
}
