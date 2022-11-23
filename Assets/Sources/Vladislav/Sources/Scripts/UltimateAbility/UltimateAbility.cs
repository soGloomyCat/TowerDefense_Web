using TowerDefense.Daniel;
using UnityEngine;

public abstract class UltimateAbility : MonoBehaviour
{
    [SerializeField] private Sprite _abilityIcon;
    [SerializeField] private AudioClip _useAudio = null;

    public Sprite GetIcon()
    {
        return _abilityIcon;
    }

    public virtual void Use(Warrior warrior) { MusicPlayer.TryPlaySFX(_useAudio); }

    public virtual void Use(CastleTargets castle) { MusicPlayer.TryPlaySFX(_useAudio); }

    public virtual void Use(Transform spawnPoint, Transform parent) { MusicPlayer.TryPlaySFX(_useAudio); }
}
