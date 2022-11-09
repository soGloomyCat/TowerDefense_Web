using UnityEngine;

public abstract class CloseEnemy : Enemy
{
    [SerializeField] private Projectile _projectile;

    private void OnEnable()
    {
        base.OnEnable();
        AnimatedModel.ShootPointReached += ThrowProjectile;
    }

    private void OnDisable()
    {
        base.OnDisable();
        AnimatedModel.ShootPointReached -= ThrowProjectile;
    }

    protected void ThrowProjectile()
    {
        if (Target == null)
            return;

        Projectile projectile = Instantiate(_projectile, transform.position, Quaternion.identity);
        projectile.SetDamageValue(AttackPower);
        projectile.Move(Target);
    }
}
