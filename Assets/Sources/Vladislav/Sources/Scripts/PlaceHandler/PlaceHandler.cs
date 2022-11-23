using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceHandler : MonoBehaviour
{
    [SerializeField] private List<Place> _places;
    [SerializeField] private WarriorsViewHandler _warriorsHandler;
    [SerializeField] private GridLayoutGroup _warriorsHorizontalPanel;
    [SerializeField] private RectTransform _horizontalPanel;
    [SerializeField] private GridLayoutGroup _warriorsVerticalPanel;
    [SerializeField] private ContentSizeFitter _verticalFitter;
    [SerializeField] private Button _battleButton;
    [SerializeField] private EnemyDetector _enemyDetector;
    [SerializeField] private Holder _holder;

    private List<Place> _activePlaces;
    private List<GameObject> _activeView;
    private bool _needCheck;

    public event Action BattleStarted;
    public event Action BattleButtonActivated;

    private void OnEnable()
    {
        _battleButton.onClick.AddListener(StartGame);
        _activeView = _warriorsHandler.GetActiveWarriors();
        _activePlaces = GetActivePlaces();
        _warriorsHorizontalPanel.enabled = true;
        _warriorsVerticalPanel.enabled = true;
        _needCheck = true;
    }

    private void Update()
    {
        if (_needCheck && CheckPlaces())
            _battleButton.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _battleButton.onClick.RemoveListener(StartGame);
        CleanPanels();
    }

    public void ActivateHorizontalPanel()
    {
        _warriorsHorizontalPanel.gameObject.SetActive(true);
        _warriorsVerticalPanel.gameObject.SetActive(false);
    }

    public void ActivateVerticalPanel()
    {
        _warriorsVerticalPanel.gameObject.SetActive(true);
        _warriorsHorizontalPanel.gameObject.SetActive(false);
    }

    private bool CheckPlaces()
    {
        foreach (var place in _activePlaces)
        {
            if (place.IsEmpty == false)
            {
                _needCheck = false;
                BattleButtonActivated?.Invoke();
                return true;
            }
        }

        return false;
    }

    private void StartGame()
    {
        BattleStarted?.Invoke();

        foreach (var place in _activePlaces)
        {
            if (place.IsEmpty == false)
                _enemyDetector.AddNewWarrior(place.CreateWarrior(_holder));
        }
    }

    private List<Place> GetActivePlaces()
    {
        List<Place> tempPlaces = new List<Place>();
        SetSizePanels(_activeView.Count);

        foreach (var place in _places)
        {
            if (place.IsActive)
            {
                tempPlaces.Add(place);
            }
        }

        for (int i = 0; i < _activeView.Count; i++)
        {
            Instantiate(_activeView[i], _warriorsHorizontalPanel.transform);
            Instantiate(_activeView[i], _warriorsVerticalPanel.transform);
        }

        Invoke("Deactivate", 0.1f);
        return tempPlaces;
    }

    private void Deactivate()
    {
        _warriorsHorizontalPanel.enabled = false;
        _verticalFitter.enabled = false;
        _warriorsVerticalPanel.enabled = false;
    }

    private void CleanPanels()
    {
        for (int i = 0; i < _warriorsHorizontalPanel.transform.childCount; i++)
        {
            Destroy(_warriorsHorizontalPanel.transform.GetChild(i).gameObject);
            Destroy(_warriorsVerticalPanel.transform.GetChild(i).gameObject);
        }

        _battleButton.gameObject.SetActive(false);
    }

    private void SetSizePanels(int activeWarriorsCount)
    {

        switch (activeWarriorsCount)
        {
            case 1:
                _horizontalPanel.anchoredPosition = new Vector2(200, -200);
                _horizontalPanel.sizeDelta = new Vector2(250, 250);
                break;
            case 2:
                _horizontalPanel.anchoredPosition = new Vector2(200, -300);
                _horizontalPanel.sizeDelta = new Vector2(250, 450);
                break;
            case 3:
                _horizontalPanel.anchoredPosition = new Vector2(200, -400);
                _horizontalPanel.sizeDelta = new Vector2(250, 650);
                break;
            default:
                break;
        }
    }
}
