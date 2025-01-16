
using System.Text;
using UnityEngine;
using System.Globalization;

namespace LetterQuest.Gameplay.Metrics
{
    public class SimpleMetrics
    {
        private StringBuilder _stringBuilder = new();
        private int _numInvalidPlacements;
        private int _numSuccessfulGrabs;
        private int _numFailedGrabs;
        private int _numLettersSkipped;
        private int _numBreaksTaken;
        private int _playDuration;

        #region Public Methods

        public void IncrementBreaks() => _numBreaksTaken++;
        public void IncrementSkips() => _numLettersSkipped++;
        public void IncrementGrabFails() => _numFailedGrabs++;
        public void IncrementSuccessfulGrabs() => _numSuccessfulGrabs++;
        public void IncrementInvalidPlacements() => _numInvalidPlacements++;
        public void CalculatePlayDuration(float start) => _playDuration = CalculateDifference(start);

        public string GetBreaksText() => AppendNumber(_numBreaksTaken);
        public string GetLettersSkippedText() => AppendNumber(_numLettersSkipped);
        public string GetFailedGrabsText() => AppendNumber(_numFailedGrabs);
        public string GetInvalidPlacementText() => AppendNumber(_numInvalidPlacements);
        public string GetSuccessfulGrabsText() => AppendNumber(_numSuccessfulGrabs);

        public string GetPlayDurationText()
        {
            AppendText(MetricsData.ColorStart, true);
            AppendText(_playDuration.ToString(CultureInfo.InvariantCulture));
            AppendText(MetricsData.ColorEnd);
            return AppendAndReturnText(MetricsData.SecondsLabel);
        }

        public void ClearData()
        {
            _playDuration = 0;
            _numBreaksTaken = 0;
            _numLettersSkipped = 0;
            _numFailedGrabs = 0;
            _numSuccessfulGrabs = 0;
            _numInvalidPlacements = 0;
        }

        public void Dispose()
        {
            ClearData();
            _stringBuilder.Clear();
            _stringBuilder = null;
        }

        #endregion

        #region Private Methods

        private string AppendNumber(int count)
        {
            AppendText(MetricsData.ColorStart, true);
            AppendText(count.ToString());
            return AppendAndReturnText(MetricsData.ColorEnd);
        }

        private void AppendText(string text, bool clear = false)
        {
            if (clear) _stringBuilder.Clear();
            _stringBuilder.Append(text);
        }

        private string AppendAndReturnText(string text)
        {
            _stringBuilder.Append(text);
            return _stringBuilder.ToString();
        }

        private static int CalculateDifference(float start) => Mathf.FloorToInt(Time.realtimeSinceStartup - start);

        #endregion
    }
}
