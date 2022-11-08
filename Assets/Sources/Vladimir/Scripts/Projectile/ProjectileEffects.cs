using UnityEngine;

public class ProjectileEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem _flyEffect;
    [SerializeField] private ParticleSystem _collisionEffect;

    public void FlyEffect()
    { 
        if (_flyEffect != null)
            _flyEffect.Play();
    }

    public void CollisionEffect()
    { 
        if (_collisionEffect != null)
            _collisionEffect.Play();
    }
}
