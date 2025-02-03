
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
        [SerializeField] private Button loadMetricsPanelButton;
        [SerializeField] private CanvasGroup metricsCanvasGroup;
        [SerializeField] private CanvasGroup lineGraphCanvasGroup;
        [SerializeField] private CanvasGroup progressReportCanvasGroup;
        [field: SerializeField] private ProgressReportUi progressReportUi;
        [field: SerializeField] private LoadMetricsUi loadMetricsUi;
        [field: SerializeField] private MetricsUi metricsUi;
        private MetricsDatabase _metricsDatabase;
        private ProgressReport _progressReport;
        private bool _reportOpened;

        #region Unity Methods
        
        private void Start()
        {
            userLogin.CheckLogin();
            loadMetricsUi.Initialize();
            metricsUi.SetData(metrics.Data);
            _progressReport = new ProgressReport();
            _metricsDatabase = new MetricsDatabase();

            if (metrics.HasData == false) return;
            progressReportButton.interactable = true;
        }

        private void OnDisable()
        {
            metricsUi.Dispose();
            DisposeProgressReport();
            _metricsDatabase = null;
            _progressReport = null;
        }
        
        #endregion
        
        #region Public Methods
        
        public void ShowGrabTimes() => metricsUi.CreateGrabTimeGraph(metrics.Data);
        public void ShowWordTimes() => metricsUi.CreateWordTimeGraph(metrics.Data);
        public void ShowLetterTimes() => metricsUi.CreateLetterTimeGraph(metrics.Data);
        public void ShowLoadingPanel() => loadMetricsUi.DisplayLoadingPanel();
        public void ShowMetricsPanel() => loadMetricsUi.DisplayMetricsPanel();

        public void LoadMetrics()
        {
            if (loadMetricsUi.SaveFileIndex == 0)
            {
                metrics.Data.Clear();
                metricsUi.SetData(metrics.Data);
                return;
            }
            
            var data = MetricsDatabase.Load(userLogin.GetSaveFolderName(), loadMetricsUi.SaveFile);
            if (ReferenceEquals(data, null)) return;
            
            metrics.SetData(data);
            metricsUi.SetData(data);
            progressReportButton.interactable = true;
        }

        public void SetupLoading()
        {
            var savePath = userLogin.GetSaveFolderName();
            var fileNames = MetricsDatabase.LoadAllNames(savePath);
            loadMetricsUi.SetSaveFileNames(fileNames);
        }

        public void CreateProgressReport()
        {
            if (_reportOpened) return;
            progressReportButton.interactable = false;
            if (_metricsDatabase.LoadAll(userLogin.GetSaveFolderName()) < 2) return;
            
            ShowProgressReportUi();
            progressReportUi.SetData(_progressReport.AnalyzeMetrics(_metricsDatabase.GetData()));
            loadMetricsPanelButton.interactable = false;
            _reportOpened = true;
        }

        public void DisposeProgressReport()
        {
            if (_reportOpened == false) return;
            
            HideProgressReportUi();
            loadMetricsPanelButton.interactable = true;
            progressReportButton.interactable = true;
            progressReportUi.Dispose();
            _metricsDatabase.Clear();
            _progressReport.Clear();
            _reportOpened = false;
        }
        
        #endregion
        
        #region Private Methods
        
        private void ShowProgressReportUi()
        {
            ToggleCanvasGroup(progressReportCanvasGroup, true);
            ToggleCanvasGroup(lineGraphCanvasGroup);
            ToggleCanvasGroup(metricsCanvasGroup);
        }
        
        private void HideProgressReportUi()
        {
            ToggleCanvasGroup(metricsCanvasGroup, true);
            ToggleCanvasGroup(lineGraphCanvasGroup, true);
            ToggleCanvasGroup(progressReportCanvasGroup);
        }
        
        private static void ToggleCanvasGroup(CanvasGroup group, bool value = false)
        {
            group.alpha = value ? 1f : 0f;
            group.blocksRaycasts = value;
            group.interactable = value;
        }
        
        #endregion
    }
}
