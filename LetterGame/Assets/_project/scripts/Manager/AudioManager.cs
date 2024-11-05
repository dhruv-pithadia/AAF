using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip[] musicClips;  // Background music clips
    public AudioClip[] sfxClips;    // Sound effect clips

    protected override void Awake()
    {
        base.Awake();
        // Additional initialization if needed
    }

    public void PlayMusic(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= musicClips.Length) return;
        backgroundMusicSource.clip = musicClips[clipIndex];
        backgroundMusicSource.Play();
    }

    public void PlaySoundEffect(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= sfxClips.Length) return;
        sfxSource.PlayOneShot(sfxClips[clipIndex]);
    }

    public void StopMusic()
    {
        backgroundMusicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        backgroundMusicSource.volume = Mathf.Clamp01(volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = Mathf.Clamp01(volume);
    }
}