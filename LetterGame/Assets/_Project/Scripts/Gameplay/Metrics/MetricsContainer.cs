
using UnityEngine;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics
{
    [CreateAssetMenu(fileName = "MetricsContainer", menuName = "LetterQuest/Metrics Container")]
    public class MetricsContainer : ScriptableObject
    {
        public MetricsData Data { get; private set; }
        public MetricsHandler Handler { get; private set; }
        private float _startLevelTime;

        private void OnEnable()
        {
            Data = new MetricsData();
            Handler = new MetricsHandler(Data);
        }

        private void OnDisable()
        {
            Handler = null;
            Data = null;
        }

        public void StartLevel()
        {
            Data.Clear();
            _startLevelTime = Time.realtimeSinceStartup;
        }

        public void EndBreak(float breakDuration)
        {
            _startLevelTime += breakDuration;
        }

        public void EndLevel()
        {
            Handler.CalculatePlayDuration(_startLevelTime);
        }
    }
}
