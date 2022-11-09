using UnityEngine;

public class BattleDirector : MonoBehaviour
{
    [SerializeField] private CastleHealth _castleHealth;
    [SerializeField] private BattleCanvas _battleCanvas;
    [SerializeField] private EnemySpawner _enemySpawner;

    private void OnEnable()
    {
        _castleHealth.CastleDestroyed += OnCastleDestroy;
        _enemySpawner.AllEnemiesKilled += OnAllEnemiesKill;
        _battleCanvas.PanelButtonClicked += OnPanelButtonClick;
    }

    private void OnDisable()
    {
        _castleHealth.CastleDestroyed -= OnCastleDestroy;
        _enemySpawner.AllEnemiesKilled -= OnAllEnemiesKill;
        _battleCanvas.PanelButtonClicked -= OnPanelButtonClick;
    }

    private void Start()
    {
        OnNewWave();
    }

    public void OnNewWave()
    {
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
        _enemySpawner.StopAttack();
    }

    private void OnPanelButtonClick()
    {
        _enemySpawner.Clear();
        _castleHealth.ResetCastle();
    }
}
