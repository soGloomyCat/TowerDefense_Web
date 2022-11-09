using UnityEngine;

public abstract class FarEnemy : Enemy
{
    [SerializeField] private Projectile _projectile;
    [SerializeField] private Transform _projectileStartPoint;

    //private Vector3 _target;

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

    //public void SetTarget(Vector3 target) => _target = target;

    protected void ThrowProjectile()
    { 
        if (Target == null)
            return;

        Projectile projectile = Instantiate(_projectile, _projectileStartPoint.position, Quaternion.identity);
        projectile.SetDamageValue(AttackPower);
        projectile.Move(Target);
    }
}
