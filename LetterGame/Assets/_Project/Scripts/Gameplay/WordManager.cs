
using TMPro;
using UnityEngine;
using LetterQuest.Framework.Audio;
using LetterQuest.Gameplay.Letters;
using LetterQuest.Gameplay.Words.Data;
using LetterQuest.Gameplay.Words.Ui;

namespace LetterQuest.Gameplay.Words.Manager
{
    public class WordManager : MonoBehaviour
    {
        [SerializeField] private int maxWordCount = 20;
        [SerializeField] private TMP_Text currentWord;
        [field: SerializeField] private LetterSlotHandler LetterSlotHandler { get; set; }
        [field: SerializeField] private ProgressBar ProgressBar { get; set; }
        private WordGenerator _wordGenerator;

        #region Unity Methods

        public void Start()
        {
            LetterSlotHandler.Initialize();
            ProgressBar.Initialize(new WordTrackingData(0, maxWordCount));
            LetterSlotHandler.WordCompleteEvent += OnWordComplete;
            _wordGenerator = new WordGenerator();
            _wordGenerator.SetWordDifficulty();
            AssignNextWord();
        }

        public void OnDisable()
        {
            LetterSlotHandler.Dispose();
            ProgressBar.Dispose();
            LetterSlotHandler.WordCompleteEvent -= OnWordComplete;
            if (ReferenceEquals(_wordGenerator, null)) return;
            _wordGenerator.Dispose();
            _wordGenerator = null;
        }

        #endregion

        #region Public Methods

        public void SkipWord()
        {
            PrepareNextWord();
        }

        #endregion

        #region Private Methods

        private void OnWordComplete()
        {
            AudioManager.Instance?.PlaySoundEffect(3);
            PrepareNextWord();
        }

        private void PrepareNextWord()
        {
            ProgressBar.WordCountTick();
            LetterSlotHandler.ResetAllSlots();
            AssignNextWord();
        }

        private void AssignNextWord()
        {
            currentWord.text = _wordGenerator.GetNextWord();
            if (currentWord.text == string.Empty)
            {
                IncreaseDifficulty();
            }
            else
            {
                LetterSlotHandler.OnWordUpdate(currentWord.text);
            }
        }

        private void IncreaseDifficulty()
        {
            if (_wordGenerator.GoToNextDifficulty())
            {
                AssignNextWord();
            }
            else
            {
                //  TODO: End Game
                Debug.Log("[WordManager]: The Game Is Done");
            }
        }

        #endregion
    }
}
