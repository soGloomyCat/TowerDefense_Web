using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;

    private Coroutine _coroutine;

    private void Awake()
    {
        _coroutine = StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        int index = 0;
        Vector3 tempPosition = _points[index].position;

        while (transform.position != tempPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, tempPosition, 5 * Time.deltaTime);

            if (Vector3.Distance(transform.position, tempPosition) <= 0.5f)
            {
                index++;

                if (index == 4)
                    index = 0;

                tempPosition = _points[index].position;
            }

            yield return null;
        }
    }
}
