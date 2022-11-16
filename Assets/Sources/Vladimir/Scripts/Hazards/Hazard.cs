using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Hazard : MonoBehaviour
{
    [SerializeField] private float _damagePerSecond;
    [SerializeField] private float _duration;
    [SerializeField] private float _speed;

    public float DamagePerSecond => _damagePerSecond;
    public float Duration => _duration;
}
