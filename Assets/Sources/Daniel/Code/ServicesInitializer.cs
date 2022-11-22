using System.Collections;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

namespace TowerDefense.Daniel
{
    public class ServicesInitializer : MonoBehaviour
    {
        private IEnumerator Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize();
            
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

            yield break;
        }
    }
}
