using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _startHealth;
    [SerializeField] private Slider _bar;

    private float _currentHealth;

    public event UnityAction Dead;

    private void Awake()
    {
        _currentHealth = _startHealth;
        _bar.maxValue = _currentHealth;
        _bar.value = _currentHealth;
    }

    public void TakeDamage(float amount)
    { 
        if (_currentHealth <= 0)
            return;

        _currentHealth -= amount;
        _bar.value = _currentHealth;

        if (_currentHealth <= 0)
        { 
            _bar.gameObject.SetActive(false);
            _currentHealth = 0;
            Dead?.Invoke();
        }
    }
}
