
using UnityEngine;
using LetterQuest.Framework.Utilities;

namespace LetterQuest.Framework.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioClip musicClips;
        public AudioClip[] sfxClips;
        private AudioSource _sfxSource;
        private AudioSource _backgroundMusicSource;

        #region Unity Methods

        private void Start()
        {
            Initialize(GetComponents<AudioSource>());
        }

        private void OnDisable()
        {
            Dispose();
        }

        #endregion

        #region Public Methods

        public void PlaySoundEffect(int clipIndex)
        {
            if (clipIndex < 0 || clipIndex >= sfxClips.Length) return;
            _sfxSource.PlayOneShot(sfxClips[clipIndex]);
        }

        public void SetMusicVolume(float volume)
        {
            _backgroundMusicSource.volume = Mathf.Clamp01(volume);
        }

        public void SetSfxVolume(float volume)
        {
            _sfxSource.volume = Mathf.Clamp01(volume);
        }

        #endregion

        #region Private Methods

        private void Initialize(AudioSource[] audioSources)
        {
            _backgroundMusicSource = audioSources[0];
            _sfxSource = audioSources[1];
            ConfigureAndPlayBgMusic();
            ConfigureSfxSource();
        }

        private void Dispose()
        {
            StopSfxSound();
            StopBgMusic();
        }

        private void ConfigureAndPlayBgMusic()
        {
            _backgroundMusicSource.clip = musicClips;
            _backgroundMusicSource.priority = 128;
            _backgroundMusicSource.volume = 0.75f;
            _backgroundMusicSource.loop = true;
            _backgroundMusicSource.Play();
        }

        private void ConfigureSfxSource()
        {
            _sfxSource.priority = 32;
            _sfxSource.loop = false;
            _sfxSource.volume = 1f;
        }

        private void StopBgMusic()
        {
            if (ReferenceEquals(_backgroundMusicSource, null)) return;
            if (_backgroundMusicSource.isPlaying == false) return;
            _backgroundMusicSource.Stop();
            _backgroundMusicSource = null;
        }

        private void StopSfxSound()
        {
            if (ReferenceEquals(_sfxSource, null)) return;
            if (_sfxSource.isPlaying == false) return;
            _sfxSource.Stop();
            _sfxSource = null;
        }

        #endregion
    }
}
