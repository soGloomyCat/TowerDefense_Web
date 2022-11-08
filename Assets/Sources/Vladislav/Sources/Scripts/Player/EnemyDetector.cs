using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public event Action<Vladislav.Enemy> EnemyFounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Vladislav.Enemy enemy))
            EnemyFounded?.Invoke(enemy);
    }
}
