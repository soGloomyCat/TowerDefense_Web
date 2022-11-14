using DG.Tweening;
using UnityEngine;

public class TargetScaler : TargetShower
{
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
    }

    public override void Hide()
    {
        transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
}
