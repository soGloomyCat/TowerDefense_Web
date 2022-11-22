using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TowerDefense.Daniel
{
    public abstract class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private float _maxVolume = 0.75f;

        private readonly Dictionary<AudioClip, AudioSource> _sources = new Dictionary<AudioClip, AudioSource>();

        private static MusicPlayer _instance = null;
        private AudioSource _sfx = null;
        private AudioClip _current = null;
        private bool _enabled = true;

        public bool Enabled => _enabled;

        protected virtual void Awake()
        {
            if (_instance != null)
            {
                throw new InvalidOperationException("Can't have multiple music players at once");
            }
            _instance = this;

            var go = new GameObject("SFX");
            go.transform.parent = transform;
            _sfx = go.AddComponent<AudioSource>();
        }

        public void Enable()
        {
            _enabled = true;

            foreach (var source in _sources)
            {
                source.Value.UnPause();
            }
        }

        public void Disable()
        {
            _enabled = false;

            foreach (var source in _sources)
            {
                source.Value.Pause();
            }
        }

        public void SwitchTo(AudioClip clip, bool isLooped = true, float speed = 0.25f)
        {
            if (speed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(speed));
            }

            if (_current == clip)
            {
                return;
            }

            if (_current != null && _sources.ContainsKey(_current))
            {
                var currentClip = _current;
                var current = _sources[currentClip];

                current.DOKill();
                current.DOFade(0, speed).SetSpeedBased().OnComplete(() =>
                {
                    Destroy(current.gameObject);
                    _sources.Remove(currentClip);
                });
            }

            _current = clip;

            if (_sources.ContainsKey(clip))
            {
                _sources[clip].DOKill();
                _sources[clip].DOFade(_maxVolume, speed).SetSpeedBased();

                return;
            }

            var go = new GameObject(clip.name);
            go.transform.parent = transform;

            var source = go.AddComponent<AudioSource>();

            source.clip = clip;
            source.loop = isLooped;
            //source.pitch = Enabled ? 1 : 0;
            source.DOFade(_maxVolume, speed).From(0).SetSpeedBased();
            if (Enabled)
            {
                source.Play();
            }

            _sources[clip] = source;
        }

        public void PlaySFX(AudioClip clip, float volumeScale = 0.75f)
        {
            if (!Enabled)
            {
                return;
            }

            _sfx.PlayOneShot(clip, volumeScale);
        }

        public static bool TryPlaySFX(AudioClip clip, float volumeScale = 0.75f)
        {
            if (_instance != null)
            {
                _instance.PlaySFX(clip, volumeScale);

                return true;
            }

            return false;
        }
    }
}