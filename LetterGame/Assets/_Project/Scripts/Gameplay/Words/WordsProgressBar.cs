
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LetterQuest.Gameplay.Words.Data;

namespace LetterQuest.Gameplay.Words.Ui
{
    [System.Serializable]
    public class WordsProgressBar
    {
        [SerializeField] private TMP_Text currentWordCountText;
        [SerializeField] private Image progressBar;
        private WordTrackingData _data;

        #region Public Methods

        public void Initialize(WordTrackingData data)
        {
            _data = data;
            OnDataUpdate();
        }

        public void Dispose()
        {
            _data = null;
        }

        public void WordCountTick()
        {
            _data.Tick();
            OnDataUpdate();
        }

        public float GetProgress() => _data.GetPercent;

        #endregion

        #region Private Methods

        private void OnDataUpdate()
        {
            progressBar.fillAmount = _data.GetPercent;
            currentWordCountText.text = _data.GetText();
        }

        #endregion
    }
}
