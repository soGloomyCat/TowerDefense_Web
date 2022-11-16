using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteTrail : MonoBehaviour
{
    [SerializeField] private float _lifeTime;

    private float _leftTime;

    private void Update()
    {
        _leftTime += Time.deltaTime;

        if (_leftTime >= _lifeTime)
            Destroy(gameObject);
    }
}
