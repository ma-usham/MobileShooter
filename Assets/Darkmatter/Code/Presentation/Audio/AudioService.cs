using Darkmatter.Core;
using System.Collections.Generic;
using UnityEngine;

namespace Darkmatter.Presentation
{

    [System.Serializable]
    public struct AudioEntry
    {
        public AudioId id;
        public AudioClip clip;
    }
    public class AudioService : MonoBehaviour, IAudioService
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Audio Clips")]
        [SerializeField] private AudioEntry[] clips;

        private Dictionary<AudioId, AudioClip> _clipMap;

        private void Awake()
        {
            _clipMap = new Dictionary<AudioId, AudioClip>();

            foreach (var entry in clips)
            {
                if (!_clipMap.ContainsKey(entry.id))
                    _clipMap.Add(entry.id, entry.clip);
            }

            DontDestroyOnLoad(gameObject);
            PlayMusic(AudioId.Music_Gameplay);
        }

        public void PlayMusic(AudioId id)
        {
            if (!_clipMap.TryGetValue(id, out var clip)) return;

            musicSource.clip = clip;
            musicSource.loop = true;
            musicSource.Play();
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void PlaySFX(AudioId id,float volume)
        {
            if (!_clipMap.TryGetValue(id, out var clip)) return;
            sfxSource.PlayOneShot(clip,volume);
        }

        public void PlaySFXAt(AudioId id, Vector3 position)
        {
            if (!_clipMap.TryGetValue(id, out var clip)) return;
            AudioSource.PlayClipAtPoint(clip, position);
        }
    }
}
