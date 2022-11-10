using TowerDefense.Daniel;
using TowerDefense.Daniel.UI;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private Transform _handler;
    [SerializeField] private Animator _handlerAnimator;
    [SerializeField] private Button _marketButton;
    [SerializeField] private Market _marketItem;
    [SerializeField] private RoomHolder _roomHolder;
    [SerializeField] private Button _prepairButton;
    [SerializeField] private PlaceHandler _warriors;
    [SerializeField] private Place _place;
    [SerializeField] private Button _battleButton;

    private MarketItem _item;

    private void OnEnable()
    {
        _marketButton.onClick.AddListener(ActivateSecondTutorial);
        _roomHolder.Clicked += ActivateFourthTutorial;
        _prepairButton.onClick.AddListener(PrepairActivateFifthTutorial);
        _place.Clicked += ActivateSeventhTutorial;
        _battleButton.onClick.AddListener(OverTutorial);
    }

    private void Awake()
    {
        _handler.transform.parent = _marketButton.transform;
        _handler.transform.localPosition = Vector3.zero;
        _handler.transform.localScale = Vector3.one;
    }

    private void OnDisable()
    {
        _marketButton.onClick.RemoveListener(ActivateSecondTutorial);
        _roomHolder.Clicked -= ActivateFourthTutorial;
        _prepairButton.onClick.RemoveListener(PrepairActivateFifthTutorial);
        _place.Clicked -= ActivateSeventhTutorial;
        _battleButton.onClick.RemoveListener(OverTutorial);
        _item.Clicked -= ActivateThirdTutorial;
    }

    private void ActivateSecondTutorial()
    {
        _item = _marketItem.GetItem();
        _item.Clicked += ActivateThirdTutorial;
        _handler.transform.parent = _marketItem.transform;
        _handler.transform.localPosition = new Vector3(-850, -500, 0);
        _handler.transform.localScale = Vector3.one;
    }

    private void ActivateThirdTutorial(MarketItem marketItem)
    {
        _handler.transform.parent = transform;
        _handler.transform.localPosition = new Vector3(650, -50, 0);
        _handler.transform.localScale = Vector3.one;
    }

    private void ActivateFourthTutorial(RoomHolder roomHolder)
    {
        _handler.transform.parent = _prepairButton.transform;
        _handler.transform.localPosition = Vector3.zero;
        _handler.transform.localScale = Vector3.one;
    }

    private void PrepairActivateFifthTutorial()
    {
        Invoke("ActivateFifthTutorial", 0.5f);
    }

    private void ActivateFifthTutorial()
    {
        _handler.transform.parent = _warriors.transform;
        _handler.transform.localPosition = new Vector3(-620, 250, 0);
        _handler.transform.localScale = Vector3.one;
        _handlerAnimator.SetBool("Slide", true);
    }

    private void ActivateSeventhTutorial()
    {
        _handlerAnimator.SetBool("Slide", false);
        _handler.transform.parent = _battleButton.transform;
        _handler.transform.localPosition = new Vector3(0, -75, 0);
        _handler.transform.localScale = Vector3.one;
        _handler.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void OverTutorial()
    {
        _handler.transform.parent = transform;
        _handler.transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
