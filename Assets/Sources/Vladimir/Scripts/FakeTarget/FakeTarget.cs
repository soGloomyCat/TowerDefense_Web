using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(TargetShower))]
public class FakeTarget : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    [SerializeField] private FakeTargetHealthHandler _healthHandler;
    [SerializeField] private Transform _targetPoint;

    private float _currentHealth;
    private BoxCollider _collider;
    private TargetShower _targetShower;

    public Transform TargetPoint => _targetPoint;

    public event UnityAction<FakeTarget> TargetDestroyed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            ApplyDamage(projectile.Damage);
            projectile.Deactivate();

            if (_healthHandler != null)
                _healthHandler.OnHealthChange(_currentHealth);
        }
    }

    public void Inizialize()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        _targetShower = GetComponent<TargetShower>();
        _currentHealth = _startHealth;
    }

    private void ApplyDamage(float damage)
    {
        if (_currentHealth == 0)
            return;

        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            TargetDestroyed?.Invoke(this);
        }
    }

    public void Show()
    {
        _targetShower.Show();
    }

    public void Hide()
    {
        _targetShower.Hide();
    }

    public void ResetTarget()
    {
        gameObject.SetActive(false);
        _targetShower.ResetShower();
    }
}
