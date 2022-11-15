using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SliderHandler : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AddReact _icon;

    public float Value => _slider.value;

    public void Setup(float max, float current)
    {
        _slider.maxValue = max;
        _slider.value = current;
    }

    public void Add(float amount)
    {
        float end = _slider.value + amount;
        _slider.DOValue(end, 0.2f);
        _icon.React();
    }
}
