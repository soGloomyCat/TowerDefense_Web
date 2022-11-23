using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

namespace TowerDefense.Daniel
{
    public class ServicesInitializer : MonoBehaviour
    {
        [SerializeField] private GameObject _gameScene;
        [SerializeField] private DataHandler _dataHandler;

        private IEnumerator Start()
        {
            PlayerPrefs.DeleteAll();
#if UNITY_WEBGL && !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize();
            Agava.YandexGames.InterstitialAd.Show(onOpenCallback: null, onCloseCallback: StartGame, onErrorCallback: StartGame);
            SetLanguage();
#endif
            StartGame(true);
            yield break;
        }

        private void StartGame(bool isInizialize)
        {
            _dataHandler.LoadProgressData();
            _gameScene.SetActive(true);
        }

        private void StartGame(string message)
        {
            _dataHandler.LoadProgressData();
            _gameScene.SetActive(true);
        }

        private void SetLanguage()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            var lang = YandexGamesSdk.Environment.i18n.lang switch
            {
                "ru" => "Russian",
                "be" => "Russian",
                "kk" => "Russian",
                "uk" => "Russian",
                "uz" => "Russian",
                "tr" => "Turkish",
                _ => "English",
            };

            LeanLocalization.SetCurrentLanguageAll(lang);
#endif
        }
    }
}
