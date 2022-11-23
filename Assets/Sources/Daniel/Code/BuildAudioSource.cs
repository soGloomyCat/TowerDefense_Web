using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.Daniel
{
    public class BuildAudioSource : MonoBehaviour
    {
        [SerializeField] private AudioClip _saw = null;
        [SerializeField] private List<AudioClip> _hammer = new List<AudioClip>();

        public void Play()
        {
            StartCoroutine(PlayAudio());
        }

        private IEnumerator PlayAudio()
        {
            var delay = new WaitForSeconds(0.25f);

            MusicPlayer.TryPlaySFX(_saw, 0.5f);

            for (int i = 0; i < 3; i++)
            {
                var clip = _hammer[Random.Range(0, _hammer.Count)];

                MusicPlayer.TryPlaySFX(clip, 0.25f);

                yield return delay;
            }

            //yield return delay; //new WaitForSeconds(0.5f);

            //_audioSource.Stop();
        }
    }
}