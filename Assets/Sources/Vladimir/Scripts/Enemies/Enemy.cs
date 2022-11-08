using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyMover))]
[RequireComponent(typeof(EnemyHealth))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected EnemyAnimatedModel AnimatedModel;
    [SerializeField] protected float AttackPower;

    private EnemyMover _enemyMover;
    private EnemyHealth _enemyHealth;
    private GameObject[] _targets;

    protected bool HasTargets;

    public event UnityAction<Enemy> Dead;

    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyHealth = GetComponent<EnemyHealth>();
        HasTargets = true;
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

    public virtual void SetTargets(GameObject[] targets)
    { }
    
    public void Move(Vector3 destination)
    {
        _enemyMover.Move(destination);
    }

    public void TakeDamage(float amount) => _enemyHealth.TakeDamage(amount);

    private void OnDead()
    {
        Dead?.Invoke(this);
    }

    protected abstract void OnDestinationReach();
}
