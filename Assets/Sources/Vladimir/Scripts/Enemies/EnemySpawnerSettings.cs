using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawnerSettings", menuName = "GameAssets/EnemySpawnerSettings")]
public class EnemySpawnerSettings : ScriptableObject
{
    [SerializeField] private WaveSettings[] _waves;

    public int Count => _waves.Length;
    public WaveSettings this[int index]
    {
        get
        {
            if (index < _waves.Length)
            {
                return _waves[index];
            }
            else
            { 
                return null;
            }
        }
    }
}

[Serializable]
public class WaveSettings
{
    [SerializeField] private WaveFormation[] _formations;
    [SerializeField] private float _tick;
    [SerializeField] private float _width;
    [SerializeField] private float _gap;
    [SerializeField] private int _winMoney;
    [SerializeField] private int _loseMoney;

    public IReadOnlyCollection<WaveFormation> Formations => _formations;
    public float Tick => _tick;
    public float Width => _width;
    public float Gap => _gap;
    public int LoseMoney => _loseMoney;
    public int WinMoney => _winMoney;

    public int Count
    {
        get
        {
            int result = 0;

            foreach (WaveFormation formation in _formations)
            { 
                result += formation.Count;
            }

            return result;
        }
    }
}

[Serializable]
public class WaveFormation
{
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private int _count;

    public Enemy Template => _enemyTemplate;
    public int Count => _count;
}
