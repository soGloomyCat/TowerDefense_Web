using UnityEngine;
using TMPro;
using System.Collections;

public class MoneyInfoPanel : MonoBehaviour
{
    [SerializeField] private AddReact _icon;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private Money _money;

    private int _speed = 3;

    private void OnEnable()
    {
        _money.ValueChanged += OnMoneyChange;
    }

    private void OnDisable()
    {
        _money.ValueChanged -= OnMoneyChange;
    }

    private void OnMoneyChange(int oldValue, int newValue)
    {
        if (oldValue < newValue)
        {
            StartCoroutine(IncreaseMoney(oldValue, newValue));
        }
        else if (oldValue > newValue)
        {
            StartCoroutine(DecreaseMoney(oldValue, newValue));
        }
        else
        {
            _moneyText.text = newValue.ToString();
        }
    }

    private IEnumerator IncreaseMoney(int oldValue, int newValue)
    { 
        int bufer = oldValue;

        while (bufer < newValue)
        {
            _icon.React();
            bufer += _speed;
            _moneyText.text = bufer.ToString();
            yield return null;
        }

        _moneyText.text = newValue.ToString();
    }

    private IEnumerator DecreaseMoney(int oldValue, int newValue)
    {
        int bufer = oldValue;

        while (bufer > newValue)
        {
            _icon.React();
            bufer -= _speed;
            _moneyText.text = bufer.ToString();
            yield return null;
        }

        _moneyText.text = newValue.ToString();
    }
}
