using DG.Tweening;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WavesSlider : MonoBehaviour
{
    [SerializeField] private WavePartSlider _wavePartSliderTemplate;
    [SerializeField] private WavePartSlider _last;

    private List<WavePartSlider> _parts = new List<WavePartSlider>();
    //private int _index;
    private Vector3 _origin;

    public void Generate(int wavesCount)
    {
        Clear();

        for (int i = 0; i < wavesCount; i++)
        {
            WavePartSlider part = Instantiate(_wavePartSliderTemplate, transform);
            part.Init(i);
            _parts.Add(part);
        }

        WavePartSlider last = Instantiate(_last, transform);
        last.Init(wavesCount);
        _parts.Add(last);

        //_index = 0;

        if (_origin == Vector3.zero)
            _origin = transform.localScale;

        transform.localScale = Vector3.zero;
    }

    public void Done(int waveNumber)
    {
        for (int i = 0; i < waveNumber; i++)
        {
            _parts[i].Done();
            _parts[i + 1].ShowIcon();
        }

        /*
        _parts[_index].Done();

        int nextIndex = _index + 1;

        if (nextIndex < _parts.Count)
            _parts[nextIndex].ShowIcon();

        _index++;
        */
    }

    public void Show()
    {
        transform.DOScale(_origin, 0.3f).SetEase(Ease.OutBack);
    }

    public void Hide()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack);
    }

    private void Clear()
    {
        foreach (WavePartSlider part in _parts)
        {
            Destroy(part.gameObject);
        }

        _parts.Clear();
    }
}
