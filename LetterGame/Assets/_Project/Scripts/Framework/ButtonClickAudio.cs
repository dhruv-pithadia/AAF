
using UnityEngine;

namespace LetterQuest.Framework.Audio
{
    public class ButtonClickAudio : MonoBehaviour
    {
        public void PlayUiAudio()
        {
            AudioManager.Instance?.PlaySoundEffect(0);
        }
    }
}
