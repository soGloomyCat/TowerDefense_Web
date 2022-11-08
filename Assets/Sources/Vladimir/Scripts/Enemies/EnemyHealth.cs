using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _startHealth;

    private float _currentHealth;

    public event UnityAction Dead;

    private void Awake()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(float amount)
    { 
        if (_currentHealth <= 0)
            return;

        _currentHealth -= amount;

        if (_currentHealth <= 0)
        { 
            _currentHealth = 0;
            Dead?.Invoke();
        }
    }
}
