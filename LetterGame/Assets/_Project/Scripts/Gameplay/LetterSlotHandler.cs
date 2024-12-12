
using UnityEngine;

namespace LetterQuest.Gameplay
{
    [System.Serializable]
    public class LetterSlotHandler
    {
        [SerializeField] private LetterSlot[] letterSlots;
        public delegate void WordComplete();
        public event WordComplete WordCompleteEvent;
        private char[] _wordToSpell;

        #region Public Methods

        public void Initialize()
        {
            for (var i = 0; i < letterSlots.Length; i++)
            {
                letterSlots[i].LetterSlotUpdateEvent += OnLetterSlotUpdate;
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < letterSlots.Length; i++)
            {
                letterSlots[i].LetterSlotUpdateEvent -= OnLetterSlotUpdate;
            }
        }

        public void OnWordUpdate(string word)
        {
            TurnOffAllSlots();
            ConvertAndAssignWord(word);
            TurnOnSlots(_wordToSpell.Length);
        }

        public void ResetAllSlots()
        {
            for (var i = 0; i < letterSlots.Length; i++)
            {
                if (letterSlots[i].IsAssigned == false) continue;
                letterSlots[i].ResetLetterSlotText();
            }
        }

        #endregion

        #region Private Methods

        private void OnLetterSlotUpdate()
        {
            if (CheckWordSpelling())
            {
                WordCompleteEvent?.Invoke();
            }
        }

        private void TurnOffAllSlots()
        {
            for (var i = 0; i < letterSlots.Length; i++)
            {
                letterSlots[i].gameObject.SetActive(false);
            }
        }

        private void TurnOnSlots(int count)
        {
            for (var i = 0; i < count; i++)
            {
                letterSlots[i].gameObject.SetActive(true);
            }
        }

        private void ConvertAndAssignWord(string word)
        {
            _wordToSpell = word.ToCharArray();

            for (var i = 0; i < _wordToSpell.Length; i++)
            {
                if (char.GetNumericValue(_wordToSpell[i]) >= 65) continue;
                _wordToSpell[i] = char.ToUpper(_wordToSpell[i]);
            }
        }

        private bool CheckWordSpelling()
        {
            for (var i = 0; i < _wordToSpell.Length; i++)
            {
                if (letterSlots[i].IsAssigned == false) return false;
                if (_wordToSpell[i] != letterSlots[i].GetLetter()) return false;
            }

            return true;
        }

        #endregion
    }
}
