
using TMPro;
using System.Text;
using UnityEngine;
using LetterQuest.Gameplay.Ui;
using System.Collections.Generic;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics.Ui
{
    [System.Serializable]
    public class MetricsUi
    {
        [SerializeField] private TMP_Text lettersSkippedText;
        [SerializeField] private TMP_Text invalidLetterText;
        [SerializeField] private TMP_Text perfectGrabsText;
        [SerializeField] private TMP_Text breaksTakenText;
        [SerializeField] private TMP_Text failedGrabsText;

        [SerializeField] private TMP_Text playDurationText;
        [SerializeField] private TMP_Text avgWordTimeText;
        [SerializeField] private TMP_Text avgGrabTimeText;
        [SerializeField] private TMP_Text avgLetterTimeText;
        [SerializeField] private LineGraph lineGraph;
        private StringBuilder _stringBuilder = new();

        #region Public Methods

        public void SetData(in MetricsData data)
        {
            lettersSkippedText.text = GetNumberText(data.NumLettersSkipped);
            invalidLetterText.text = GetNumberText(data.NumInvalidLetters);
            perfectGrabsText.text = GetNumberText(data.NumPerfectGrabs);
            breaksTakenText.text = GetNumberText(data.NumBreaksTaken);
            failedGrabsText.text = GetNumberText(data.NumFailedGrabs);

            playDurationText.text = GetPlayDurationText(data.PlayDuration);
            avgWordTimeText.text = GetAverageText(Labels.WordTimeLabel, data.TimePerWord);
            avgGrabTimeText.text = GetAverageText(Labels.GrabTimeLabel, data.TimePerGrab);
            avgLetterTimeText.text = GetAverageText(Labels.LetterTimeLabel, data.TimePerLetter);

            CreateWordTimeGraph(data);
        }

        public void CreateGrabTimeGraph(in MetricsData data)
        {
            lineGraph.CreateGraph(data.TimePerGrab, data.PlayDuration.ToString("F0"), Labels.GrabGraphLabel);
        }

        public void CreateWordTimeGraph(in MetricsData data)
        {
            lineGraph.CreateGraph(data.TimePerWord, data.PlayDuration.ToString("F0"), Labels.WordGraphLabel);
        }

        public void CreateLetterTimeGraph(in MetricsData data)
        {
            lineGraph.CreateGraph(data.TimePerLetter, data.PlayDuration.ToString("F0"), Labels.LetterGraphLabel);
        }

        public void Dispose()
        {
            _stringBuilder.Clear();
            _stringBuilder = null;
        }

        #endregion

        #region Private Methods

        private string GetPlayDurationText(float playDuration)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(Labels.PositiveColorStart);
            _stringBuilder.Append(playDuration.ToString("F2"));
            _stringBuilder.Append(Labels.ColorEnd);
            _stringBuilder.Append(Labels.SecondsLabel);
            return _stringBuilder.ToString();
        }

        private string GetNumberText(int count)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(Labels.PositiveColorStart);
            _stringBuilder.Append(count);
            _stringBuilder.Append(Labels.ColorEnd);
            return _stringBuilder.ToString();
        }

        private string GetAverageText(string label, List<float> input)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.Append(Labels.PositiveColorStart);
            _stringBuilder.Append(CalculateAverage(input).ToString("F2"));
            _stringBuilder.Append(Labels.ColorEnd);
            _stringBuilder.Append(Labels.SecondsLabel);
            return _stringBuilder.ToString();
        }

        private static float CalculateAverage(List<float> input)
        {
            if (input.Count == 0) return 0;
            var totalSum = 0f;
            for (var i = 0; i < input.Count; i++) totalSum += input[i];
            return totalSum / input.Count;
        }

        #endregion
    }
}
