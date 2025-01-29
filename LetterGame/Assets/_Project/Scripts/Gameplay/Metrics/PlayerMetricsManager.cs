
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
            if (_saved) return;
            MetricsDatabase.Save(userLogin.GetSavePath(), metrics.Data);
            _saved = true;
        }

        public void CreateProgressReport()
        {
            if (_reportOpened) return;
            SaveCurrentMetrics();
            progressReportButton.enabled = false;
            if (_metricsDatabase.LoadAll(userLogin.GetFolderPath()) < 2) return;
            progressReportUi.SetData(_progressReport.AnalyzeMetrics(_metricsDatabase.GetData()));
            _reportOpened = true;
        }

        public void DisposeProgressReport()
        {
            if (_reportOpened == false) return;
            progressReportButton.enabled = true;
            progressReportUi.Dispose();
            _metricsDatabase.Clear();
            _progressReport.Clear();
            _reportOpened = false;
        }
    }
}
