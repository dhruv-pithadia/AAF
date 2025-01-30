
using TMPro;
using System.Text;
using UnityEngine;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics.Ui
{
    [System.Serializable]
    public class ProgressReportUi
    {
        [SerializeField] private TMP_Text avgFailedGrabsText;
        [SerializeField] private TMP_Text minFailedGrabsText;
        [SerializeField] private TMP_Text maxFailedGrabsText;

        [SerializeField] private TMP_Text avgPerfectGrabsText;
        [SerializeField] private TMP_Text minPerfectGrabsText;
        [SerializeField] private TMP_Text maxPerfectGrabsText;

        [SerializeField] private TMP_Text avgLettersText;
        [SerializeField] private TMP_Text minLettersText;
        [SerializeField] private TMP_Text maxLettersText;

        [SerializeField] private TMP_Text avgWordTimeText;
        [SerializeField] private TMP_Text avgGrabTimeText;
        [SerializeField] private TMP_Text avgLetterTimeText;

        [SerializeField] private TMP_Text failedGrabAnalysisText;
        [SerializeField] private TMP_Text perfectGrabAnalysisText;
        [SerializeField] private TMP_Text incorrectLetterAnalysisText;
        [SerializeField] private TMP_Text wordTimeAnalysisText;
        [SerializeField] private TMP_Text grabTimeAnalysisText;
        [SerializeField] private TMP_Text letterTimeAnalysisText;
        private StringBuilder _stringBuilder;

        #region Public Methods

        public void SetData(in ProgressReportData data)
        {
            _stringBuilder = new StringBuilder();
            minFailedGrabsText.text = ConvertToText(data.MinFailedGrabs, Labels.MinFailGrabLabel);
            avgFailedGrabsText.text = ConvertToText(data.AvgFailedGrabs, Labels.AvgFailGrabLabel);
            maxFailedGrabsText.text = ConvertToText(data.MaxFailedGrabs, Labels.MaxFailGrabLabel);

            minPerfectGrabsText.text = ConvertToText(data.MinPerfectGrabs, Labels.MinPerfectGrabLabel);
            avgPerfectGrabsText.text = ConvertToText(data.AvgPerfectGrabs, Labels.AvgPerfectGrabLabel);
            maxPerfectGrabsText.text = ConvertToText(data.MaxPerfectGrabs, Labels.MaxPerfectGrabLabel);

            minLettersText.text = ConvertToText(data.MinInvalidLetters, Labels.MinInvalidLetterLabel);
            avgLettersText.text = ConvertToText(data.AvgInvalidLetters, Labels.AvgInvalidLetterLabel);
            maxLettersText.text = ConvertToText(data.MaxInvalidLetters, Labels.MaxInvalidLetterLabel);

            avgWordTimeText.text = ConvertToText(data.TimePerWordAvg, Labels.AvgWordTimeLabel, true);
            avgGrabTimeText.text = ConvertToText(data.TimePerGrabAvg, Labels.AvgGrabTimeLabel, true);
            avgLetterTimeText.text = ConvertToText(data.TimePerLetterAvg, Labels.AvgLetterTimeLabel, true);

            failedGrabAnalysisText.text = FailedGrabs(data.FailedGrabAnalysis);
            perfectGrabAnalysisText.text = PerfectGrabs(data.PerfectGrabAnalysis);
            incorrectLetterAnalysisText.text = InvalidLetters(data.InvalidLetterAnalysis);
            wordTimeAnalysisText.text = AvgWordTime(data.TimePerWordAnalysis);
            grabTimeAnalysisText.text = AvgGrabTime(data.TimePerGrabAnalysis);
            letterTimeAnalysisText.text = AvgLetterTime(data.TimePerLetterAnalysis);
        }

        public void Dispose()
        {
            _stringBuilder.Clear();
            _stringBuilder = null;
        }

        #endregion

        #region Private Methods

        private string ConvertToText(float value, string label, bool seconds = false)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.Append(Labels.PositiveColorStart);
            _stringBuilder.Append(value.ToString("F2"));
            _stringBuilder.Append(Labels.ColorEnd);
            if (seconds) _stringBuilder.Append(Labels.SecondsLabel);
            return _stringBuilder.ToString();
        }

        private string ConvertToText(int value, string label)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.Append(Labels.PositiveColorStart);
            _stringBuilder.Append(value);
            _stringBuilder.Append(Labels.ColorEnd);
            return _stringBuilder.ToString();
        }

        private string FailedGrabs(AnalysisType value)
        {
            return value switch
            {
                AnalysisType.Stable => ConvertToText(value, Labels.FailGrabStableLabel),
                AnalysisType.Positive => ConvertToText(value, Labels.FailGrabPositiveLabel),
                AnalysisType.Negative => ConvertToText(value, Labels.FailGrabNegativeLabel),
                _ => string.Empty
            };
        }

        private string PerfectGrabs(AnalysisType value)
        {
            return value switch
            {
                AnalysisType.Stable => ConvertToText(value, Labels.PerfectGrabStableLabel),
                AnalysisType.Positive => ConvertToText(value, Labels.PerfectGrabPositiveLabel),
                AnalysisType.Negative => ConvertToText(value, Labels.PerfectGrabNegativeLabel),
                _ => string.Empty
            };
        }

        private string InvalidLetters(AnalysisType value)
        {
            return value switch
            {
                AnalysisType.Stable => ConvertToText(value, Labels.InvalidLetterStableLabel),
                AnalysisType.Positive => ConvertToText(value, Labels.InvalidLetterPositiveLabel),
                AnalysisType.Negative => ConvertToText(value, Labels.InvalidLetterNegativeLabel),
                _ => string.Empty
            };
        }

        private string AvgWordTime(AnalysisType value)
        {
            return value switch
            {
                AnalysisType.Stable => ConvertToText(value, Labels.WordTimeStableLabel),
                AnalysisType.Positive => ConvertToText(value, Labels.WordTimePositiveLabel),
                AnalysisType.Negative => ConvertToText(value, Labels.WordTimeNegativeLabel),
                _ => string.Empty
            };
        }

        private string AvgGrabTime(AnalysisType value)
        {
            return value switch
            {
                AnalysisType.Stable => ConvertToText(value, Labels.GrabTimeStableLabel),
                AnalysisType.Positive => ConvertToText(value, Labels.GrabTimePositiveLabel),
                AnalysisType.Negative => ConvertToText(value, Labels.GrabTimeNegativeLabel),
                _ => string.Empty
            };
        }

        private string AvgLetterTime(AnalysisType value)
        {
            return value switch
            {
                AnalysisType.Stable => ConvertToText(value, Labels.LetterTimeStableLabel),
                AnalysisType.Positive => ConvertToText(value, Labels.LetterTimePositiveLabel),
                AnalysisType.Negative => ConvertToText(value, Labels.LetterTimeNegativeLabel),
                _ => string.Empty
            };
        }

        private string ConvertToText(AnalysisType value, string label)
        {
            _stringBuilder.Clear();
            switch (value)
            {
                case AnalysisType.Positive:
                    _stringBuilder.Append(Labels.PositiveColorStart);
                    break;
                case AnalysisType.Negative:
                    _stringBuilder.Append(Labels.NegativeColorStart);
                    break;
            }

            _stringBuilder.Append(label);
            if (value != AnalysisType.Stable) _stringBuilder.Append(Labels.ColorEnd);
            return _stringBuilder.ToString();
        }

        #endregion
    }
}
