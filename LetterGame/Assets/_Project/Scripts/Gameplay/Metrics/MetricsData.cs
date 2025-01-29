
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Metrics.Data
{
    public class MetricsData
    {
        public int NumInvalidLetters;
        public int NumLettersSkipped;
        public int NumPerfectGrabs;
        public int NumFailedGrabs;
        public int NumBreaksTaken;
        public int PlayDuration;

        public readonly List<float> TimePerGrab = new();
        public readonly List<float> TimePerWord = new();
        public readonly List<float> TimePerLetter = new();

        public void Clear()
        {
            PlayDuration = 0;
            NumFailedGrabs = 0;
            NumBreaksTaken = 0;
            NumPerfectGrabs = 0;
            NumLettersSkipped = 0;
            NumInvalidLetters = 0;
            TimePerLetter.Clear();
            TimePerGrab.Clear();
            TimePerWord.Clear();
        }
    }
}
