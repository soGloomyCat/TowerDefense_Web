using UnityEngine;

public class LevelsHandler : MonoBehaviour
{
    public const string LEVEL_NUMBER_KEY = "towerDefense/LevelNumber";
    public const string WAVE_NUMBER_KEY = "towerDefense/WaveNumber";

    [SerializeField] private BattleDirector _battleDirector;

    public int CurrentLevelNumber { get; private set; }
    public int CurrentWaveNumber { get; private set; }

    public void Init()
    {
        CurrentLevelNumber = PlayerPrefs.GetInt(LEVEL_NUMBER_KEY, 1);
        CurrentWaveNumber = PlayerPrefs.GetInt(WAVE_NUMBER_KEY, 1);
    }
    /*
    private void OnEnable()
    {
        _battleDirector.LevelFinished += OnLevelFinish;
        _battleDirector.WaveFinished += OnWaveFinish;
    }

    private void OnDisable()
    {
        _battleDirector.LevelFinished -= OnLevelFinish;
        _battleDirector.WaveFinished -= OnWaveFinish;
    }
    */
    public void OnLevelFinish()
    {
        CurrentLevelNumber++;
        PlayerPrefs.SetInt(LEVEL_NUMBER_KEY, CurrentLevelNumber);
        
        CurrentWaveNumber = 1;
        PlayerPrefs.SetInt(WAVE_NUMBER_KEY, CurrentWaveNumber);
    }

    public void OnWaveFinish(int waveNumber)
    {
        CurrentWaveNumber = waveNumber;
        PlayerPrefs.SetInt(WAVE_NUMBER_KEY, CurrentWaveNumber);
    }
}
