
using UnityEngine;
using LetterQuest.Gameplay.Events;
using LetterQuest.Framework.Scenes;
using LetterQuest.Gameplay.Metrics;
using LetterQuest.Gameplay.Settings;
using LetterQuest.Gameplay.Words.Ui;
using LetterQuest.Gameplay.Words.Data;

namespace LetterQuest.Gameplay.Words.Manager
{
    public class WordManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameDifficulty gameDifficulty;
        [SerializeField] private PlayerMetrics playerMetrics;
        [SerializeField] private WordContainer wordContainer;
        [SerializeField] private EventBus eventBus;

        private const string GameOverText = "Game Over!";
        private WordsProgressBar _wordsProgressBar;
        private WordGenerator _wordGenerator;
        private const float Timer = 3f;
        private float _startTime;

        #region Unity Methods

        public void Start()
        {
            _wordsProgressBar = FindFirstObjectByType<WordsProgressBar>();
            _wordGenerator = new WordGenerator(gameDifficulty.Value);
            eventBus.WordCompleteEvent += OnWordComplete;
            playerMetrics.StartLevel();
            AssignNextWord();
        }

        public void OnDisable()
        {
            eventBus.WordCompleteEvent -= OnWordComplete;
            _wordGenerator.Dispose();
            _wordGenerator = null;
        }

        #endregion

        #region Public Methods

        public void SkipWord()
        {
            playerMetrics.SimpleMetrics.IncrementSkips();
            eventBus.OnWordReset();
            AssignNextWord();
        }

        #endregion

        #region Private Methods

        private void OnWordComplete()
        {
            eventBus.OnWordReset();
            wordContainer.PlayWordAnimation();
            playerMetrics.AdvancedMetrics.AddWordTime(_startTime);
            Invoke(nameof(AssignNextWord), Timer);
        }

        private void AssignNextWord()
        {
            if (_wordsProgressBar.GetProgress() >= 1f)
            {
                wordContainer.SetWord(GameOverText, true);
                Invoke(nameof(GameOver), Timer);
                return;
            }

            _startTime = Time.realtimeSinceStartup;
            wordContainer.SetWord(_wordGenerator.GetNextWord());
            eventBus.OnWordSet();
        }

        private void GameOver()
        {
            playerMetrics.EndLevel();
            GetComponent<SceneTransition>().LoadScene();
        }

        #endregion
    }
}
