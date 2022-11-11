using UnityEngine;
using UnityEngine.UI;

public class BattleDirector : MonoBehaviour
{
    [SerializeField] private CastleHealth _castleHealth;
    [SerializeField] private BattleCanvas _battleCanvas;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private CastleTargets _castleTargets;
    [SerializeField] private EnemySquad _enemySquad;
    [SerializeField] private Money _money;
    [SerializeField] private Button _battleButton;

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

    private void Start()
    {
        _money.Init();
        //OnNewWave();
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
    }

    private void OnPanelButtonClick()
    {
        _enemySquad.Clear();
        _castleHealth.ResetCastle();
    }
}
