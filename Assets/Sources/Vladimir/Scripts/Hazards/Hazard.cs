using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Hazard : MonoBehaviour
{
    [SerializeField] private float _damagePerSecond;
    [SerializeField] private float _duration;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    public float DamagePerSecond => _damagePerSecond;
    public float Duration => _duration;
}
