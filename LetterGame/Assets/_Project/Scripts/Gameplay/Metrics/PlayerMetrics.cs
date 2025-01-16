
using UnityEngine;

namespace LetterQuest.Gameplay.Metrics
{
    [CreateAssetMenu(fileName = "Player Metrics", menuName = "LetterQuest/Player Metrics")]
    public class PlayerMetrics : ScriptableObject
    {
        public SimpleMetrics SimpleMetrics { get; private set; }
        public AdvancedMetrics AdvancedMetrics { get; private set; }
        private float _startTime;

        #region Unity Methods

        private void OnEnable()
        {
            SimpleMetrics = new SimpleMetrics();
            AdvancedMetrics = new AdvancedMetrics();
        }

        private void OnDisable()
        {
            AdvancedMetrics.Dispose();
            SimpleMetrics.Dispose();
            AdvancedMetrics = null;
            SimpleMetrics = null;
        }

        #endregion

        #region Public Methods

        public void StartLevel()
        {
            SimpleMetrics.ClearData();
            AdvancedMetrics.ClearData();
            _startTime = Time.realtimeSinceStartup;
        }

        public void EndLevel()
        {
            SimpleMetrics.CalculatePlayDuration(_startTime);
        }

        #endregion
    }

    public static class MetricsData
    {
        public const string AvgWordTimeLabel = "Average time between words :\n";
        public const string AveGrabTimeLabel = "Average time between grab attempts :\n";
        public const string AvgLetterPlacementLabel = "Average time between letter placements :\n";

        public const string SecondsLabel = " seconds";
        public const string ColorStart = "<color=#00ff00ff>";
        public const string ColorEnd = "</color>";
    }
}
