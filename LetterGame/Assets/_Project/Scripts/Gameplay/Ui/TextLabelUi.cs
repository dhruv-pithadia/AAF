
using TMPro;
using UnityEngine;

namespace LetterQuest.Gameplay.Ui
{
    public class TextLabelUi : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowText(string labelText)
        {
            gameObject.SetActive(true);
            text.text = labelText;
        }

        public void HideText()
        {
            gameObject.SetActive(false);
            text.text = string.Empty;
        }
    }
}
