using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LeaderboardAnimationsHandler : MonoBehaviour
{
    private const string DeactivateTrigger = "Deactivate";
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Deactivate()
    {
        _animator.SetTrigger(DeactivateTrigger);
    }
}
