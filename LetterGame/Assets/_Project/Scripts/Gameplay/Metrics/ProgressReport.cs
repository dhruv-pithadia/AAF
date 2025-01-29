
using System.Collections.Generic;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics
{
    public class ProgressReport
    {
        private readonly ProgressReportData _data = new();

        #region Public Methods

        public ProgressReportData AnalyzeMetrics(in MetricsData[] metricsData)
        {
            _data.Clear();
            _data.MetricsCount = metricsData.Length;
            for (var i = 0; i < _data.MetricsCount; i++)
            {
                var metrics = metricsData[i];
                HandleFailedGrabs(metrics.NumFailedGrabs);
                HandlePerfectGrabs(metrics.NumPerfectGrabs);
                HandleInvalidLetters(metrics.NumInvalidLetters);
                HandleLetterTime(metrics.TimePerLetter);
                HandleWordTime(metrics.TimePerWord);
                HandleGrabTime(metrics.TimePerGrab);
            }

            CalculateAverages();
            return _data;
        }

        public void Clear()
        {
            _data.Clear();
        }

        #endregion

        #region Private Methods

        private void HandleFailedGrabs(int value)
        {
            _data.AvgFailedGrabs += value;
            if (value < _data.MinFailedGrabs) _data.MinFailedGrabs = value;
            if (value > _data.MaxFailedGrabs) _data.MaxFailedGrabs = value;
        }

        private void HandlePerfectGrabs(int value)
        {
            _data.AvgPerfectGrabs += value;
            if (value < _data.MinPerfectGrabs) _data.MinPerfectGrabs = value;
            if (value > _data.MaxPerfectGrabs) _data.MaxPerfectGrabs = value;
        }

        private void HandleInvalidLetters(int value)
        {
            _data.AvgInvalidLetters += value;
            if (value < _data.MinInvalidLetters) _data.MinInvalidLetters = value;
            if (value > _data.MaxInvalidLetters) _data.MaxInvalidLetters = value;
        }
        
        private void HandleLetterTime(List<float> data)
        {
            var count = data.Count;
            for (var i = 0; i < count; i++)
            {
                _data.AvgTimePerLetter += data[i];
            }

            _data.TimePerPlacementCount += count;
        }

        private void HandleWordTime(List<float> data)
        {
            var count = data.Count;
            for (var i = 0; i < count; i++)
            {
                _data.AvgTimePerWord += data[i];
            }

            _data.TimePerWordCount += count;
        }

        private void HandleGrabTime(List<float> data)
        {
            var count = data.Count;
            for (var i = 0; i < count; i++)
            {
                _data.AvgTimePerGrab += data[i];
            }

            _data.TimePerGrabCount += count;
        }

        private void CalculateAverages()
        {
            _data.AvgFailedGrabs /= _data.MetricsCount;
            _data.AvgPerfectGrabs /= _data.MetricsCount;
            _data.AvgInvalidLetters /= _data.MetricsCount;

            _data.AvgTimePerWord /= _data.TimePerWordCount;
            _data.AvgTimePerGrab /= _data.TimePerGrabCount;
            _data.AvgTimePerLetter /= _data.TimePerPlacementCount;
        }

        #endregion
    }
}
