
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

        public float PlayDuration;
        public List<float> TimePerGrab = new();
        public List<float> TimePerWord = new();
        public List<float> TimePerLetter = new();

        public void SetData(MetricsData data)
        {
            Clear();
            PlayDuration = data.PlayDuration;
            NumBreaksTaken = data.NumBreaksTaken;
            NumFailedGrabs = data.NumFailedGrabs;
            NumPerfectGrabs = data.NumPerfectGrabs;
            NumInvalidLetters = data.NumInvalidLetters;
            NumLettersSkipped = data.NumLettersSkipped;

            for (var i = 0; i < data.TimePerWord.Count; i++)
            {
                TimePerWord.Add(data.TimePerWord[i]);
            }

            for (var i = 0; i < data.TimePerGrab.Count; i++)
            {
                TimePerGrab.Add(data.TimePerGrab[i]);
            }

            for (var i = 0; i < data.TimePerLetter.Count; i++)
            {
                TimePerLetter.Add(data.TimePerLetter[i]);
            }
        }

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

        public void Dispose()
        {
            Clear();
            TimePerWord = null;
            TimePerGrab = null;
            TimePerLetter = null;
        }
    }
}
