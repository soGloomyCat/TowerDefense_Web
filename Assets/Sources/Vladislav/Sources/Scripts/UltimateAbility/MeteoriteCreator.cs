using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteCreator : UltimateAbility
{
    [SerializeField] private Meteorite _meteorite;

    private void Create()
    {
        Transform spawnPoint = GameObject.Find("MeteoriteSpawnPoint").transform;
        Meteorite tempMeteorite = Instantiate(_meteorite);
        tempMeteorite.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
        tempMeteorite.transform.rotation = spawnPoint.rotation;
        tempMeteorite.Fall();
    }

    protected override void ActivateUltimate()
    {
        Warrior.ActivateUltimate(false);
        Create();
    }
}
