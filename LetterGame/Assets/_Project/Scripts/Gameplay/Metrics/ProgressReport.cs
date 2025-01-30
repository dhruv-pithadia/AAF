
using System.Collections.Generic;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics
{
    public class ProgressReport
    {
        private readonly ProgressReportData _data = new();
        private readonly List<float> allInvalidLetters = new();
        private readonly List<float> allFailedGrabAverages = new();
        private readonly List<float> allPerfectGrabAverages = new();
        private readonly List<float> allTimePerWordAverages = new();
        private readonly List<float> allTimePerGrabAverages = new();
        private readonly List<float> allTimePerLetterAverages = new();
        private const float Offset = 0.5f;

        #region Public Methods

        public ProgressReportData AnalyzeMetrics(in MetricsData[] metricsData)
        {
            Clear();
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
            AnalyzeAll();
            return _data;
        }

        public void Clear()
        {
            _data.Clear();
            allInvalidLetters.Clear();
            allFailedGrabAverages.Clear();
            allPerfectGrabAverages.Clear();
            allTimePerWordAverages.Clear();
            allTimePerGrabAverages.Clear();
            allTimePerLetterAverages.Clear();
        }

        #endregion

        #region Private Methods

        private void HandleFailedGrabs(int value)
        {
            _data.AvgFailedGrabs += value;
            allFailedGrabAverages.Add(value);
            if (value < _data.MinFailedGrabs) _data.MinFailedGrabs = value;
            if (value > _data.MaxFailedGrabs) _data.MaxFailedGrabs = value;
        }

        private void HandlePerfectGrabs(int value)
        {
            _data.AvgPerfectGrabs += value;
            allPerfectGrabAverages.Add(value);
            if (value < _data.MinPerfectGrabs) _data.MinPerfectGrabs = value;
            if (value > _data.MaxPerfectGrabs) _data.MaxPerfectGrabs = value;
        }

        private void HandleInvalidLetters(int value)
        {
            allInvalidLetters.Add(value);
            _data.AvgInvalidLetters += value;
            if (value < _data.MinInvalidLetters) _data.MinInvalidLetters = value;
            if (value > _data.MaxInvalidLetters) _data.MaxInvalidLetters = value;
        }

        private void HandleLetterTime(List<float> data)
        {
            var count = data.Count;
            if (count == 0) return;

            var sumTimes = 0f;
            for (var i = 0; i < count; i++)
            {
                sumTimes += data[i];
            }

            _data.TimePerLetterCount += count;
            _data.TimePerLetterAvg += sumTimes;
            allTimePerLetterAverages.Add(sumTimes / count);
        }

        private void HandleWordTime(List<float> data)
        {
            var count = data.Count;
            if (count == 0) return;

            var sumTimes = 0f;
            for (var i = 0; i < count; i++)
            {
                sumTimes += data[i];
            }

            _data.TimePerWordCount += count;
            _data.TimePerWordAvg += sumTimes;
            allTimePerWordAverages.Add(sumTimes / count);
        }

        private void HandleGrabTime(List<float> data)
        {
            var count = data.Count;
            if (count == 0) return;

            var sumTimes = 0f;
            for (var i = 0; i < count; i++)
            {
                sumTimes += data[i];
            }

            _data.TimePerGrabCount += count;
            _data.TimePerGrabAvg += sumTimes;
            allTimePerGrabAverages.Add(sumTimes / count);
        }

        private void CalculateAverages()
        {
            _data.AvgFailedGrabs /= _data.MetricsCount;
            _data.AvgPerfectGrabs /= _data.MetricsCount;
            _data.AvgInvalidLetters /= _data.MetricsCount;

            if (_data.TimePerLetterCount > 0) _data.TimePerLetterAvg /= _data.TimePerLetterCount;
            if (_data.TimePerWordCount > 0) _data.TimePerWordAvg /= _data.TimePerWordCount;
            if (_data.TimePerGrabCount > 0) _data.TimePerGrabAvg /= _data.TimePerGrabCount;
        }

        private void AnalyzeAll()
        {
            var count = 0;
            var average = 0f;
            AnalyzeFailedGrabCount(count, average);
            AnalyzePerfectGrabCount(count, average);
            AnalyzeInvalidLetterCount(count, average);
            AnalyzeTimePerLetter(count, average);
            AnalyzeTimePerGrab(count, average);
            AnalyzeTimePerWord(count, average);
        }

        private void AnalyzeInvalidLetterCount(int count, float average)
        {
            average = 0f;
            count = allInvalidLetters.Count - 1;
            for (var i = 0; i < count; i++)
            {
                average += allInvalidLetters[i];
            }

            average /= count;
            if (allInvalidLetters[^1] > average + Offset) _data.InvalidLetterAnalysis = AnalysisType.Negative;
            else if (allInvalidLetters[^1] < average - Offset) _data.InvalidLetterAnalysis = AnalysisType.Positive;
        }

        private void AnalyzePerfectGrabCount(int count, float average)
        {
            average = 0f;
            count = allPerfectGrabAverages.Count - 1;
            for (var i = 0; i < count; i++)
            {
                average += allPerfectGrabAverages[i];
            }

            average /= count;
            if (allPerfectGrabAverages[^1] > average + Offset) _data.PerfectGrabAnalysis = AnalysisType.Positive;
            else if (allPerfectGrabAverages[^1] < average - Offset) _data.PerfectGrabAnalysis = AnalysisType.Negative;
        }

        private void AnalyzeFailedGrabCount(int count, float average)
        {
            average = 0f;
            count = allFailedGrabAverages.Count - 1;
            for (var i = 0; i < count; i++)
            {
                average += allFailedGrabAverages[i];
            }

            average /= count;
            if (allFailedGrabAverages[^1] > average + Offset) _data.FailedGrabAnalysis = AnalysisType.Negative;
            else if (allFailedGrabAverages[^1] < average - Offset) _data.FailedGrabAnalysis = AnalysisType.Positive;
        }

        private void AnalyzeTimePerLetter(int count, float average)
        {
            count = allTimePerLetterAverages.Count - 1;
            if (count <= 0) return;

            average = 0f;
            for (var i = 0; i < count; i++)
            {
                average += allTimePerLetterAverages[i];
            }

            average /= count;
            if (allTimePerLetterAverages[^1] > average + Offset) _data.TimePerLetterAnalysis = AnalysisType.Negative;
            else if (allTimePerLetterAverages[^1] < average - Offset)
                _data.TimePerLetterAnalysis = AnalysisType.Positive;
        }

        private void AnalyzeTimePerGrab(int count, float average)
        {
            count = allTimePerGrabAverages.Count - 1;
            if (count <= 0) return;

            average = 0f;
            for (var i = 0; i < count; i++)
            {
                average += allTimePerGrabAverages[i];
            }

            average /= count;
            if (allTimePerGrabAverages[^1] > average + Offset) _data.TimePerGrabAnalysis = AnalysisType.Negative;
            else if (allTimePerGrabAverages[^1] < average - Offset) _data.TimePerGrabAnalysis = AnalysisType.Positive;
        }

        private void AnalyzeTimePerWord(int count, float average)
        {
            count = allTimePerWordAverages.Count - 1;
            if (count <= 0) return;

            average = 0f;
            for (var i = 0; i < count; i++)
            {
                average += allTimePerWordAverages[i];
            }

            average /= count;
            if (allTimePerWordAverages[^1] > average + Offset) _data.TimePerWordAnalysis = AnalysisType.Negative;
            else if (allTimePerWordAverages[^1] < average - Offset) _data.TimePerWordAnalysis = AnalysisType.Positive;
        }

        #endregion
    }
}
