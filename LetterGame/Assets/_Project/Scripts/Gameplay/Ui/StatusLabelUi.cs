
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LetterQuest.Gameplay.Ui
{
    public class StatusLabelUi : MonoBehaviour
    {
        [SerializeField] private Image statusImg;
        [SerializeField] private TMP_Text labelText;

        public void UpdateStatus(string text, Color color)
        {
            if (statusImg.color == color) return;
            statusImg.color = color;
            labelText.text = text;
        }
    }
}
