using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Warrior Stats", menuName = "Tower Defense/Vladislav/Warrior Level Stas", order = 51)]
public class WarriorLevelStats : ScriptableObject
{
    private const string LevelKey = "Level";

    [SerializeField] private List<Stats> _stats;

    private int _currentLevel;

    public int CurrentLevel => _currentLevel;

    public void Inizialize()
    {
        _currentLevel = PlayerPrefs.GetInt(LevelKey, 0);
    }

    public Stats GetCurrentLevelStats()
    {
        return _stats[_currentLevel];
    }

    public void LevelUp()
    {
        if (_currentLevel < _stats.Count - 1)
        {
            _currentLevel++;
            PlayerPrefs.SetInt(LevelKey, _currentLevel);
        }
    }

    [Serializable]
    public struct Stats
    {
        [SerializeField] public int Level;
        [SerializeField] public float Cooldown;
        [SerializeField] public float DamageMultiplier;
        [SerializeField] public int SkinIndex;
    }
}
