using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private const string AttackTrigger = "RangeAttack";

    [SerializeField] private Animator _animator;

    public event Action NeedShoot;

    public void ActivateAttackAnimation() => _animator.SetTrigger(AttackTrigger);

    public void OnShootTriggerActivate() => NeedShoot?.Invoke();
}
