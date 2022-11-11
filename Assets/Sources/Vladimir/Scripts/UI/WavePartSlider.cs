using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WavePartSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _doneIcon;
    [SerializeField] private TMP_Text _waveNumber;

    public void Init(int number)
    { 
        _slider.value = 0;
        _waveNumber.text = number.ToString();
        _doneIcon.SetActive(false);
    }

    public void Done()
    {
        _slider.value = 1;
        _doneIcon.SetActive(true);
    }

    public void ShowIcon()
    {
        _doneIcon.SetActive(true);
    }
}
