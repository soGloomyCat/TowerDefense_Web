using UnityEngine;
using UnityEngine.UI;
using Agava.WebUtility;
using Agava.YandexGames;
using TowerDefense.Daniel;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Leaderboard _leaderboard;
    [SerializeField] private Button _leaderboardButton;
    [SerializeField] private Button _leaderboardExitButton;
    [SerializeField] private GameObject _pausePanel;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private MusicPlayer _musicPlayer;

    private void OnEnable()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Agava.YandexGames.YandexGamesSdk.IsInitialized)
            if (Agava.YandexGames.PlayerAccount.IsAuthorized == false)
                _leaderboardButton.gameObject.SetActive(false);
#endif

        _leaderboardButton.onClick.AddListener(OpenLeaderboard);
        _leaderboardExitButton.onClick.AddListener(CloseLeaderboard);
        _pauseButton.onClick.AddListener(ResumeGame);
    }

    private void Update()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (WebApplication.InBackground)
            OpenPausePanel();
#endif
    }

    private void OnDisable()
    {
        _leaderboardButton.onClick.RemoveListener(OpenLeaderboard);
        _leaderboardExitButton.onClick.RemoveListener(CloseLeaderboard);
        _pauseButton.onClick.RemoveListener(ResumeGame);
    }

    private void OpenLeaderboard()
    {
        _leaderboard.gameObject.SetActive(true);
    }

    private void CloseLeaderboard()
    {
        _leaderboard.PrepairDeactivate();
    }

    private void OpenPausePanel()
    {
        Time.timeScale = 0;
        _pausePanel.gameObject.SetActive(true);
        _musicPlayer.Disable();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        _pausePanel.gameObject.SetActive(false);
        _musicPlayer.Enable();
    }
}
