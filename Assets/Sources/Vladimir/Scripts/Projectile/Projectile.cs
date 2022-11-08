using UnityEngine;

[RequireComponent(typeof(ProjectileMover))]
[RequireComponent(typeof(ProjectileEffects))]
public class Projectile : MonoBehaviour
{
    private ProjectileMover _projectileMover;
    private ProjectileEffects _projectileEffects;
    private float _damage;

    public float Damage => _damage;

    private void Awake()
    {
        _projectileMover = GetComponent<ProjectileMover>();
        _projectileEffects = GetComponent<ProjectileEffects>();
    }

    private void OnEnable()
    {
        _projectileMover.TargetReached += OnTargetReached;
    }

    private void OnDisable()
    {
        _projectileMover.TargetReached -= OnTargetReached;
    }

    public void SetDamageValue(float value) => _damage = value;

    public void Move(Vector3 target)
    { 
        _projectileMover.Move(target);
        _projectileEffects.FlyEffect();
    }

    private void OnTargetReached()
    {
        _projectileEffects.CollisionEffect();
        Debug.Log($"Damage done: {_damage}");
    }
}
