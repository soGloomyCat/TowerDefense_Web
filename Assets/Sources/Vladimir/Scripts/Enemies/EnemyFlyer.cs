using System.Collections;
using UnityEngine;

public class EnemyFlyer : MonoBehaviour
{
    [SerializeField] private float _altitude;
    [SerializeField] private float _fallSpeed;

    public float Altitude => _altitude;

    public void Fall()
    {
        StartCoroutine(FallDown());
    }

    private IEnumerator FallDown()
    {
        while (transform.localPosition.y > 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * _fallSpeed);
            yield return null;
        }
    }
}
