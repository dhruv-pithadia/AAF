
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
        private StringBuilder _stringBuilder = new();

        #region Public Methods

        public void SetData(in ProgressReportData data)
        {
            avgFailedGrabsText.text = ConvertToText(data.AvgFailedGrabs, Labels.AvgFailGrabLabel);
            minFailedGrabsText.text = ConvertToText(data.MinFailedGrabs, Labels.MinFailGrabLabel);
            maxFailedGrabsText.text = ConvertToText(data.MaxFailedGrabs, Labels.MaxFailGrabLabel);

            avgPerfectGrabsText.text = ConvertToText(data.AvgPerfectGrabs, Labels.AvgPerfectGrabLabel);
            minPerfectGrabsText.text = ConvertToText(data.MinPerfectGrabs, Labels.MinPerfectGrabLabel);
            maxPerfectGrabsText.text = ConvertToText(data.MaxPerfectGrabs, Labels.MaxPerfectGrabLabel);

            avgLettersText.text = ConvertToText(data.AvgInvalidLetters, Labels.AvgInvalidLetterLabel);
            minLettersText.text = ConvertToText(data.MinInvalidLetters, Labels.MinInvalidLetterLabel);
            maxLettersText.text = ConvertToText(data.MaxInvalidLetters, Labels.MaxInvalidLetterLabel);

            avgWordTimeText.text = ConvertToText(data.AvgTimePerWord, Labels.AvgWordTimeLabel);
            avgGrabTimeText.text = ConvertToText(data.AvgTimePerGrab, Labels.AvgGrabTimeLabel);
            avgLetterTimeText.text = ConvertToText(data.AvgTimePerLetter, Labels.AvgLetterTimeLabel);
        }

        public void Dispose()
        {
            _stringBuilder.Clear();
            _stringBuilder = null;
        }

        #endregion

        #region Private Methods

        private string ConvertToText(float value, string label)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.Append(Labels.ColorStart);
            _stringBuilder.Append(value);
            _stringBuilder.Append(Labels.ColorEnd);
            _stringBuilder.Append(Labels.SecondsLabel);
            return _stringBuilder.ToString();
        }

        private string ConvertToText(int value, string label)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(label);
            _stringBuilder.Append(Labels.ColorStart);
            _stringBuilder.Append(value);
            _stringBuilder.Append(Labels.ColorEnd);
            return _stringBuilder.ToString();
        }

        #endregion
    }
}
