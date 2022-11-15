using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class WavesSlider : MonoBehaviour
{
    [SerializeField] private WavePartSlider _wavePartSliderTemplate;
    [SerializeField] private Transform _last;

    private List<WavePartSlider> _parts = new List<WavePartSlider>();
    private int _index;
    private Vector3 _origin;

    public void Generate(int wavesCount)
    {
        for (int i = 0; i < wavesCount; i++)
        {
            WavePartSlider part = Instantiate(_wavePartSliderTemplate, transform);
            part.Init(i);
            _parts.Add(part);
        }

        Instantiate(_last, transform);
        _index = 0;
        _origin = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    public void Done()
    {
        _parts[_index].Done();

        int nextIndex = _index + 1;

        if (nextIndex < _parts.Count)
            _parts[nextIndex].ShowIcon();

        _index++;
    }

    public void Show()
    { 
        transform.DOScale(_origin, 0.3f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
    }
}
