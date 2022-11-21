using TMPro;
using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.UI;
using UnityEngine;
using UnityEngine.UI;
using Lean.Localization;

public class Tutorial : MonoBehaviour
{
    private const string StartMessage = "Зайди в магазин, чтобы купить новую комнату";
    private const string FirstMessage = "Возьми стрельбище и установи в свободную ячейку";
    private const string SecondMessage = "В будущем, ты сможешь улучшать комнаты, а пока вернись в магазин";
    private const string ThirdMessage = "Установи стратегический пункт, он будет улучшать замок";
    private const string FourthMessage = "Вернись в магазин (в последний раз)";
    private const string FifthMessage = "Возьми кузницу, она отвечает за кол-во башен";
    private const string SixthMessage = "Теперь нажми кнопку 'В бой'";
    private const string SeventhMessage = "Перетащи лучника на башню";
    private const string EighthMessage = "Ты можешь поставить второго лучника и после отправляйся защищать замок";
    private const string NinthMessage = "";
    private const string TenthMessage = "";

    [SerializeField] private TMP_Text _info;
    [SerializeField] private Button _marketButton;
    [SerializeField] private Castle _castle;
    [SerializeField] private Button _prepairButton;
    [SerializeField] private PlaceHandler _placeHandler;
    [SerializeField] private Button _battleButton;

    private bool _isFirstPurchase = true;
    private bool _isSecondPurchase = true;
    private bool _isThirdPurchase = true;

    private void OnEnable()
    {
        _marketButton.onClick.AddListener(ActivateCastleTutorial);
        _castle.RoomAdded += ChangeText;
        _prepairButton.onClick.AddListener(ActivatePrepairTutorial);
        _placeHandler.BattleButtonActivated += ActivateFinalMessage;
        _battleButton.onClick.AddListener(DeactivateTutorial);
    }

    private void OnDisable()
    {
        _marketButton.onClick.RemoveListener(ActivateCastleTutorial);
        _castle.RoomAdded -= ChangeText;
        _prepairButton.onClick.RemoveListener(ActivatePrepairTutorial);
        _placeHandler.BattleButtonActivated -= ActivateFinalMessage;
        _battleButton.onClick.RemoveListener(DeactivateTutorial);
    }

    private void Start()
    {
        _info.text = LeanLocalization.GetTranslationText(nameof(StartMessage));
    }

    private void ActivateCastleTutorial()
    {
        if (_isFirstPurchase)
            _info.text = LeanLocalization.GetTranslationText(nameof(FirstMessage));
        else if (_isSecondPurchase)
            _info.text = LeanLocalization.GetTranslationText(nameof(ThirdMessage));
        else if (_isThirdPurchase)
            _info.text = LeanLocalization.GetTranslationText(nameof(FifthMessage));
    }

    private void ChangeText(IReadOnlyRoom readOnlyRoom)
    {
        if (_isFirstPurchase)
        {
            _isFirstPurchase = false;
            _info.text = LeanLocalization.GetTranslationText(nameof(SecondMessage));
        }
        else if (_isSecondPurchase)
        {
            _isSecondPurchase = false;
            _info.text = LeanLocalization.GetTranslationText(nameof(FourthMessage));
        }
        else if (_isThirdPurchase)
        {
            _isThirdPurchase = false;
            _info.text = LeanLocalization.GetTranslationText(nameof(SixthMessage));
        }
    }

    private void ActivatePrepairTutorial()
    {
        _info.text = LeanLocalization.GetTranslationText(nameof(SeventhMessage));
    }

    private void ActivateFinalMessage()
    {
        _info.text = LeanLocalization.GetTranslationText(nameof(EighthMessage));
    }

    private void DeactivateTutorial()
    {
        gameObject.SetActive(false);
    }
}
