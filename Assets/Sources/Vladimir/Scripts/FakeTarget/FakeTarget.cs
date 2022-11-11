using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(BoxCollider))]
public class FakeTarget : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    [SerializeField] private FakeTargetHealthHandler _healthHandler;

    private float _currentHealth;
    private BoxCollider _collider;

    public event UnityAction<FakeTarget> TargetDestroyed;

    private void OnEnable()
    {
        _collider = GetComponent<BoxCollider>();
        _collider.isTrigger = true;
        transform.localScale = Vector3.zero;
        _currentHealth = _startHealth;
    }

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
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public void ResetTarget()
    {
        gameObject.SetActive(false);
    }
}
