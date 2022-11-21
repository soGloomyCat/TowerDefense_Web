using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class BattleDirector : MonoBehaviour
{
    [SerializeField] private CastleHealth _castleHealth;
    [SerializeField] private BattleCanvas _battleCanvas;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CastleTargets _castleTargets;
    [SerializeField] private EnemySquad _enemySquad;
    [SerializeField] private Money _money;
    [SerializeField] private Button _battleButton;
    [SerializeField] private WavesSlider _wavesSlider;
    [SerializeField] private SettingsStorage _settingsStorage;
    [SerializeField] private LevelsHandler _levelsHandler;
    [SerializeField] private TMP_Text _waveInfo;

    public event UnityAction LevelFinished;
    public event UnityAction<int> WaveFinished;

    private void OnEnable()
    {
        _castleHealth.CastleDestroyed += OnCastleDestroy;
        _enemySquad.AllEnemiesKilled += OnAllEnemiesKill;
        _battleCanvas.PanelButtonClicked += OnPanelButtonClick;
        _battleButton.onClick.AddListener(OnNewWave);
    }

    private void OnDisable()
    {
        _castleHealth.CastleDestroyed -= OnCastleDestroy;
        _enemySquad.AllEnemiesKilled -= OnAllEnemiesKill;
        _battleCanvas.PanelButtonClicked -= OnPanelButtonClick;
        _battleButton.onClick.RemoveListener(OnNewWave);
    }
    /*
    private void Awake(int levelNumber, int waveNumber)
    {
        _money.Init();
        _enemySpawner.Init();
        //OnNewWave();
    }
    */

    private void Awake()
    {
        _levelsHandler.Init();
        _money.Init();
        SetupSpawner();
        _enemySpawner.Init();
        _waveInfo.text = $"Волна {_levelsHandler.CurrentWaveNumber}";
    }

    public void OnNewWave()
    {
        _enemySpawner.TryNextWave();
        _battleCanvas.ShowBar();
    }

    private void OnCastleDestroy()
    {
        OnBattleEnd();
        _battleCanvas.ShowLosePanel(_enemySpawner.WaveSettings.LoseMoney);
    }

    private void OnAllEnemiesKill(int waveNumber)
    {
        _battleCanvas.ShowWinPanel(_enemySpawner.WaveSettings.WinMoney);
        OnBattleEnd();
    }

    private void OnBattleEnd()
    {
        _battleCanvas.HideBar();
        _enemySquad.StopAttack();
        _castleTargets.ResetTargets();
        _wavesSlider.Done(_levelsHandler.CurrentWaveNumber);
        _enemySpawner.OnWaveDone();
        _levelsHandler.OnWaveFinish(_enemySpawner.WaveNumber);

        if (!_enemySpawner.HasNextWave)
        {
            _levelsHandler.OnLevelFinish();
            SetupSpawner();
        }

        _waveInfo.text = $"Волна {_levelsHandler.CurrentWaveNumber}";
    }

    private void OnPanelButtonClick()
    {
        _enemySquad.Clear();
        _castleHealth.ResetCastle();
    }

    private void SetupSpawner()
    {
        _enemySpawner.SetLevelSettings(_settingsStorage.GetSettings(_levelsHandler.CurrentLevelNumber), _levelsHandler.CurrentWaveNumber);
    }
}
