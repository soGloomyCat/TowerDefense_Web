using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class BattleCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform _castleBar;
    [SerializeField] private ResultPanel _winPanel;
    [SerializeField] private ResultPanel _losePanel;
    [SerializeField] private Money _money;

    private Transform _openedPanel;
    private int _reward;

    public event UnityAction PanelButtonClicked;

    private void Awake()
    {
        _winPanel.transform.localScale = Vector3.zero;
        _losePanel.transform.localScale = Vector3.zero;
    }

    public void ShowWinPanel(int money, Action callback = null)
    {
        _winPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        { 
            _openedPanel = _winPanel.transform;
            _reward = money;
            _winPanel.SetMoney(money);
            _money.Deposit(_reward);

            if (callback != null)
                callback();
        });
    }

    public void ShowLosePanel(int money, Action callback = null)
    {
        _openedPanel = _losePanel.transform;
        _reward = money;
        _losePanel.SetMoney(money);
        _money.Deposit(_reward);

        _losePanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            if (callback != null)
                callback();
        });
    }

    public void OnPanelButtonClick()
    {
        _openedPanel.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
        {
            PanelButtonClicked?.Invoke();
        });
    }

    public void ShowBar()
    {
        _castleBar.DOAnchorPosY(-120, 0.5f).SetEase(Ease.OutBack);
    }

    public void HideBar()
    {
        _castleBar.DOAnchorPosY(120, 0.5f).SetEase(Ease.OutBack);
    }
}
