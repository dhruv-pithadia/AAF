
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LetterQuest.Gameplay.Words.Ui
{
    public class WordsProgressBar : MonoBehaviour
    {
        [SerializeField] private int maxWordCount = 20;
        [SerializeField] private TMP_Text currentWordCountText;
        [SerializeField] private Image progressBar;
        private int _currentWordCount = -1;

        #region Public Methods

        public float GetProgress()
        {
            OnDataUpdate();
            return GetPercent;
        }

        #endregion

        #region Private Methods

        private string GetText() => $"{_currentWordCount} / {maxWordCount}";
        private float GetPercent => _currentWordCount / (float)maxWordCount;

        private void OnDataUpdate()
        {
            _currentWordCount++;
            progressBar.fillAmount = GetPercent;
            currentWordCountText.text = GetText();
        }

        #endregion
    }
}
