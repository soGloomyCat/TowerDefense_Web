using UnityEngine;
using DG.Tweening;

public class AddReact : MonoBehaviour
{
    private bool _isAvailable;

    private void Awake()
    {
        _isAvailable = true;
    }

    public void React()
    {
        if (!_isAvailable)
            return;

        _isAvailable = false;

        transform.DOPunchScale(Vector3.one * 0.15f, 0.3f).OnComplete(() =>
        {
            _isAvailable = true;
        });
    }
}
