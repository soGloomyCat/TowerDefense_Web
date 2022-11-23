using UnityEngine;
using Agava.YandexGames;
using System;

public class AdHandler : MonoBehaviour
{
    public event Action AdStarted;
    public event Action AdFinished;
    public event Action RewardAdStarted;
    public event Action RewardAdFinished;

    public void ShowInterstitialAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(onOpenCallback: () =>
        {
            AdStarted?.Invoke();
        }, onCloseCallback: (result) =>
        {
            AdFinished?.Invoke();
        }, onErrorCallback: (result) =>
        {
            AdFinished?.Invoke();
        });
#endif
    }

    public void ShowRewardedAd()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(onOpenCallback: () =>
         {
             RewardAdStarted?.Invoke();
         }, onCloseCallback: () =>
         {
             RewardAdFinished?.Invoke();
         }, onErrorCallback: (result) =>
         {
             AdFinished?.Invoke();
         });
#endif
    }
}
