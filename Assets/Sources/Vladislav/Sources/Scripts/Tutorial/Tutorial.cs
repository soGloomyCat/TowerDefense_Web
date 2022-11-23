using System;
using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Lean.Localization;

public class Tutorial : MonoBehaviour
{
    private const int FirstBlockFramesIndex = 0;
    private const int SecondBlockFramesIndex = 1;
    private const int ThirdBlockFramesIndex = 2;
    private const int ForthBlockFramesIndex = 3;

    private const string StartMessage = "StartMessage";
    private const string FirstMessage = "FirstMessage";
    private const string SecondMessage = "SecondMessage";
    private const string ThirdMessage = "ThirdMessage";
    private const string FourthMessage = "FourthMessage";
    private const string FifthMessage = "FifthMessage";
    private const string SixthMessage = "SixthMessage";
    private const string SeventhMessage = "SeventhMessage";
    private const string EighthMessage = "EighthMessage";

    [SerializeField] private TMP_Text _infoText;
    [SerializeField] private Button _marketButton;
    [SerializeField] private Castle _castle;
    [SerializeField] private Button _prepairButton;
    [SerializeField] private PlaceHandler _placeHandler;
    [SerializeField] private Button _battleButton;
    [SerializeField] private BlockFrameHandler _blockFrame;
    [SerializeField] private Market _market;

    private int _currentTextIndex;
    private bool _isFirstPurchase = true;
    private bool _isSecondPurchase = true;
    private bool _isThirdPurchase = true;

    public event Action Started;
    public event Action Done;

    private void OnEnable()
    {
        _marketButton.onClick.AddListener(ActivateCastleTutorial);
        _castle.RoomAdded += ChangeText;
        _prepairButton.onClick.AddListener(ActivatePrepairTutorial);
        _placeHandler.BattleButtonActivated += ActivateFinalMessage;
        _battleButton.onClick.AddListener(DeactivateTutorial);
        _market.ItemSelected += OnItemSelected;
    }

    private void Awake()
    {
        Started?.Invoke();
        _currentTextIndex = 0;
        _blockFrame.ActivateFrame(FirstBlockFramesIndex);
        _infoText.text = LeanLocalization.GetTranslationText(StartMessage);
    }

    private void OnDisable()
    {
        _marketButton.onClick.RemoveListener(ActivateCastleTutorial);
        _castle.RoomAdded -= ChangeText;
        _prepairButton.onClick.RemoveListener(ActivatePrepairTutorial);
        _placeHandler.BattleButtonActivated -= ActivateFinalMessage;
        _battleButton.onClick.RemoveListener(DeactivateTutorial);
        _market.ItemSelected -= OnItemSelected;
    }

    private void ActivateCastleTutorial()
    {
        _blockFrame.ActivateFrame(SecondBlockFramesIndex);

        if (_isFirstPurchase)
            _infoText.text = LeanLocalization.GetTranslationText(FirstMessage);
        else if (_isSecondPurchase)
            _infoText.text = LeanLocalization.GetTranslationText(ThirdMessage);
        else if (_isThirdPurchase)
            _infoText.text = LeanLocalization.GetTranslationText(FifthMessage);
    }

    private void ChangeText(IReadOnlyRoom readOnlyRoom)
    {
        _blockFrame.ActivateFrame(FirstBlockFramesIndex);

        if (_isFirstPurchase)
        {
            _isFirstPurchase = false;
            _infoText.text = LeanLocalization.GetTranslationText(SecondMessage);
        }
        else if (_isSecondPurchase)
        {
            _isSecondPurchase = false;
            _infoText.text = LeanLocalization.GetTranslationText(FourthMessage);
        }
        else if (_isThirdPurchase)
        {
            _isThirdPurchase = false;
            _blockFrame.ActivateFrame(ForthBlockFramesIndex);
            _infoText.text = LeanLocalization.GetTranslationText(SixthMessage);
        }
    }

    private void ActivatePrepairTutorial()
    {
        _blockFrame.DeactivateAllFrames();
        _infoText.text = LeanLocalization.GetTranslationText(SeventhMessage);
    }

    private void ActivateFinalMessage()
    {
        _infoText.text = LeanLocalization.GetTranslationText(EighthMessage);
    }

    private void DeactivateTutorial()
    {
        Done?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnItemSelected(MarketItem marketItem)
    {
        _blockFrame.ActivateFrame(ThirdBlockFramesIndex);
    }
}
