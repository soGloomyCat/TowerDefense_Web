using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Hazard : MonoBehaviour
{
    [SerializeField] private float _damagePerSecond;
    [SerializeField] private float _duration;
    [SerializeField] private float _speed;

    private Collider _collider;

    public float DamagePerSecond => _damagePerSecond;
    public float Duration => _duration;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }
}
