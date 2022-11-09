using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CastleHealth : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    [SerializeField] private CastleBar _castleBar;
    [SerializeField] private Transform _target;

    private float _currentHealth;
    private BoxCollider _collider;

    public Transform Target => _target;

    public event UnityAction CastleDestroyed;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        ResetCastle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            ApplyDamage(projectile.Damage);
            projectile.Deactivate();
        }
    }

    private void ApplyDamage(float damage)
    {
        if (_currentHealth == 0)
            return;

        _currentHealth -= damage;
        _castleBar.OnTakeDamage(_currentHealth);

        if (_currentHealth <= 0)
        { 
            _currentHealth = 0;
            CastleDestroyed?.Invoke();
        }
    }

    public void ResetCastle()
    {
        _currentHealth = _startHealth;
        _castleBar.Init(_startHealth);
    }
}
