
using UnityEngine;
using UnityEngine.UI;
using LetterQuest.Framework.Ui;
using LetterQuest.Framework.Audio;

namespace LetterQuest.Gameplay.Ui
{
    public class SettingsUi : CanvasGroupHandler
    {
        [SerializeField] private Image audioIcon;
        [SerializeField] private Sprite audioOnIcon;
        [SerializeField] private Sprite audioOffIcon;

        private void Start()
        {
            UpdateAudioButton(AudioManager.Instance.IsMuted);
        }

        public void MusicVolumeChanged(float value)
        {
            AudioManager.Instance?.SetMusicVolume(value);
        }

        public void SfxVolumeChanged(float value)
        {
            AudioManager.Instance?.SetSfxVolume(value);
        }

        public void ToggleMute()
        {
            UpdateAudioButton(AudioManager.Instance.ToggleMute());
        }
        
        private void UpdateAudioButton(bool isMuted)
        {
            audioIcon.sprite = isMuted ? audioOffIcon : audioOnIcon;
        }
    }
}
