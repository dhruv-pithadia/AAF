
using System.Text;
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Metrics
{
    public class AdvancedMetrics
    {
        public List<float> TimePerGrab { get; private set; } = new();
        public List<float> TimePerLetterPlacement { get; private set; } = new();
        public List<float> TimePerWord { get; private set; } = new();

        private StringBuilder _stringBuilder = new();

        #region Public Methods

        public void AddGrabTime(float time) => TimePerGrab.Add(CalculateElapsedTime(time));
        public void AddWordTime(float time) => TimePerWord.Add(CalculateElapsedTime(time));
        public void AddLetterTime(float time) => TimePerLetterPlacement.Add(CalculateElapsedTime(time));

        public string GetAvgWordRateText() => BuildText(MetricsData.AvgWordTimeLabel, TimePerWord);
        public string GetAvgGrabRateText() => BuildText(MetricsData.AveGrabTimeLabel, TimePerGrab);
        public string GetAvgLetterRateText() => BuildText(MetricsData.AvgLetterPlacementLabel, TimePerLetterPlacement);

        public void ClearData()
        {
            TimePerGrab.Clear();
            TimePerWord.Clear();
            TimePerLetterPlacement.Clear();
        }

        public void Dispose()
        {
            ClearData();
            _stringBuilder.Clear();
            _stringBuilder = null;
        }

        #endregion

        #region Private Methods
        
        private string BuildText(string label, List<float> input)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.Append(MetricsData.ColorStart);
            _stringBuilder.Append(CalculateAverage(input).ToString());
            _stringBuilder.Append(MetricsData.ColorEnd);
            _stringBuilder.Append(MetricsData.SecondsLabel);
            return _stringBuilder.ToString();
        }

        private static float CalculateElapsedTime(float startTime) => Time.realtimeSinceStartup - startTime;

        private static int CalculateAverage(List<float> input)
        {
            if (input.Count == 0) return 0;

            var totalSum = 0f;
            for (var i = 0; i < input.Count; i++) totalSum += input[i];
            return Mathf.RoundToInt(totalSum) / input.Count;
        }

        #endregion
    }
}
