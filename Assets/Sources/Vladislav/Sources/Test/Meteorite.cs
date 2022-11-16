using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField] private float _lifeTime;
    [SerializeField] private MeteoriteTrail _trail;
    [SerializeField] private float _trailCooldown;

    private Rigidbody _rigidbody;
    private float _currentTime;
    private float _leftTime;

    private void Update()
    {
        if (_currentTime >= _lifeTime)
            Destroy(gameObject);

        _currentTime += Time.deltaTime;
        _leftTime += Time.deltaTime;
    }

    public void Fall()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.AddRelativeForce(transform.forward * 400);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out MeteoriteZone meteoriteZone) && _leftTime >= _trailCooldown)
        {
            Debug.Log("Check");
            _leftTime = 0;
            MeteoriteTrail tempTrail = Instantiate(_trail);
            tempTrail.transform.position = new Vector3(transform.position.x, meteoriteZone.transform.position.y + 0.05f, transform.position.z);
        }
    }
}
