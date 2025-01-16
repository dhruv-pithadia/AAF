
using LetterQuest.Framework.Ui;
using LetterQuest.Framework.Audio;

namespace LetterQuest.Gameplay.Data
{
    public class AudioSettings : CanvasGroupHandler
    {
        public void ToggleAudioSettings() => ToggleUi();

        public void MusicVolumeChanged(float value)
        {
            AudioManager.Instance?.SetMusicVolume(value);
        }

        public void SfxVolumeChanged(float value)
        {
            AudioManager.Instance?.SetSfxVolume(value);
        }
    }
}
