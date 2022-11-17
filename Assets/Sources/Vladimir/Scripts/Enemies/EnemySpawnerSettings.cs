using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnemySpawnerSettings", menuName = "GameAssets/EnemySpawnerSettings")]
public class EnemySpawnerSettings : ScriptableObject
{
    [SerializeField] protected WaveSettings[] _waves;

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

    public virtual void Generate() { }
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

    public void SetProps(float tick, float width, float gap, int winMoney, int loseMoney)
    {
        _tick = tick;
        _width = width;
        _gap = gap;
        _winMoney = winMoney;
        _loseMoney = loseMoney;
    }

    public void SetFormations(WaveFormation[] waveFormations)
    { 
        _formations = waveFormations;
    }
}

[Serializable]
public class WaveFormation
{
    [SerializeField] private Enemy _enemyTemplate;
    [SerializeField] private int _count;

    public Enemy Template => _enemyTemplate;
    public int Count => _count;

    public void SetFormation(Enemy template, int count)
    { 
        _enemyTemplate = template;
        _count = count;
    }
}
