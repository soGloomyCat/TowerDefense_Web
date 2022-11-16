using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragCamera : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 tempPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            Debug.Log(tempPosition);
            transform.position = new Vector3(transform.position.x + tempPosition.x/2, transform.position.y, transform.position.z);
        }
    }
}
