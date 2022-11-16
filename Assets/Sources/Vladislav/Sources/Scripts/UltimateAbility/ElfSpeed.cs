using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElfSpeed : UltimateAbility
{
    protected override void ActivateUltimate()
    {
        Warrior.ActivateUltimate(true);
    }
}
