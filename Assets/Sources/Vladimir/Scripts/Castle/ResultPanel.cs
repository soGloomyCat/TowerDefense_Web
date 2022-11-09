using UnityEngine;
using TMPro;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;

    public void SetMoney(int amount)
    { 
        _moneyText.text = amount.ToString();
    }
}
