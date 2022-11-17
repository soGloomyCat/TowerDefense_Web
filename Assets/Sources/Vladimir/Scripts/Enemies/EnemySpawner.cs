using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _closePoint;
    [SerializeField] private Transform _farPoint;
    [SerializeField] private float _depthSpread;
    //[SerializeField] private EnemySpawnerSettings _settings;
    //[SerializeField] private Button _battleButton;
    //[SerializeField] private TMP_Text _waveInfo;
    [SerializeField] private EnemySquad _enemySquad;
    //[SerializeField] private Slider _slider;
    [SerializeField] private SliderHandler _slider;
    [SerializeField] private SliderHandler _verticalSlider;
    [SerializeField] private WavesSlider _wavesSlider;
    //[SerializeField] private Transform _targetCastle;

    private EnemySpawnerSettings _settings;
    private Dictionary<float, bool> _points = new Dictionary<float, bool>();
    //private List<Enemy> _enemies = new List<Enemy>();
    private float _timeBuffer;
    private bool _isGoing;
    //private int _wavesIndex;
    private WaveSettings _waveSettings;
    private FormationsDirector _formationsDirector;
    private PointsDirector _pointsDirector;

    //public int WaveNumber => _wavesIndex + 1;
    public int WaveNumber { get; private set; }
    //public IReadOnlyList<Enemy> Enemies => _enemies;
    public WaveSettings WaveSettings => _waveSettings;
    public bool HasNextWave
    {
        get
        {
            return _settings[WaveNumber - 1] != null;
        }
    }

    //public event UnityAction<int> AllEnemiesKilled;
    /*
    private void OnEnable()
    {
        _battleButton.onClick.AddListener(TryNextWave);
    }

    private void OnDisable()
    {
        _battleButton.onClick.RemoveListener(TryNextWave);
    }
    */
    /*
    private void Awake()
    {
        GeneratePoints();
    }

    private void Start()
    {
        _wavesSlider.Generate(_settings.Count);
        _waveInfo.text = $"Волна {WaveNumber}";
    }
    */
    public void Init()
    {
        GeneratePoints();
        //_waveInfo.text = $"Волна {WaveNumber}";
    }

    private void Update()
    {
        Tick();
    }

    public void TryNextWave()
    {
        if (_isGoing)
            return;

        _waveSettings = _settings[WaveNumber - 1];

        if (_waveSettings != null)
        {
            GeneratePoints();
            _formationsDirector = new FormationsDirector(_waveSettings.Formations);
            _pointsDirector = new PointsDirector(_points);
            _enemySquad.OnWaveStart(WaveNumber);
            _slider.Setup(_formationsDirector.EnemiesCount, 0);
            _verticalSlider.Setup(_formationsDirector.EnemiesCount, 0);
            _wavesSlider.Generate(_settings.Count);
            //_slider.maxValue = _formationsDirector.EnemiesCount;
            //_slider.value = 0;
            _isGoing = true;
            return;
        }
        else
        {
            return;
        }
    }

    public void SetLevelSettings(EnemySpawnerSettings settings, int waveNumber)
    { 
        _settings = settings;
        WaveNumber = waveNumber;
        //_waveInfo.text = $"Волна {WaveNumber}";
    }

    private void StopSpawn()
    {
        //_wavesIndex++;
        //_waveInfo.text = $"Волна {WaveNumber}";
        _isGoing = false;
    }

    /*
    public void Clear()
    {
        foreach (Enemy enemy in _enemies)
            Destroy(enemy.gameObject);

        _enemies.Clear();
    }

    public void StopAttack()
    {
        foreach (Enemy enemy in _enemies)
            enemy.StopAttack();
    }
    */

    private void Tick()
    {
        float tick = 0.3f;

        if (_waveSettings != null)
            tick = _waveSettings.Tick;

        _timeBuffer += Time.deltaTime;

        if (_timeBuffer >= tick && _isGoing)
        {
            _timeBuffer = 0;
            Spawn();
        }
    }

    private void Spawn()
    {
        if (!_formationsDirector.TryGetTemplate(out Enemy enemyTemplate))
        {
            StopSpawn();
            return;
        }

        Enemy enemy = Instantiate(enemyTemplate, transform);
        //enemy.Dead += OnEnemyDead;
        //_enemies.Add(enemy);
        _enemySquad.Add(enemy);
        //_slider.value++;
        _slider.Add(1);
        _verticalSlider.Add(1);

        SetPosition(enemy, out float x);
        enemy.Move(GetDestination(enemy, x));
    }

    private void SetPosition(Enemy enemy, out float x)
    {
        if (enemy.TryGetComponent(out EnemyBoss boss))
            x = 0;
        else
            //x = _points[Random.Range(0, _points.Count)];
            x = _pointsDirector.GetPoint();

        enemy.transform.localPosition = new Vector3(x, 0, 0);
    }

    private Vector3 GetDestination(Enemy enemy, float x)
    {
        float z = 0;

        if (enemy is FarEnemy)
        {
            z = _farPoint.localPosition.z;
        }
        else if (enemy is CloseEnemy)
        {
            z = _closePoint.localPosition.z;
        }

        //enemy.SetTarget(_targetCastle.position);

        return new Vector3(x, 0, z + Random.Range(-_depthSpread, _depthSpread));
    }

    private void GeneratePoints()
    {
        float width = 15;
        float gap = 0.5f;

        if (_waveSettings != null)
        {
            width = _waveSettings.Width;
            gap = _waveSettings.Gap;
        }

        _points.Clear();
        float current = -width / 2;

        while (current <= width / 2)
        {
            current += gap;
            _points.Add(current, true);
        }
    }

    public void OnWaveDone()
    {
        WaveNumber++;
        
    }

    /*
    private void OnEnemyDead(Enemy enemy)
    {
        enemy.Dead -= OnEnemyDead;
        _enemies.Remove(enemy);

        if (_enemies.Count == 0)
            AllEnemiesKilled?.Invoke(WaveNumber);

        _waveInfo.text = $"Волна {WaveNumber}";
    }
    */

    void OnDrawGizmos()
    {
        float width = 15;

        if (_waveSettings != null)
            width = _waveSettings.Width;

        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(_closePoint.position, new Vector3(0.2f, 1, width));

        Gizmos.color = Color.red;
        Gizmos.DrawCube(_farPoint.position, new Vector3(0.2f, 1, width));
    }

    private class PointsDirector
    {
        private Dictionary<float, bool> _points;

        public PointsDirector(Dictionary<float, bool> points)
        {
            _points = points;
        }

        public float GetPoint()
        {
            if (HasFreePoint())
            {
                KeyValuePair<float, bool> pair;

                do
                {
                    pair = _points.ElementAt(Random.Range(0, _points.Count));
                }
                while (!pair.Value);

                _points[pair.Key] = false;
                return pair.Key;
            }
            else
            {
                KeyValuePair<float, bool> pair = _points.ElementAt(Random.Range(0, _points.Count));
                _points[pair.Key] = false;
                return pair.Key;
            }
        }

        private bool HasFreePoint()
        {
            foreach (KeyValuePair<float, bool> pair in _points)
            {
                if (pair.Value == true)
                    return true;
            }

            return false;
        }
    }

    private class FormationsDirector
    {
        private List<FormationPair> _formations = new List<FormationPair>();

        public int EnemiesCount { get; private set; }

        public FormationsDirector(IReadOnlyCollection<WaveFormation> waveSettings)
        {
            foreach (WaveFormation waveFormation in waveSettings)
            {
                _formations.Add(new FormationPair() { Template = waveFormation.Template, Count = waveFormation.Count });
                EnemiesCount += waveFormation.Count;
            }
        }

        public bool TryGetTemplate(out Enemy enemy)
        {
            if (HasFormations())
            {
                enemy = GetTemplate();
                return true;
            }
            else
            {
                enemy = null;
                return false;
            }
        }

        private Enemy GetTemplate()
        {
            FormationPair pair;
            int index;

            do
            {
                index = Random.Range(0, _formations.Count);
                pair = _formations[index];
            }
            while (pair.IsCompleted);

            pair.Count--;
            return _formations[index].Template;
        }

        private bool HasFormations()
        {
            foreach (FormationPair pair in _formations)
            {
                if (!pair.IsCompleted)
                    return true;
            }

            return false;
        }

        private class FormationPair
        {
            public Enemy Template;
            public int Count;

            public bool IsCompleted => Count == 0;
        }
    }
}
