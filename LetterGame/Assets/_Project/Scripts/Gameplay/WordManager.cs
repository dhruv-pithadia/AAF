
using TMPro;
using UnityEngine;
using LetterQuest.Gameplay.Data;
using LetterQuest.Gameplay.Letters;
using LetterQuest.Gameplay.Words.Ui;
using LetterQuest.Gameplay.Words.Data;

namespace LetterQuest.Gameplay.Words.Manager
{
    public class WordManager : MonoBehaviour
    {
        [SerializeField] private int maxWordCount = 20;
        [SerializeField] private TMP_Text currentWord;
        [SerializeField] private CurrentWordData currentWordData;
        [SerializeField] private GameDifficulty gameDifficulty;
        [field: SerializeField] private LetterSlotHandler LetterSlotHandler { get; set; }
        [field: SerializeField] private ProgressBar ProgressBar { get; set; }
        private WordGenerator _wordGenerator;

        #region Unity Methods

        public void Start()
        {
            LetterSlotHandler.Initialize();
            LetterSlotHandler.WordCompleteEvent += OnWordComplete;
            ProgressBar.Initialize(new WordTrackingData(0, maxWordCount));
            _wordGenerator = new WordGenerator(gameDifficulty.Value);
            AssignNextWord();
        }

        public void OnDisable()
        {
            LetterSlotHandler.WordCompleteEvent -= OnWordComplete;
            LetterSlotHandler.Dispose();
            ProgressBar.Dispose();
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
            ProgressBar.WordCountTick();
            LetterSlotHandler.ResetAllSlots();
            AssignNextWord();
        }

        private void AssignNextWord()
        {
            currentWordData.SetCurrentWord(_wordGenerator.GetNextWord());

            if (currentWordData.GetText() == string.Empty)
            {
                //  TODO: Go to Player Metrics Screen
                Debug.Log("[WordManager]: level ends here");
            }
            else
            {
                currentWord.text = currentWordData.GetText();
                LetterSlotHandler.OnWordUpdate(currentWordData.GetTextUpperCase());
            }
        }

        #endregion
    }
}
