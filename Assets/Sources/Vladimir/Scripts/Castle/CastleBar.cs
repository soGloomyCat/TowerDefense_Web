using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CastleBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AddReact _icon;

    public void Init(float max)
    { 
        _slider.maxValue = max;
        _slider.value = max;
    }

    public void OnTakeDamage(float currentValue)
    {
        //_slider.value = currentValue;
        _slider.DOValue(currentValue, 0.2f);
        _icon.React();
    }
}
