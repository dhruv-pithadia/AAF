
using UnityEngine;
using UnityEngine.UI;
using LetterQuest.Core.Login;
using LetterQuest.Gameplay.Metrics.Ui;
using LetterQuest.Gameplay.Metrics.Database;

namespace LetterQuest.Gameplay.Metrics.Manager
{
    public class PlayerMetricsManager : MonoBehaviour
    {
        [SerializeField] private UserLogin userLogin;
        [SerializeField] private MetricsContainer metrics;
        [SerializeField] private Button progressReportButton;
        [SerializeField] private CanvasGroup metricsCanvasGroup;
        [SerializeField] private CanvasGroup lineGraphCanvasGroup;
        [SerializeField] private CanvasGroup progressReportCanvasGroup;
        [field: SerializeField] private ProgressReportUi progressReportUi;
        [field: SerializeField] private MetricsUi metricsUi;
        private MetricsDatabase _metricsDatabase;
        private ProgressReport _progressReport;
        private bool _reportOpened;
        private bool _saved;

        private void Start()
        {
            userLogin.Initialize();
            metricsUi.SetData(metrics.Data);
            _progressReport = new ProgressReport();
            _metricsDatabase = new MetricsDatabase();
        }

        private void OnDisable()
        {
            userLogin.Dispose();
            metricsUi.Dispose();
            DisposeProgressReport();
            _metricsDatabase = null;
            _progressReport = null;
        }

        public void SaveCurrentMetrics()
        {
            if (_saved || metrics.HasData == false) return;
            MetricsDatabase.Save(userLogin.GetSavePath(), metrics.Data);
            _saved = true;
        }

        public void LoadLastSavedMetrics()
        {
            if (_reportOpened) return;
            if (_metricsDatabase.LoadAll(userLogin.GetFolderPath()) == 0) return;
            var data = _metricsDatabase.GetFirstLoadedData();
            if (ReferenceEquals(data, null)) return;
            metrics.SetData(data);
            metricsUi.SetData(data);
        }
        
        public void ShowGrabTimes() => metricsUi.CreateGrabTimeGraph(metrics.Data);
        public void ShowWordTimes() => metricsUi.CreateWordTimeGraph(metrics.Data);
        public void ShowLetterTimes() => metricsUi.CreateLetterTimeGraph(metrics.Data);

        public void CreateProgressReport()
        {
            if (_reportOpened) return;
            SaveCurrentMetrics();
            progressReportButton.enabled = false;
            if (_metricsDatabase.LoadAll(userLogin.GetFolderPath()) < 2) return;
            TurnOffCanvas(metricsCanvasGroup);
            TurnOffCanvas(lineGraphCanvasGroup);
            TurnOnCanvas(progressReportCanvasGroup);
            progressReportUi.SetData(_progressReport.AnalyzeMetrics(_metricsDatabase.GetData()));
            _reportOpened = true;
        }

        public void DisposeProgressReport()
        {
            if (_reportOpened == false) return;
            TurnOnCanvas(metricsCanvasGroup);
            TurnOnCanvas(lineGraphCanvasGroup);
            TurnOffCanvas(progressReportCanvasGroup);
            progressReportButton.enabled = true;
            progressReportUi.Dispose();
            _metricsDatabase.Clear();
            _progressReport.Clear();
            _reportOpened = false;
        }

        private static void TurnOnCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        private static void TurnOffCanvas(CanvasGroup canvasGroup)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
