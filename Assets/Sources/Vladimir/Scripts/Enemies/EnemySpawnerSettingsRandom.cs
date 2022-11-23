using UnityEngine;

[CreateAssetMenu(fileName = "EnemySpawnerSettingsRandom", menuName = "GameAssets/EnemySpawnerSettingsRandom")]
public class EnemySpawnerSettingsRandom : EnemySpawnerSettings
{
    [Header("Random Settings")]
    [SerializeField] protected int _wavesCount;
    [SerializeField] protected Vector2Int _formationCountRange;
    [SerializeField] protected Vector2Int _formationEnemiesCountRange;
    [SerializeField] private EnemyTemplatesHolder _enemyTemplatesHolder;
    [SerializeField] private float _tick;
    [SerializeField] private float _width;
    [SerializeField] private float _gap;
    [SerializeField] private int _winMoney;
    [SerializeField] private int _loseMoney;

    public override void Generate()
    {
        //int wavesCount = Random.Range(_wavesCountRange.x, _wavesCountRange.y);
        WaveSettings[] waveSettings = new WaveSettings[_wavesCount];

        for (int i = 0; i < _wavesCount; i++)
        {
            if (i == 4 || i == 9)
            {
                waveSettings[i] = GenerateBossWave();
            }
            else
            {
                waveSettings[i] = GenerateWave();
            }
        }

        _waves = waveSettings;
    }

    private WaveSettings GenerateWave()
    {
        WaveSettings wave = new WaveSettings();
        wave.SetProps(_tick, _width, _gap, _winMoney, _loseMoney);

        int formationsCount = Random.Range(_formationCountRange.x, _formationCountRange.y + 1);
        WaveFormation[] waveFormations = new WaveFormation[formationsCount];

        for (int j = 0; j < formationsCount; j++)
        {
            waveFormations[j] = GenerateFormation();
        }

        wave.SetFormations(waveFormations);
        return wave;
    }

    private WaveSettings GenerateBossWave()
    {
        WaveSettings wave = new WaveSettings();
        wave.SetProps(_tick, _width, _gap, _winMoney, _loseMoney);

        int formationsCount = 1;
        WaveFormation[] waveFormations = new WaveFormation[formationsCount];

        for (int j = 0; j < formationsCount; j++)
        {
            waveFormations[j] = GenerateBossFormation();
        }

        wave.SetFormations(waveFormations);
        return wave;
    }

    private WaveFormation GenerateFormation()
    {
        WaveFormation waveFormation = new WaveFormation();

        Enemy template = _enemyTemplatesHolder.GetRandomTemplate();
        int count = Random.Range(_formationEnemiesCountRange.x, _formationEnemiesCountRange.y);
        /*
        if (template.TryGetComponent(out EnemyBoss boss))
        {
            count = 1;
        }
        else
        { 
            count = Random.Range(_formationEnemiesCountRange.x, _formationEnemiesCountRange.y);
        }
        */
        waveFormation.SetFormation(template, count);
        return waveFormation;
    }

    private WaveFormation GenerateBossFormation()
    {
        WaveFormation waveFormation = new WaveFormation();

        Enemy template = _enemyTemplatesHolder.GetRandomTemplateBoss();
        int count = 1;
        waveFormation.SetFormation(template, count);
        return waveFormation;
    }
}
