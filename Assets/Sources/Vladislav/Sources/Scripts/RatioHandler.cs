using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatioHandler : MonoBehaviour
{
    [SerializeField] private Camera _castleCamera;
    [SerializeField] private Camera _prepairCamera;
    [SerializeField] private Camera _battleCamera;
    [SerializeField] private BattleCanvas _battleCanvas;
    [SerializeField] private PlaceHandler _placeHandler;

    private Vector3 _castleCameraPosition;
    private float _castleCameraFOV;
    private Vector3 _prepairCameraPosition;
    private float _prepairCameraSize;
    private Vector3 _battleCameraPosition;
    private float _battleCameraFOV;

    private void Awake()
    {
        _castleCameraPosition = _castleCamera.transform.position;
        _castleCameraFOV = _castleCamera.fieldOfView;
        _prepairCameraPosition = _prepairCamera.transform.position;
        _prepairCameraSize = _prepairCamera.orthographicSize;
        _battleCameraPosition = _battleCamera.transform.position;
        _battleCameraFOV = _battleCamera.fieldOfView;
    }

    private void Update()
    {
        ControlRatio();
    }

    private void ControlRatio()
    {
        float screenRatio = (Screen.width / Screen.height);

        if (screenRatio >= 1)
        {
            SetHorizontalView1();
        }
        else if (screenRatio < 1)
        {
            SetVerticalView();
        }
    }

    private void SetVerticalView()
    {
        _castleCamera.transform.position = new Vector3(-7.2f, 8.6f, -13f);
        _castleCamera.fieldOfView = 90;
        _prepairCamera.transform.position = new Vector3(18f, 15f, 4f);
        _prepairCamera.orthographicSize = 16.8f;
        _battleCamera.transform.position = new Vector3(17.7f, 18f, -14.4f);
        _battleCamera.fieldOfView = 115;
        _battleCanvas.ActivateVerticalBars();
        _placeHandler.ActivateVerticalPanel();
    }

    private void SetHorizontalView1()
    {
        _castleCamera.transform.position = _castleCameraPosition;
        _castleCamera.fieldOfView = _castleCameraFOV;
        _prepairCamera.transform.position = _prepairCameraPosition;
        _prepairCamera.orthographicSize = _prepairCameraSize;
        _battleCamera.transform.position = _battleCameraPosition;
        _battleCamera.fieldOfView = _battleCameraFOV;
        _battleCanvas.ActivateHorizontalBars();
        _placeHandler.ActivateHorizontalPanel();
    }
}
