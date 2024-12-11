
using TMPro;
using UnityEngine;

namespace LetterQuest.Gameplay
{
    public class WordManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentWord;
        private WordGenerator _wordGenerator;

        #region Unity Methods

        private void Start()
        {
            _wordGenerator = new WordGenerator();
            _wordGenerator.SetWordDifficulty(0);
            AssignNextWord();
        }

        private void OnDestroy()
        {
            if (ReferenceEquals(_wordGenerator, null)) return;
            _wordGenerator.Dispose();
            _wordGenerator = null;
        }

        #endregion

        #region Public Methods

        public void SkipWord()
        {
            AssignNextWord();
        }

        #endregion

        #region Private Methods

        private void AssignNextWord()
        {
            currentWord.text = _wordGenerator.GetNextWord();
            if (currentWord.text == string.Empty)
            {
                Debug.LogError("[WordSlotManager]: No word found");
            }
        }

        #endregion
    }
}
