
using UnityEngine;
using LetterQuest.Core;

namespace LetterQuest
{
    public class ButtonClickAudio : MonoBehaviour
    {
        public void PlayUiAudio()
        {
            AudioManager.Instance.PlaySoundEffect(0);
        }
    }
}
