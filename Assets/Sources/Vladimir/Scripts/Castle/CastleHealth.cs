using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class CastleHealth : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    [SerializeField] private CastleBar _castleBar;
    [SerializeField] private CastleBar _castleVerticalBar;
    [SerializeField] private Transform _target;
    [SerializeField] private SpireAbility _ability;

    private float _currentHealth;
    private BoxCollider _collider;
    private bool _isInvincible;

    public Transform Target => _target;

    public event UnityAction CastleDestroyed;

    private void OnEnable()
    {
        _ability.SpellActivated += ActivateInvincible;
        _ability.SpellOvered += DeactivateInvincible;
    }

    private void Awake()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        _isInvincible = false;
        ResetCastle();
    }

    private void OnDisable()
    {
        _ability.SpellActivated -= ActivateInvincible;
        _ability.SpellOvered -= DeactivateInvincible;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            ApplyDamage(projectile.Damage);
            projectile.Deactivate();
        }
    }

    public void ResetCastle()
    {
        _currentHealth = _startHealth;
        _castleBar.Init(_startHealth);
        _castleVerticalBar.Init(_startHealth);
    }

    private void ApplyDamage(float damage)
    {
        if (_currentHealth == 0 || _isInvincible)
            return;

        _currentHealth -= damage;
        _castleBar.OnTakeDamage(_currentHealth);
        _castleVerticalBar.OnTakeDamage(_currentHealth);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            CastleDestroyed?.Invoke();
        }
    }

    private void ActivateInvincible()
    {
        _isInvincible = true;
    }

    private void DeactivateInvincible()
    {
        _isInvincible = false;
    }
}
