using TMPro;
using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;
using TowerDefense.Daniel.UI;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    private const string StartMessage = "����� � �������, ����� ������ ����� �������";
    private const string FirstMessage = "������ ���������� � �������� � ��������� ������";
    private const string SecondMessage = "� �������, �� ������� �������� �������, � ���� ������� � �������";
    private const string ThirdMessage = "�������� �������������� �����, �� ����� �������� �����";
    private const string FourthMessage = "������� � ������� (� ��������� ���)";
    private const string FifthMessage = "������ �������, ��� �������� �� ���-�� �����";
    private const string SixthMessage = "������ ����� ������ '� ���'";
    private const string SeventhMessage = "�������� ������� �� �����";
    private const string EighthMessage = "�� ������ ��������� ������� ������� � ����� ����������� �������� �����";
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

    private void Awake()
    {
        _info.text = StartMessage;
    }

    private void OnDisable()
    {
        _marketButton.onClick.RemoveListener(ActivateCastleTutorial);
        _castle.RoomAdded -= ChangeText;
        _prepairButton.onClick.RemoveListener(ActivatePrepairTutorial);
        _placeHandler.BattleButtonActivated -= ActivateFinalMessage;
        _battleButton.onClick.RemoveListener(DeactivateTutorial);
    }

    private void ActivateCastleTutorial()
    {
        if (_isFirstPurchase)
            _info.text = FirstMessage;
        else if (_isSecondPurchase)
            _info.text = ThirdMessage;
        else if (_isThirdPurchase)
            _info.text = FifthMessage;
    }

    private void ChangeText(IReadOnlyRoom readOnlyRoom)
    {
        if (_isFirstPurchase)
        {
            _isFirstPurchase = false;
            _info.text = SecondMessage;
        }
        else if (_isSecondPurchase)
        {
            _isSecondPurchase = false;
            _info.text = FourthMessage;
        }
        else if (_isThirdPurchase)
        {
            _isThirdPurchase = false;
            _info.text = SixthMessage;
        }
    }

    private void ActivatePrepairTutorial()
    {
        _info.text = SeventhMessage;
    }

    private void ActivateFinalMessage()
    {
        _info.text = EighthMessage;
    }

    private void DeactivateTutorial()
    {
        gameObject.SetActive(false);
    }
}
