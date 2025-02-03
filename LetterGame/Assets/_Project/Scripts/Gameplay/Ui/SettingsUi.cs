
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using LetterQuest.Framework.Ui;
using LetterQuest.Framework.Audio;
using UnityEngine.Rendering.Universal;

namespace LetterQuest.Gameplay.Ui
{
    public class SettingsUi : CanvasGroupHandler
    {
        [SerializeField] private Image audioIcon;
        [SerializeField] private Sprite audioOnIcon;
        [SerializeField] private Sprite audioOffIcon;
        private ColorAdjustments _colorAdjustments;
        private Volume _volume;

        private void Start()
        {
            _volume = FindFirstObjectByType<Volume>();
            UpdateAudioButton(AudioManager.Instance.IsMuted);
            _volume.profile.TryGet(out _colorAdjustments);
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

        public void ContrastChanged(float value)
        {
            _colorAdjustments.contrast.value = value;
        }

        public void HueChanged(float value)
        {
            _colorAdjustments.hueShift.value = value;
        }

        public void SaturationChanged(float value)
        {
            _colorAdjustments.saturation.value = value;
        }
    }
}
