using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TowerDefense.Daniel.Models;
using System.Linq;

namespace TowerDefense.Daniel.UI
{
    public class StatView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name = null;
        [SerializeField] private TMP_Text _value = null;

        public void Initialize(int level, RoomMarketInformation.Stat stat)
        {
            _name.text = stat.Name;

            /*var result = 0;
            for (int i = 0; i < stat.Values.Count && i <= level; i++)
            {
                result += stat.Values[i];
            }
            var addition = level < stat.Values.Count ? $"<color=green>+{stat.Values[level + 1]}" : "";*/

            var result = stat.Values[level];
            var addition = level + 1 < stat.Values.Count ? $"<color=green>+{stat.Values[level + 1] - result}" : "";

            _value.text = $"{result}{addition}";
        }
    }
}