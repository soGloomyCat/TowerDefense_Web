using System.Collections;
using UnityEngine;

public class EnemyModelMountPoints : MonoBehaviour, IEnumerable
{
    [SerializeField] private Transform[] _points;

    public IEnumerator GetEnumerator()
    {
        foreach (Transform point in _points)
        {
            if (point != null)
            { 
                yield return point;
            }
        }
    }
}
