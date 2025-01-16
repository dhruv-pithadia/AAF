
using TMPro;
using UnityEngine;
using LetterQuest.Gameplay.Data;
using LetterQuest.Gameplay.Events;
using UnityEngine.SceneManagement;
using LetterQuest.Gameplay.Metrics;
using LetterQuest.Gameplay.Words.Ui;
using LetterQuest.Gameplay.Animation;
using LetterQuest.Gameplay.Words.Data;

namespace LetterQuest.Gameplay.Words.Manager
{
    public class WordManager : MonoBehaviour
    {
        [SerializeField] private int maxWordCount = 20;

        [Header("References")]
        [SerializeField] private TMP_Text currentWordText;
        [SerializeField] private AnimatorHook animatorHook;
        [SerializeField] private GameDifficulty gameDifficulty;
        [SerializeField] private PlayerMetrics playerMetrics;
        [SerializeField] private EventBus eventBus;
        [field: SerializeField] private WordsProgressBar WordsProgressBar { get; set; }

        private const string MetricsSceneName = "PlayerMetrics";
        private const string GameOverText = "Game Over!";
        private WordGenerator _wordGenerator;
        private const float Timer = 3f;
        private float _startTime;

        #region Unity Methods

        public void Start()
        {
            _wordGenerator = new WordGenerator(gameDifficulty.Value);
            WordsProgressBar.Initialize(new WordTrackingData(0, maxWordCount));
            eventBus.WordCompleteEvent += OnWordComplete;
            playerMetrics.StartLevel();
            AssignNextWord();
        }

        public void OnDisable()
        {
            eventBus.WordCompleteEvent -= OnWordComplete;
            WordsProgressBar.Dispose();
            if (ReferenceEquals(_wordGenerator, null)) return;
            _wordGenerator.Dispose();
            _wordGenerator = null;
        }

        #endregion

        #region Public Methods

        public void SkipWord()
        {
            playerMetrics.SimpleMetrics.IncrementSkips();
            WordsProgressBar.WordCountTick();
            eventBus.OnWordReset();
            AssignNextWord();
        }

        #endregion

        #region Private Methods

        private void OnWordComplete()
        {
            animatorHook.Play();
            eventBus.OnWordReset();
            WordsProgressBar.WordCountTick();
            playerMetrics.AdvancedMetrics.AddWordTime(_startTime);
            Invoke(nameof(AssignNextWord), Timer);
        }

        private void AssignNextWord()
        {
            _startTime = Time.realtimeSinceStartup;
            if (WordsProgressBar.GetProgress() >= 1f)
            {
                currentWordText.text = GameOverText;
                Invoke(nameof(EndLevel), Timer);
            }
            else
            {
                currentWordText.text = _wordGenerator.GetNextWord();
                eventBus.OnWordSet(currentWordText.text.ToUpper());
            }
        }

        private void EndLevel()
        {
            playerMetrics.EndLevel();
            SceneManager.LoadScene(MetricsSceneName, LoadSceneMode.Single);
        }

        #endregion
    }
}
