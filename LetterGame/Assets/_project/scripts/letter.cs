
using TMPro;
using UnityEngine;

namespace LetterQuest
{
    public class Letter : MonoBehaviour
    {
        private TMP_Text _letterText;

        public void OnSpawn(string text)
        {
            _letterText = GetComponentInChildren<TMP_Text>();
            _letterText.text = text;
        }

        public void OnDespawn()
        {
            _letterText.text = string.Empty;
        }
    }
}
