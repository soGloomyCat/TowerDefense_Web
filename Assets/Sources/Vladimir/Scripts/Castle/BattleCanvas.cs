using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;

public class BattleCanvas : MonoBehaviour
{
    [SerializeField] private RectTransform _castleBar;
    [SerializeField] private RectTransform _castleVerticalBar;
    [SerializeField] private RectTransform _spawnerBar;
    [SerializeField] private RectTransform _spawnerVerticalBar;
    [SerializeField] private ResultPanel _winPanel;
    [SerializeField] private ResultPanel _losePanel;
    [SerializeField] private Money _money;
    [SerializeField] private WavesSlider _wavesSlider;

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
            _wavesSlider.Show();

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
            _wavesSlider.Show();

            if (callback != null)
                callback();
        });
    }

    public void OnPanelButtonClick()
    {
        _openedPanel.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
        {
            _wavesSlider.Hide();
            PanelButtonClicked?.Invoke();
        });
    }

    public void ShowBar()
    {
        _castleBar.DOAnchorPosX(90, 0.5f).SetEase(Ease.OutBack);
        _spawnerBar.DOAnchorPosX(-90, 0.5f).SetEase(Ease.OutBack);
        _castleVerticalBar.DOAnchorPosY(-100, 0.5f).SetEase(Ease.OutBack);
        _spawnerVerticalBar.DOAnchorPosY(130, 0.5f).SetEase(Ease.OutBack);
    }

    public void HideBar()
    {
        _castleBar.DOAnchorPosX(-90, 0.5f).SetEase(Ease.InBack);
        _spawnerBar.DOAnchorPosX(90, 0.5f).SetEase(Ease.InBack);
        _castleVerticalBar.DOAnchorPosY(100, 0.5f).SetEase(Ease.OutBack);
        _spawnerVerticalBar.DOAnchorPosY(-130, 0.5f).SetEase(Ease.OutBack);
    }

    public void ActivateVerticalBars()
    {
        _castleBar.gameObject.SetActive(false);
        _spawnerBar.gameObject.SetActive(false);
        _castleVerticalBar.gameObject.SetActive(true);
        _spawnerVerticalBar.gameObject.SetActive(true);
    }

    public void ActivateHorizontalBars()
    {
        _castleVerticalBar.gameObject.SetActive(false);
        _spawnerVerticalBar.gameObject.SetActive(false);
        _castleBar.gameObject.SetActive(true);
        _spawnerBar.gameObject.SetActive(true);
    }
}
