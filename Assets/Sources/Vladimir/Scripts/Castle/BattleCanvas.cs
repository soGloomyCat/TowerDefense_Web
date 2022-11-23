using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using System;
using UnityEngine.UI;
using TowerDefense.Daniel;

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
    [SerializeField] private LevelsHandler _levelsHandler;
    [SerializeField] private CanvasScaler _canvasScaler;
    [SerializeField] private AdHandler _adHandler;

    private Transform _openedPanel;
    private int _reward;

    public event UnityAction PanelButtonClicked;

    private void OnEnable()
    {
        _adHandler.AdFinished += OnAdFinished;
        _adHandler.RewardAdFinished += OnRewardedAdFinished;
    }

    private void Awake()
    {
        _winPanel.transform.localScale = Vector3.zero;
        _losePanel.transform.localScale = Vector3.zero;
    }

    public void ShowWinPanel(int money, Action callback = null)
    {
        _openedPanel = _winPanel.transform;
        _reward = money;
        _winPanel.SetMoney(money);
        _winPanel.SetLevel(_levelsHandler.CurrentLevelNumber);

        _winPanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
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
        _losePanel.SetLevel(_levelsHandler.CurrentLevelNumber);

        _losePanel.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
        {
            _wavesSlider.Show();

            if (callback != null)
                callback();
        });
    }

    public void OnNextButtonClicked()
    {
        AudioController.TryPlayUIClick();

        _adHandler.ShowInterstitialAd();
    }

    public void OnAdButtonClicked()
    {
        AudioController.TryPlayUIClick();

        _adHandler.ShowRewardedAd();
    }

    public void ShowBar()
    {
        _castleBar.DOAnchorPosX(90, 0.5f).SetEase(Ease.OutBack);
        _spawnerBar.DOAnchorPosX(-90, 0.5f).SetEase(Ease.OutBack);
        _castleVerticalBar.DOAnchorPosY(-130, 0.5f).SetEase(Ease.OutBack);
        _spawnerVerticalBar.DOAnchorPosY(130, 0.5f).SetEase(Ease.OutBack);
    }

    public void HideBar()
    {
        _castleBar.DOAnchorPosX(-90, 0.5f).SetEase(Ease.InBack);
        _spawnerBar.DOAnchorPosX(90, 0.5f).SetEase(Ease.InBack);
        _castleVerticalBar.DOAnchorPosY(130, 0.5f).SetEase(Ease.OutBack);
        _spawnerVerticalBar.DOAnchorPosY(-130, 0.5f).SetEase(Ease.OutBack);
    }

    public void ActivateVerticalBars()
    {
        _canvasScaler.matchWidthOrHeight = 0.2f;
        _castleBar.gameObject.SetActive(false);
        _spawnerBar.gameObject.SetActive(false);
        _castleVerticalBar.gameObject.SetActive(true);
        _spawnerVerticalBar.gameObject.SetActive(true);
    }

    public void ActivateHorizontalBars()
    {
        _canvasScaler.matchWidthOrHeight = 0.5f;
        _castleVerticalBar.gameObject.SetActive(false);
        _spawnerVerticalBar.gameObject.SetActive(false);
        _castleBar.gameObject.SetActive(true);
        _spawnerBar.gameObject.SetActive(true);
    }

    private void OnAdFinished()
    {
        _money.Deposit(_reward);
        OnPanelButtonClick();
    }

    private void OnRewardedAdFinished()
    {
        _money.Deposit(_reward * 2);
        OnPanelButtonClick();
    }

    private void OnPanelButtonClick()
    {
        _openedPanel.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() =>
        {
            _wavesSlider.Hide();
            PanelButtonClicked?.Invoke();
        });
    }
}
