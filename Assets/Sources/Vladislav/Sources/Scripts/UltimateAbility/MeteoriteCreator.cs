using UnityEngine;

public class MeteoriteCreator : UltimateAbility
{
    [SerializeField] private Meteorite _meteorite;

    public override void Use(Transform spawnPoint, Transform parent)
    {
        Meteorite tempMeteorite = Instantiate(_meteorite, parent);
        tempMeteorite.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
        tempMeteorite.transform.rotation = spawnPoint.rotation;
        tempMeteorite.Fall(parent);
    }
}
