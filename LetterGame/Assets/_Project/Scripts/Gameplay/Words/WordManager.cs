﻿
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
        [SerializeField] private WordContainer wordContainer;
        [SerializeField] private MetricsContainer metrics;
        [SerializeField] private EventBus eventBus;

        private const string GameOverText = "Game Over!";
        private WordsProgressBar _wordsProgressBar;
        private WordGenerator _wordGenerator;
        private const float Timer = 3f;
        private float _startTime;
        private float _breakTime;
        private bool _allowSkip;

        #region Unity Methods

        public void Start()
        {
            _wordsProgressBar = FindFirstObjectByType<WordsProgressBar>();
            _wordGenerator = new WordGenerator(gameDifficulty.Value);
            eventBus.WordCompleteEvent += OnWordComplete;
            eventBus.BreakEvent += OnBreak;
            metrics.StartLevel();
            AssignNextWord();
        }

        private void OnBreak(bool toggle)
        {
            if (toggle) Pause();
            else Resume();
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
            if (_allowSkip == false) return;
            metrics.Handler.IncrementSkips();
            eventBus.OnWordReset();
            AssignNextWord();
        }

        #endregion

        #region Private Methods
        
        private void Pause()
        {
            _allowSkip = false;
            _breakTime = Time.realtimeSinceStartup;
            metrics.Handler.IncrementBreaks();
        }

        private void Resume()
        {
            var breakDuration = Time.realtimeSinceStartup - _breakTime;
            metrics.EndBreak(breakDuration);
            _startTime += breakDuration;
            _allowSkip = true;
        }

        private void OnWordComplete()
        {
            _allowSkip = false;
            eventBus.OnWordReset();
            wordContainer.PlayWordAnimation();
            metrics.Handler.AddWordTime(_startTime);
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
            _allowSkip = true;
        }

        private void GameOver()
        {
            metrics.EndLevel();
            GetComponent<SceneTransition>().LoadScene();
        }

        #endregion
    }
}
