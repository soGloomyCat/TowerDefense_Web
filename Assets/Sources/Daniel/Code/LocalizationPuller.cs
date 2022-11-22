using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agava.YandexGames;
using Lean.Localization;

namespace TowerDefense.Daniel
{
    public class LocalizationPuller : MonoBehaviour
    {
        private IEnumerator Start()
        {
            yield return new WaitUntil(() => YandexGamesSdk.IsInitialized);

            LeanLocalization.SetCurrentLanguageAll(YandexGamesSdk.Environment.i18n.lang);
        }
    }
}