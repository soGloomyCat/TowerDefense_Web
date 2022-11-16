using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TargetSearcher : MonoBehaviour
{
    private Collider _searchCollider;

    public event Action<Enemy> TargetFounded;

    private void Awake()
    {
        _searchCollider = GetComponent<Collider>();
    }

    public void Activate()
    {
        _searchCollider.enabled = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
        {
            if (Vector3.Distance(transform.position, enemy.transform.position) <= 0.5f)
                return;

            TargetFounded?.Invoke(enemy);
            _searchCollider.enabled = false;
        }
    }
}
