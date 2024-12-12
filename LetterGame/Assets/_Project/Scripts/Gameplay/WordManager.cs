
using TMPro;
using UnityEngine;

namespace LetterQuest.Gameplay
{
    [System.Serializable]
    public class WordManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentWord;
        [field: SerializeField] private LetterSlotHandler letterSlotHandler;
        private WordGenerator _wordGenerator;

        #region Unity Methods

        public void Start()
        {
            letterSlotHandler.Initialize();
            letterSlotHandler.WordCompleteEvent += OnWordComplete;
            _wordGenerator = new WordGenerator();
            _wordGenerator.SetWordDifficulty(0);
            AssignNextWord();
        }

        public void OnDisable()
        {
            letterSlotHandler.Dispose();
            letterSlotHandler.WordCompleteEvent -= OnWordComplete;
            if (ReferenceEquals(_wordGenerator, null)) return;
            _wordGenerator.Dispose();
            _wordGenerator = null;
        }

        #endregion

        #region Public Methods

        public void SkipWord()
        {
            OnWordComplete();
        }

        #endregion

        #region Private Methods

        private void OnWordComplete()
        {
            letterSlotHandler.ResetAllSlots();
            AssignNextWord();
        }

        private void AssignNextWord()
        {
            currentWord.text = _wordGenerator.GetNextWord();
            if (currentWord.text == string.Empty)
            {
                Debug.LogError("[WordSlotManager]: No word found");
            }
            else
            {
                letterSlotHandler.OnWordUpdate(currentWord.text);
            }
        }

        #endregion
    }
}
