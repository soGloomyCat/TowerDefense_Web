using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private Coroutine _coroutine;

    public void PrepairRotate(Vector3 target)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(RotateTo(target));
    }

    private IEnumerator RotateTo(Vector3 target)
    {
        Vector3 tempDirection = target - transform.position;
        Quaternion tempRotate = Quaternion.LookRotation(tempDirection);
        tempRotate = new Quaternion(transform.rotation.x, tempRotate.y, transform.rotation.z, tempRotate.w);

        while (transform.rotation.y != tempRotate.y)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, tempRotate, _rotateSpeed * Time.fixedDeltaTime);
            yield return null;
        }
    }
}
