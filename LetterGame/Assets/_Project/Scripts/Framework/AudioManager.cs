
using UnityEngine;
using LetterQuest.Framework.Utilities;

namespace LetterQuest.Framework.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioClip musicClips;
        [SerializeField] private AudioClip[] sfxClips;
        private AudioSource _bgMusicSource;
        private AudioSource _sfxSource;

        #region Unity Methods

        private void Start() => Initialize(GetComponents<AudioSource>());
        private void OnDisable() => Dispose();

        #endregion

        #region Public Methods

        public void SetSfxVolume(float volume) => _sfxSource.volume = ClampVolume(volume);
        public void SetMusicVolume(float volume) => _bgMusicSource.volume = ClampVolume(volume);

        public void PlaySoundEffect(int clipIndex)
        {
            if (clipIndex < 0 || clipIndex >= sfxClips.Length) return;
            _sfxSource.PlayOneShot(sfxClips[clipIndex]);
        }

        #endregion

        #region Private Methods

        private void Initialize(AudioSource[] audioSources)
        {
            _bgMusicSource = audioSources[0];
            _sfxSource = audioSources[1];
            ConfigureAndPlayBgMusic();
            ConfigureSfxSource();
        }

        private void Dispose()
        {
            CleanupBgMusic();
            CleanupSfx();
        }

        private void ConfigureAndPlayBgMusic()
        {
            _bgMusicSource.clip = musicClips;
            _bgMusicSource.priority = 128;
            _bgMusicSource.volume = 0.5f;
            _bgMusicSource.loop = true;
            _bgMusicSource.Play();
        }

        private void ConfigureSfxSource()
        {
            _sfxSource.priority = 16;
            _sfxSource.loop = false;
            _sfxSource.volume = 1f;
        }

        private void CleanupBgMusic()
        {
            if (ReferenceEquals(_bgMusicSource, null)) return;
            if (_bgMusicSource.isPlaying == false) return;
            _bgMusicSource.Stop();
            _bgMusicSource = null;
        }

        private void CleanupSfx()
        {
            if (ReferenceEquals(_sfxSource, null)) return;
            if (_sfxSource.isPlaying == false) return;
            _sfxSource.Stop();
            _sfxSource = null;
        }

        private static float ClampVolume(float volume) => Mathf.Clamp01(volume);

        #endregion
    }
}
