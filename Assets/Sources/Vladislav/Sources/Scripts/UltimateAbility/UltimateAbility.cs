using UnityEngine;

public abstract class UltimateAbility : MonoBehaviour
{
    [SerializeField] private Sprite _abilityIcon;

    public Sprite GetIcon()
    {
        return _abilityIcon;
    }

    public virtual void Use(Warrior warrior) { }

    public virtual void Use(CastleTargets castle) { }

    public virtual void Use(Transform spawnPoint, Transform parent) { }
}
