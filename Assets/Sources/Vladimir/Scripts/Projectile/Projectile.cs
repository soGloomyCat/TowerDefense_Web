using UnityEngine;

[RequireComponent(typeof(ProjectileMover))]
[RequireComponent(typeof(ProjectileEffects))]
[RequireComponent(typeof(SphereCollider))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject _model;

    private ProjectileMover _projectileMover;
    private ProjectileEffects _projectileEffects;
    private SphereCollider _sphereCollider;
    private float _damage;

    public float Damage => _damage;

    private void Awake()
    {
        _projectileMover = GetComponent<ProjectileMover>();
        _projectileEffects = GetComponent<ProjectileEffects>();
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.isTrigger = true;
    }

    private void OnEnable()
    {
        //_projectileMover.TargetReached += OnTargetReached;
    }

    private void OnDisable()
    {
        //_projectileMover.TargetReached -= OnTargetReached;
    }

    public void SetDamageValue(float value) => _damage = value;

    public void Move(Vector3 target)
    { 
        _projectileMover.Move(target);
        _projectileEffects.FlyEffect();
    }

    public void Deactivate()
    {
        if (_model != null)
            _model.SetActive(false);

        _projectileMover.Deactivate();
        _sphereCollider.enabled = false;
        _projectileEffects.CollisionEffect();
        Destroy(gameObject, 1f);
    }

    /*
    private void OnTargetReached()
    {
        _projectileEffects.CollisionEffect();
    }
    */
}
