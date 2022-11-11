using System.Collections.Generic;
using UnityEngine;

public class FireRamp : MonoBehaviour
{
    [SerializeField] private EnemyModelMountPoints _mountPoints;
    [SerializeField] private ParticleSystem _template;

    private List<ParticleSystem> _particles = new List<ParticleSystem>();

    private void OnEnable()
    {
        foreach (Transform point in _mountPoints)
        {
            ParticleSystem effect = Instantiate(_template, point);
            _particles.Add(effect);
        }
    }

    public void Activate()
    {
        foreach (ParticleSystem effect in _particles)
            if (effect != null)
                effect.Play();
    }

    public void Deactivate()
    {
        foreach (ParticleSystem effect in _particles)
            if (effect != null)
                effect.Stop();
    }
}
