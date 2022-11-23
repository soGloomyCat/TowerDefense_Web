using UnityEngine;
using Agava.YandexGames;

public class Leaderboard : MonoBehaviour
{
    private const string LeaderboardName = "TowerDefenseLB";
    private const string ScoreKey = "Score";

    [SerializeField] private PlayerFrame _framePrefab;
    [SerializeField] private Transform _pool;
    [SerializeField] private LeaderboardAnimationsHandler _animationsHandler;

    private int _score;

    public int Score => _score;

    private void OnEnable()
    {
        _score = PlayerPrefs.GetInt(ScoreKey, 0);

#if UNITY_WEBGL && !UNITY_EDITOR
#if YANDEX_GAMES
        if (Agava.YandexGames.YandexGamesSdk.IsInitialized)
        {
            ClearEntri();
            Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, onSuccessCallback: (result) =>
            {
                PlayerFrame tempFrame;
                string ananimusName = "Ananimus";

                for (int i = 0; i < result.entries.Length; i++)
                {
                    tempFrame = Instantiate(_framePrefab, _pool);

                    if (result.entries[i].player.publicName == "")
                        tempFrame.SetData(ananimusName, result.entries[i].score);
                    else
                        tempFrame.SetData(result.entries[i].player.publicName, result.entries[i].score);
                }
            });
        }
#endif
#endif

#if UNITY_EDITOR
        ClearEntri();
        PlayerFrame tempFrame;

        for (int i = 0; i < 5; i++)
        {
            tempFrame = Instantiate(_framePrefab, _pool);
            tempFrame.SetData("ananimusName", i);
        }
#endif
    }

    public void SaveScore()
    {
        PlayerPrefs.SetInt(ScoreKey, ++_score);

#if UNITY_WEBGL && !UNITY_EDITOR
#if YANDEX_GAMES
        Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, _score);
#endif
#endif
    }

    public void PrepairDeactivate()
    {
        _animationsHandler.Deactivate();
    }

    public void DeactivateView()
    {
        gameObject.SetActive(false);
    }

    private void ClearEntri()
    {
        for (int i = 0; i < _pool.childCount; i++)
            Destroy(_pool.GetChild(i).gameObject);
    }
}
