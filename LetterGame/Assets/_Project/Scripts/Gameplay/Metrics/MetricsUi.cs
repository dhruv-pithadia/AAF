
using TMPro;
using System.Text;
using UnityEngine;
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
        }

        public void Dispose()
        {
            _stringBuilder.Clear();
            _stringBuilder = null;
        }

        #endregion

        #region Private Methods

        private string GetPlayDurationText(int playDuration)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(Labels.ColorStart);
            _stringBuilder.Append(playDuration);
            _stringBuilder.Append(Labels.ColorEnd);
            _stringBuilder.Append(Labels.SecondsLabel);
            return _stringBuilder.ToString();
        }

        private string GetNumberText(int count)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(Labels.ColorStart);
            _stringBuilder.Append(count);
            _stringBuilder.Append(Labels.ColorEnd);
            return _stringBuilder.ToString();
        }

        private string GetAverageText(string label, List<float> input)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.Append(Labels.ColorStart);
            _stringBuilder.Append(CalculateAverage(input));
            _stringBuilder.Append(Labels.ColorEnd);
            _stringBuilder.Append(Labels.SecondsLabel);
            return _stringBuilder.ToString();
        }

        private static int CalculateAverage(List<float> input)
        {
            if (input.Count == 0) return 0;
            var totalSum = 0f;
            for (var i = 0; i < input.Count; i++) totalSum += input[i];
            return Mathf.RoundToInt(totalSum) / input.Count;
        }

        #endregion
    }

    public static class Labels
    {
        public const string WordTimeLabel = "Average time between words :\n";
        public const string GrabTimeLabel = "Average time between grab attempts :\n";
        public const string LetterTimeLabel = "Average time between letter placements :\n";

        public const string AvgFailGrabLabel = "Average Failed Grab Count : ";
        public const string MinFailGrabLabel = "Lowest Failed Grab Count : ";
        public const string MaxFailGrabLabel = "Highest Failed Grab Count : ";

        public const string AvgPerfectGrabLabel = "Average Perfect Grab Count : ";
        public const string MinPerfectGrabLabel = "Lowest Perfect Grab Count : ";
        public const string MaxPerfectGrabLabel = "Highest Perfect Grab Count : ";

        public const string AvgInvalidLetterLabel = "Average Invalid Letter Count : ";
        public const string MinInvalidLetterLabel = "Lowest Invalid Letter Count : ";
        public const string MaxInvalidLetterLabel = "Highest Invalid Letter Count : ";

        public const string AvgWordTimeLabel = "Average Time per Word : ";
        public const string AvgGrabTimeLabel = "Average Time per Grab : ";
        public const string AvgLetterTimeLabel = "Average Time per Letter : ";

        public const string SecondsLabel = " seconds";
        public const string ColorStart = "<b><color=#00ff00ff>";
        public const string ColorEnd = "</color></b>";
    }
}
