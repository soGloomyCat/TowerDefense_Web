using UnityEngine;
using TMPro;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private TMP_Text _levelText;

    public void SetMoney(int amount)
    { 
        _moneyText.text = amount.ToString();
    }

    public void SetLevel(int levelNumber)
    { 
        _levelText.text = levelNumber.ToString();
    }
}
