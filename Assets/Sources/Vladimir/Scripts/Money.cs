using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Money", menuName = "GameAssets/Money")]
public class Money : ScriptableObject
{
    private const string MONEY_KEY = "towerDefense/Money";

    [SerializeField] private int _value;

    public int Value => _value;

    public event UnityAction<int, int> ValueChanged;

    public void Init()
    {
        _value = PlayerPrefs.GetInt(MONEY_KEY, 0);
        FireEvent(_value);
    }

    public void Deposit(int amount)
    {
        int old = _value;
        _value += amount;
        FireEvent(old);
    }

    public void Deposit()
    {
        int old = _value;
        _value += 1;
        FireEvent(old);
    }

    public bool TryWithdraw(int amount)
    {
        if (amount <= _value)
        {
            int old = _value;
            _value -= amount;
            FireEvent(old);
            return true;
        }

        return false;
    }

    private void FireEvent(int old)
    {
        PlayerPrefs.SetInt(MONEY_KEY, _value);
        ValueChanged?.Invoke(old, _value);
    }
}
