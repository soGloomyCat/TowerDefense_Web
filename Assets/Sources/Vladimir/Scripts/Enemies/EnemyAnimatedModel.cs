using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class EnemyAnimatedModel : MonoBehaviour
{
    private readonly int _idle = Animator.StringToHash("Idle");
    private readonly int _attack = Animator.StringToHash("Attack");
    private readonly int _death = Animator.StringToHash("Death");
    private Animator _animator;

    public event UnityAction ShootPointReached;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnShootPointReach() => ShootPointReached?.Invoke();

    public void Idle() => _animator.SetTrigger(_idle);
    public void Attack() => _animator.SetTrigger(_attack);
    public void Death() => _animator.SetTrigger(_death);
}
