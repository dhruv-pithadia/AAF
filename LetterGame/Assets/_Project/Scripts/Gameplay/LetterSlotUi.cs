
using UnityEngine;
using UnityEngine.UI;

namespace LetterQuest.Gameplay.Letters.Ui
{
    public class LetterSlotUi : MonoBehaviour
    {
        [SerializeField] private int index;
        public delegate void LetterSlotUpdate(int index);
        public event LetterSlotUpdate LetterSlotUpdateEvent;
        private Image _slotBorderImage;
        private TextMesh _textMesh;

        #region Unity Methods

        private void Awake()
        {
            _textMesh = GetComponentInChildren<TextMesh>();
            _slotBorderImage = GetComponent<Image>();
            ResetLetterSlotText();
        }

        #endregion
        
        #region Public Methods

        public bool IsAssigned => _textMesh.text != string.Empty;
        public char GetLetter() => IsAssigned ? _textMesh.text[^1] : char.MinValue;
        public void AssignSlotBorderColor(Color color) => _slotBorderImage.color = color;

        public void SetLetterSlotText(string text)
        {
            _textMesh.text = text;
            LetterSlotUpdateEvent?.Invoke(index);
        }

        public void ResetLetterSlotText()
        {
            _textMesh.text = string.Empty;
            AssignSlotBorderColor(Color.black);
        }

        #endregion
    }
}
