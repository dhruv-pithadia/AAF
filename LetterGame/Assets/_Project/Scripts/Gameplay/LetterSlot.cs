
using UnityEngine;
using UnityEngine.UI;

namespace LetterQuest.Gameplay
{
    public class LetterSlot : MonoBehaviour
    {
        public delegate void LetterSlotUpdate();
        public event LetterSlotUpdate LetterSlotUpdateEvent;
        private Image _slotBorderImage;
        private TextMesh _textMesh;

        private void Awake()
        {
            _textMesh = GetComponentInChildren<TextMesh>();
            _slotBorderImage = GetComponent<Image>();
            ResetLetterSlotText();
        }

        public char GetLetter() => _textMesh.text[^1];
        public bool IsAssigned => _textMesh.text != string.Empty;
        public void AssignSlotBorderColor(Color color) => _slotBorderImage.color = color;

        public void SetLetterSlotText(string text)
        {
            _textMesh.text = text;
            LetterSlotUpdateEvent?.Invoke();
        }

        public void ResetLetterSlotText()
        {
            _textMesh.text = string.Empty;
            AssignSlotBorderColor(Color.black);
        }
    }
}
