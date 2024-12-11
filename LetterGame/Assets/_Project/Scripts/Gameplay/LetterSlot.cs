
using UnityEngine;

namespace LetterQuest.Gameplay
{
    public class LetterSlot : MonoBehaviour
    {
        public delegate void LetterSlotUpdate();
        public event LetterSlotUpdate LetterSlotUpdateEvent;
        private TextMesh _textMesh;

        private void Awake()
        {
            _textMesh = GetComponentInChildren<TextMesh>();
            ResetLetterSlotText();
        }

        public void SetLetterSlotText(string text)
        {
            _textMesh.text = text;
            LetterSlotUpdateEvent?.Invoke();
        }

        public void ResetLetterSlotText()
        {
            _textMesh.text = string.Empty;
        }

        public bool IsAssigned() => _textMesh.text != string.Empty;
    }
}
