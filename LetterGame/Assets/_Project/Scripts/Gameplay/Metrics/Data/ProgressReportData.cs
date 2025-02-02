
namespace LetterQuest.Gameplay.Metrics.Data
{
    public class ProgressReportData
    {
        public int MetricsCount;

        public int MinFailedGrabs;
        public float AvgFailedGrabs;
        public int MaxFailedGrabs;
        public AnalysisType FailedGrabAnalysis;

        public int MinPerfectGrabs;
        public float AvgPerfectGrabs;
        public int MaxPerfectGrabs;
        public AnalysisType PerfectGrabAnalysis;

        public int MinInvalidLetters;
        public float AvgInvalidLetters;
        public int MaxInvalidLetters;
        public AnalysisType InvalidLetterAnalysis;

        public float TimePerWordAvg;
        public int TimePerWordCount;
        public AnalysisType TimePerWordAnalysis;

        public float TimePerGrabAvg;
        public int TimePerGrabCount;
        public AnalysisType TimePerGrabAnalysis;

        public float TimePerLetterAvg;
        public int TimePerLetterCount;
        public AnalysisType TimePerLetterAnalysis;

        public void Clear()
        {
            MetricsCount = 0;
            TimePerWordCount = 0;
            TimePerGrabCount = 0;
            TimePerLetterCount = 0;

            AvgFailedGrabs = 0f;
            AvgPerfectGrabs = 0f;
            AvgInvalidLetters = 0f;
            TimePerWordAvg = 0f;
            TimePerGrabAvg = 0f;
            TimePerLetterAvg = 0f;

            MinFailedGrabs = int.MaxValue;
            MaxFailedGrabs = int.MinValue;
            MinPerfectGrabs = int.MaxValue;
            MaxPerfectGrabs = int.MinValue;
            MinInvalidLetters = int.MaxValue;
            MaxInvalidLetters = int.MinValue;

            FailedGrabAnalysis = AnalysisType.Stable;
            PerfectGrabAnalysis = AnalysisType.Stable;
            InvalidLetterAnalysis = AnalysisType.Stable;
            TimePerWordAnalysis = AnalysisType.Stable;
            TimePerGrabAnalysis = AnalysisType.Stable;
            TimePerLetterAnalysis = AnalysisType.Stable;
        }
    }

    public enum AnalysisType : byte
    {
        Stable = 0,
        Positive = 1,
        Negative = 2,
    }
}
