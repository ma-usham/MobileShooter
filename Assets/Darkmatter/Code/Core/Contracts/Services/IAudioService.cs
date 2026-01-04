using UnityEngine;

namespace Darkmatter.Core
{
    public interface IAudioService
    {
        void PlayMusic(AudioId id);
        void StopMusic();

        void PlaySFX(AudioId id,float volume);
        void PlaySFXAt(AudioId id, UnityEngine.Vector3 position);
    }
}
