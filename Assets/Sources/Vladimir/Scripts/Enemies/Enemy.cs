using TowerDefense.Daniel;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(HazardHandlersList))]
[RequireComponent(typeof(Collider))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyAnimatedModel AnimatedModel;
    [SerializeField] protected float AttackPower;

    private EnemyMover _enemyMover;
    private EnemyHealth _enemyHealth;
    private HazardHandlersList _hazardHandlersList;
    private Collider _collider;

    //protected bool HasTargets;
    protected Vector3 Target;

    public bool IsFarEnemy { get; private set; }

    public event UnityAction<Enemy> Dead;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _hazardHandlersList = GetComponent<HazardHandlersList>();
        _collider = GetComponent<Collider>();
        //HasTargets = true;
    }

    protected void OnEnable()
    {
        _enemyMover.DestinationReached += OnDestinationReach;
        _enemyHealth.Dead += OnDead;
    }

    protected void OnDisable()
    {
        _enemyMover.DestinationReached -= OnDestinationReach;
        _enemyHealth.Dead -= OnDead;
    }

    private void OnTriggerEnter(Collider other)
    {
        _hazardHandlersList.Handle(other);
    }

    public void SetTarget(Vector3 target) => Target = target;

    public void Move(Vector3 destination)
    {
        _enemyMover.Move(destination);
    }

    public void TakeDamage(float amount) => _enemyHealth.TakeDamage(amount);

    public void StopAttack() => AnimatedModel.Idle();

    private void OnDead()
    {
        Dead?.Invoke(this);
        _collider.enabled = false;
        AnimatedModel.Death();
        Destroy(gameObject, 2f);
        _enemyMover.StopMove();

        if (TryGetComponent(out EnemyFlyer enemyFlyer))
        {
            enemyFlyer.Fall();
        }
    }

    protected abstract void OnDestinationReach();
}
