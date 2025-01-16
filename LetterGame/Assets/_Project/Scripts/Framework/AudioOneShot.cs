
using UnityEngine;

namespace LetterQuest.Framework.Audio
{
    public class AudioOneShot : MonoBehaviour
    {
        public void PlayOneShot(int id)
        {
            AudioManager.Instance?.PlaySoundEffect(id);
        }
    }
}
