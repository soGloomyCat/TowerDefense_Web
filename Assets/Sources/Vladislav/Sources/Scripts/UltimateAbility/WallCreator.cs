using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallCreator : UltimateAbility
{
    private void Create()
    {
        CastleTargets castle = GameObject.Find("Castle").GetComponent<CastleTargets>();
        castle.EnableAnotherTarget();
    }

    protected override void ActivateUltimate()
    {
        Warrior.ActivateUltimate(false);
        Create();
    }
}
