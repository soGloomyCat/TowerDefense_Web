using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class UltimateAbility : MonoBehaviour
{
    [SerializeField] private Sprite _abilityIcon;
    [SerializeField] protected Warrior Warrior;

    private void OnEnable()
    {
        Warrior.NeedActivateAbility += ActivateUltimate;
        Warrior.NeedIcon += GetIcon;
    }

    private void OnDisable()
    {
        Warrior.NeedActivateAbility -= ActivateUltimate;
        Warrior.NeedIcon -= GetIcon;
    }

    public Sprite GetIcon()
    {
        return _abilityIcon;
    }

    protected abstract void ActivateUltimate();
}
