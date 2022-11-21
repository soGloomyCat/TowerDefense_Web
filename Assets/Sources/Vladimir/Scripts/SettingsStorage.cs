using UnityEngine;

public class SettingsStorage : MonoBehaviour
{
    [SerializeField] private EnemySpawnerSettings[] _settingsList;

    public EnemySpawnerSettings GetSettings(int levelNumber)
    {
        switch (levelNumber)
        {
            case 1:
                return _settingsList[0];
            case 2:
                return _settingsList[1];
            case 3:
                return _settingsList[2];
            default:
                return _settingsList[3];
        }
    }
}
