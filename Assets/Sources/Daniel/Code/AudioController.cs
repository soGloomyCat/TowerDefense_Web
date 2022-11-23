using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Clown moment :o)

namespace TowerDefense.Daniel
{
    public class AudioController : MusicPlayer
    {
        [Header("Music")]
        [SerializeField] private AudioClip _castleMusic = null;
        [SerializeField] private AudioClip _prepareMusic = null;
        [SerializeField] private AudioClip _battleMusic = null;
        [SerializeField] private AudioClip _winSound = null;
        [SerializeField] private AudioClip _loseSound = null;

        [Header("SFX")]
        [SerializeField] private AudioClip _placeSound = null;
        [SerializeField] private AudioClip _uiClickSound = null;
        [SerializeField] private AudioClip _arrowSound = null;
        [SerializeField] private AudioClip _magicBallSound = null;
        [SerializeField] private AudioClip _spearSound = null;

        [Header("Targets")]
        [SerializeField] private Button _prepareButton = null;
        [SerializeField] private Button _battleButton = null;
        [SerializeField] private CastleHealth _castleHealth = null;
        [SerializeField] private EnemySquad _enemySquad = null;
        [SerializeField] private BattleCanvas _battleCanvas = null;

        private static AudioController _instance = null;

        private readonly List<Warrior> _subscribedWarriors = new List<Warrior>();
        private readonly List<Place> _subscribedPlaces = new List<Place>();

        protected override void Awake()
        {
            base.Awake();

            _instance = this;

            SwitchTo(_castleMusic);
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _prepareButton.onClick.AddListener(OnPrepareButtonClicked);
            _battleButton.onClick.AddListener(OnBattleButtonClicked);
            _castleHealth.CastleDestroyed += OnCastleDestroyed;
            _enemySquad.AllEnemiesKilled += OnAllEnemiesKilled;
            _battleCanvas.PanelButtonClicked += OnPanelButtonClicked;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            _prepareButton.onClick.RemoveListener(OnPrepareButtonClicked);
            _battleButton.onClick.RemoveListener(OnBattleButtonClicked);
            _castleHealth.CastleDestroyed -= OnCastleDestroyed;
            _enemySquad.AllEnemiesKilled -= OnAllEnemiesKilled;
            _battleCanvas.PanelButtonClicked -= OnPanelButtonClicked;
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.F))
            {
                if (Enabled)
                {
                    Disable();

                    return;
                }

                Enable();
            }
        }

        public static bool TryPlayUIClick()
        {
            return TryPlaySFX(_instance._uiClickSound, 0.75f);
        }

        private IEnumerator SubscribeToPlacesLater()
        {
            yield return null;

            _subscribedPlaces.AddRange(FindObjectsOfType<Place>());

            foreach (var place in _subscribedPlaces)
            {
                place.Clicked += OnPlaceClicked;
            }
        }

        private IEnumerator SubscribeToHandlersLater()
        {
            yield return null;

            _subscribedWarriors.AddRange(FindObjectsOfType<Warrior>());

            foreach (var warrior in _subscribedWarriors)
            {
                warrior.Shot += OnWarriorShot;
            }
        }

        private void OnPrepareButtonClicked()
        {
            SwitchTo(_prepareMusic, true, 0.5f);

            StartCoroutine(SubscribeToPlacesLater());
        }

        private void OnBattleButtonClicked()
        {
            SwitchTo(_battleMusic, true, 0.5f);

            foreach (var place in _subscribedPlaces)
            {
                place.Clicked -= OnPlaceClicked;
            }

            _subscribedPlaces.Clear();

            StartCoroutine(SubscribeToHandlersLater());
        }

        private void OnCastleDestroyed()
        {
            SwitchTo(_loseSound, false, 0.75f);
        }

        private void OnAllEnemiesKilled(int wave)
        {
            SwitchTo(_winSound, false, 0.75f);
        }

        private void OnPanelButtonClicked()
        {
            SwitchTo(_castleMusic, true, 0.5f);

            foreach (var warrior in _subscribedWarriors)
            {
                warrior.Shot -= OnWarriorShot;
            }

            _subscribedWarriors.Clear();
        }

        private void OnWarriorShot(Weapon weapon)
        {
            var sound = weapon switch
            {
                Arrow => _arrowSound,
                MagicBall => _magicBallSound,
                Spear => _spearSound,
                _ => null,
            };

            var volume = weapon switch
            {
                Arrow => 0.25f,
                //MagicBall => 0.5f,
                //Spear => 0.5f,
                _ => 0.5f,
            };

            PlaySFX(sound, volume);
        }

        private void OnPlaceClicked()
        {
            PlaySFX(_placeSound, 0.5f);
        }
    }
}