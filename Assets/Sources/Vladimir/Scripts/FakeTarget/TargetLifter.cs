using DG.Tweening;
using UnityEngine;

public class TargetLifter : TargetShower
{
    [SerializeField] private Transform _model;

    private float _originY;

    private void Awake()
    {
        _originY = _model.localPosition.y;
    }

    public override void Hide()
    {
        _model.DOLocalMoveY(_originY, 0.3f).SetEase(Ease.InBack).OnComplete(() =>
        {
            gameObject.SetActive(false);
        });
    }

    public override void Show()
    {
        gameObject.SetActive(true);
        _model.DOLocalMoveY(0, 0.3f).SetEase(Ease.OutBack);
    }

    public override void ResetShower()
    {
        _model.DOLocalMoveY(_originY, 0.3f);
    }
}
