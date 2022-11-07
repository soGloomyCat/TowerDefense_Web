using System.Collections;
using UnityEngine;
using Agava.YandexGames;

namespace TowerDefense.Daniel
{
    public class ServicesInitializer : MonoBehaviour
    {
        private IEnumerator Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
            yield return YandexGamesSdk.Initialize();
#endif

            yield break;
        }
    }
}
