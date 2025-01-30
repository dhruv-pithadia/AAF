
using UnityEngine;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics
{
    public class MetricsHandler
    {
        private readonly MetricsData _data;

        public MetricsHandler(MetricsData data) => _data = data;

        public void IncrementBreaks() => _data.NumBreaksTaken++;
        public void IncrementSkips() => _data.NumLettersSkipped++;
        public void IncrementGrabFails() => _data.NumFailedGrabs++;
        public void IncrementPerfectGrabs() => _data.NumPerfectGrabs++;
        public void IncrementInvalidLetters() => _data.NumInvalidLetters++;

        public void AddGrabTime(float time) => _data.TimePerGrab.Add(CalculateElapsedTime(time));
        public void AddWordTime(float time) => _data.TimePerWord.Add(CalculateElapsedTime(time));
        public void AddLetterTime(float time) => _data.TimePerLetter.Add(CalculateElapsedTime(time));
        public void CalculatePlayDuration(float time) => _data.PlayDuration = CalculateElapsedTime(time);

        private static float CalculateElapsedTime(float time) => Time.realtimeSinceStartup - time;
    }
}
