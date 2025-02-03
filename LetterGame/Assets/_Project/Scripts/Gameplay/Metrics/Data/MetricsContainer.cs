
using UnityEngine;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics
{
    [CreateAssetMenu(fileName = "MetricsContainer", menuName = "LetterQuest/Metrics Container")]
    public class MetricsContainer : ScriptableObject
    {
        public bool HasData { get; private set; }
        public MetricsData Data { get; private set; }
        public MetricsHandler Handler { get; private set; }
        private float _startLevelTime;

        private void OnEnable()
        {
            HasData = false;
            Data = new MetricsData();
            Handler = new MetricsHandler(Data);
        }

        private void OnDisable()
        {
            Data.Dispose();
            HasData = false;
            Handler = null;
            Data = null;
        }

        public void SetData(MetricsData data)
        {
            Data.SetData(data);
        }

        public void ClearData()
        {
            HasData = false;
            Data.Clear();
        }

        public void StartLevel()
        {
            Data.Clear();
            HasData = false;
            _startLevelTime = Time.realtimeSinceStartup;
        }

        public void EndBreak(float breakDuration)
        {
            _startLevelTime += breakDuration;
        }

        public void EndLevel()
        {
            Handler.CalculatePlayDuration(_startLevelTime);
            HasData = true;
        }
    }
}
