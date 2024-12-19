
using UnityEngine;
using LetterQuest.Framework.Audio;
using LetterQuest.Gameplay.Letters.Ui;

namespace LetterQuest.Gameplay.Letters
{
    [System.Serializable]
    public class LetterSlotHandler
    {
        [SerializeField] private LetterSlotUi[] letterSlots;
        public delegate void WordComplete();
        public event WordComplete WordCompleteEvent;
        private char[] _currentWord;

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
            AssignCurrentWord(word);
            TurnOnSlots(_currentWord.Length);
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

        private void AssignCurrentWord(string word) => _currentWord = word.ToCharArray();
        private bool DoesLetterMatchAtIndex(int i) => _currentWord[i] == letterSlots[i].GetLetter();

        private void OnLetterSlotUpdate(int index)
        {
            if (IsSpellingCorrect() == false)
            {
                AudioManager.Instance?.PlaySoundEffect(DoesLetterMatchAtIndex(index) ? 2 : 1);
            }
            else
            {
                AudioManager.Instance?.PlaySoundEffect(3);
                WordCompleteEvent?.Invoke();
            }
        }

        private bool IsSpellingCorrect()
        {
            var result = true;
            for (var i = 0; i < _currentWord.Length; i++)
            {
                if (DoesLetterMatchAtIndex(i))
                {
                    letterSlots[i].AssignSlotBorderColor(Color.green);
                }
                else
                {
                    result = false;
                    if (letterSlots[i].IsAssigned == false) continue;
                    letterSlots[i].AssignSlotBorderColor(Color.red);
                }
            }

            return result;
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

        #endregion
    }
}
