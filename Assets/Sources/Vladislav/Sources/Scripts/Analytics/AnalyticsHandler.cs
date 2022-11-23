using UnityEngine;
using Agava.YandexMetrica;
using TowerDefense.Daniel;
using TowerDefense.Daniel.Interfaces;

public class AnalyticsHandler : MonoBehaviour
{
    [SerializeField] private Castle _castle;
    [SerializeField] private BattleDirector _battleDirector;
    [SerializeField] private Tutorial _tutorial;
    [SerializeField] private AdHandler _adHandler;

    private float _totalTime;

    private void OnEnable()
    {
        _castle.RoomAdded += OnRoomAdded;
        _castle.RoomUpgraded += OnRoomUpgraded;
        _battleDirector.WaveStarted += OnWaveStartde;
        _battleDirector.WaveFinished += OnWaveFinished;
        _battleDirector.WaveFailed += OnWaveFailed;
        _tutorial.Started += OnTutorialStarted;
        _tutorial.Done += OnTutorialDone;
        _adHandler.AdStarted += OnAdStarted;
        _adHandler.AdFinished += OnAdFinished;
        _adHandler.RewardAdStarted += OnRewardedAdStarted;
        _adHandler.RewardAdFinished += OnRewardedAdFinished;
    }

    private void Awake()
    {
        _totalTime = 0;
    }

    private void OnDisable()
    {
        SendTotalTime();
        _castle.RoomAdded -= OnRoomAdded;
        _castle.RoomUpgraded -= OnRoomUpgraded;
        _battleDirector.WaveStarted -= OnWaveStartde;
        _battleDirector.WaveFinished -= OnWaveFinished;
        _battleDirector.WaveFailed -= OnWaveFailed;
        _tutorial.Started -= OnTutorialStarted;
        _tutorial.Done -= OnTutorialDone;
        _adHandler.AdStarted -= OnAdStarted;
        _adHandler.AdFinished -= OnAdFinished;
        _adHandler.RewardAdStarted -= OnRewardedAdStarted;
        _adHandler.RewardAdFinished -= OnRewardedAdFinished;
    }

    private void Update()
    {
        _totalTime += Time.deltaTime;
    }

    private void OnRoomAdded(IReadOnlyRoom room)
    {
        RoomInfoConverter textConverter = new RoomInfoConverter(room.Information.Title, room.Information.BuyingPrices[0]);
        YandexMetrica.Send("roomAdded", textConverter.GetJsonText());
    }

    private void OnRoomUpgraded(IReadOnlyRoom room)
    {
        RoomInfoConverter textConverter = new RoomInfoConverter(room.Information.Title, room.Information.UpgradePrices[room.Level - 1]);
        YandexMetrica.Send("roomUpgraded", textConverter.GetJsonText());
    }

    private void OnWaveStartde(int waveIndex)
    {
        WaveInfoConverter textConverter = new WaveInfoConverter(waveIndex, 0);
        YandexMetrica.Send("waveStarted", textConverter.GetJsonText());
    }

    private void OnWaveFinished(int waveIndex, float waveLeftTime)
    {
        WaveInfoConverter textConverter = new WaveInfoConverter(waveIndex, waveLeftTime);
        YandexMetrica.Send("waveFinished", textConverter.GetJsonText());
    }

    private void OnWaveFailed(int waveIndex, float waveLeftTime)
    {
        WaveInfoConverter textConverter = new WaveInfoConverter(waveIndex, waveLeftTime);
        YandexMetrica.Send("waveFailed", textConverter.GetJsonText());
    }

    private void SendTotalTime()
    {
        TotalTimeInfoConverter textConverter = new TotalTimeInfoConverter(_totalTime);
        YandexMetrica.Send("totalTime", textConverter.GetJsonText());
    }

    private void OnTutorialStarted()
    {
        YandexMetrica.Send("tutorialStarted");
    }

    private void OnTutorialDone()
    {
        TotalTimeInfoConverter textConverter = new TotalTimeInfoConverter(_totalTime);
        YandexMetrica.Send("tutorialDone", textConverter.GetJsonText());
    }

    private void OnAdStarted()
    {
        YandexMetrica.Send("adStarted");
    }

    private void OnAdFinished()
    {
        YandexMetrica.Send("adFinished");
    }

    private void OnRewardedAdStarted()
    {
        YandexMetrica.Send("rewardedAdStarted");
    }

    private void OnRewardedAdFinished()
    {
        YandexMetrica.Send("rewardedAdFinished");
    }

    private struct RoomInfoConverter
    {
        public string Name;
        public int Cost;

        public RoomInfoConverter(string name, int value)
        {
            Name = name;
            Cost = value;
        }

        public string GetJsonText()
        {
            return JsonUtility.ToJson(this);
        }
    }

    private struct WaveInfoConverter
    {
        public int Number;
        public float Duration;

        public WaveInfoConverter(int number, float duration)
        {
            Number = number;
            Duration = duration;
        }

        public string GetJsonText()
        {
            return JsonUtility.ToJson(this);
        }
    }

    private struct TotalTimeInfoConverter
    {
        public float LeftTime;

        public TotalTimeInfoConverter(float duration)
        {
            LeftTime = duration;
        }

        public string GetJsonText()
        {
            return JsonUtility.ToJson(this);
        }
    }
}
