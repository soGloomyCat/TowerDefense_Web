using System.Collections;
using UnityEngine;

public class ElfSpeed : UltimateAbility
{
    [SerializeField] private float _duration;
    [SerializeField] private float _tempCooldown;

    private float _leftTime;
    private Coroutine _coroutine;

    public override void Use(Warrior warrior)
    {
        base.Use(warrior);

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(AccelerateAttack(warrior));
    }

    private IEnumerator AccelerateAttack(Warrior warrior)
    {
        _leftTime = 0;
        warrior.AccelerateAttackSpeed(_tempCooldown);

        while (_leftTime <= _duration)
        {
            _leftTime += Time.deltaTime;
            yield return null;
        }

        warrior.ResumeAttackSpeed();
    }
}
