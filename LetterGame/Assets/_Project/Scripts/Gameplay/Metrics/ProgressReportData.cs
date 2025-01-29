
namespace LetterQuest.Gameplay.Metrics.Data
{
    public class ProgressReportData
    {
        public float AvgFailedGrabs;
        public float AvgPerfectGrabs;
        public float AvgInvalidLetters;
        public float AvgTimePerWord;
        public float AvgTimePerGrab;
        public float AvgTimePerLetter;

        public int MinFailedGrabs;
        public int MaxFailedGrabs;
        public int MinPerfectGrabs;
        public int MaxPerfectGrabs;
        public int MinInvalidLetters;
        public int MaxInvalidLetters;
        public int TimePerPlacementCount;
        public int TimePerGrabCount;
        public int TimePerWordCount;
        public int MetricsCount;

        public void Clear()
        {
            MetricsCount = 0;
            TimePerWordCount = 0;
            TimePerGrabCount = 0;
            TimePerPlacementCount = 0;

            AvgFailedGrabs = 0f;
            AvgPerfectGrabs = 0f;
            AvgInvalidLetters = 0f;
            AvgTimePerWord = 0f;
            AvgTimePerGrab = 0f;
            AvgTimePerLetter = 0f;

            MinFailedGrabs = int.MaxValue;
            MaxFailedGrabs = int.MinValue;
            MinPerfectGrabs = int.MaxValue;
            MaxPerfectGrabs = int.MinValue;
            MinInvalidLetters = int.MaxValue;
            MaxInvalidLetters = int.MinValue;
        }
    }
}
