using UnityEngine;
using Agava.YandexGames;

public class ScoreSaver : MonoBehaviour
{
    private const string LeaderboardName = "TowerDefenseLB";

    public void SaveScore(int newScore)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Agava.YandexGames.YandexGamesSdk.IsInitialized)
        {
            Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, onSuccessCallback: (result) =>
            {
                if (result.score < newScore)
                    Agava.YandexGames.Leaderboard.SetScore(LeaderboardName, newScore);
            });
        }
#endif
    }
}
