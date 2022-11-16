using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : Weapon
{
    [SerializeField] private float _lifeTime;

    private float _leftTime;
    private bool _canMove;

    protected override IEnumerator Fly(Enemy enemy)
    {
        _canMove = true;
        transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + 4f, enemy.transform.position.z);
        Vector3 tempPosition = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);

        while (transform.position.y < tempPosition.y)
        {
            transform.position = Vector3.MoveTowards(transform.position, tempPosition, Speed * Time.deltaTime);
            yield return null;
        }

        tempPosition = new Vector3(transform.position.x, transform.position.y - 10f, transform.position.z);

        while (_leftTime <= _lifeTime)
        {
            if (_canMove)
                transform.position = Vector3.MoveTowards(transform.position, tempPosition, Speed * Time.deltaTime);

            if (transform.position.y <= 0)
                _canMove = false;

            _leftTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
