
using TMPro;
using UnityEngine;

namespace LetterQuest.Gameplay.Metrics
{
    public class PlayerMetricsViewer : MonoBehaviour
    {
        [SerializeField] private PlayerMetrics playerMetrics;
        
        [SerializeField] private TMP_Text breaksTakenText;
        [SerializeField] private TMP_Text lettersSkippedText;
        [SerializeField] private TMP_Text failedGrabsText;
        [SerializeField] private TMP_Text incorrectPlacementText;
        [SerializeField] private TMP_Text successfulAttemptsText;
        [SerializeField] private TMP_Text playDurationText;
        
        [SerializeField] private TMP_Text averageGrabTime;
        [SerializeField] private TMP_Text averageLetterPlacementTime;
        [SerializeField] private TMP_Text averageWordCompletionTime;
        
        private void Start()
        {
            AppendStatsText();
        }

        private void AppendStatsText()
        {
            var simpleMetrics = playerMetrics.SimpleMetrics;
            breaksTakenText.text = simpleMetrics.GetBreaksText();
            failedGrabsText.text = simpleMetrics.GetFailedGrabsText();
            playDurationText.text = simpleMetrics.GetPlayDurationText();
            lettersSkippedText.text = simpleMetrics.GetLettersSkippedText();
            successfulAttemptsText.text = simpleMetrics.GetSuccessfulGrabsText();
            incorrectPlacementText.text = simpleMetrics.GetInvalidPlacementText();

            var advancedMetrics = playerMetrics.AdvancedMetrics;
            averageGrabTime.text = advancedMetrics.GetAvgGrabRateText();
            averageWordCompletionTime.text = advancedMetrics.GetAvgWordRateText();
            averageLetterPlacementTime.text = advancedMetrics.GetAvgLetterRateText();
        }
    }
}
