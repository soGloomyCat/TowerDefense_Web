using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private List<Warrior> _warriors;

    private void Awake()
    {
        _warriors = new List<Warrior>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Enemy enemy))
            TakeTarget(enemy);
    }

    public void AddNewWarrior(Warrior warrior)
    {
        _warriors.Add(warrior);
    }

    public void Clean()
    {
        _warriors = new List<Warrior>();
    }

    private void TakeTarget(Enemy enemy)
    {
        foreach (var warrior in _warriors)
        {
            if (warrior.IsBusy == false)
            {
                warrior.Attack(enemy);
                break;
            }

        }
    }
}
