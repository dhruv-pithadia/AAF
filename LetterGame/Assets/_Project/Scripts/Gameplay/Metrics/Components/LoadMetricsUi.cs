
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using LetterQuest.Gameplay.Settings;

namespace LetterQuest.Gameplay.Metrics.Ui
{
    [System.Serializable]
    public class LoadMetricsUi
    {
        [SerializeField] private Button loadMetricsButton;
        [SerializeField] private TMP_Dropdown saveFileDropdown;
        [SerializeField] private TMP_Dropdown difficultyDropdown;
        [SerializeField] private CanvasGroup loadMetricsCanvasGroup;
        [SerializeField] private CanvasGroup metricsCanvasGroup;
        [SerializeField] private GameDifficulty difficulty;

        public void Initialize()
        {
            difficultyDropdown.value = difficulty.Value;
            DisplayMetricsPanel();
        }

        public void DisplayLoadingPanel()
        {
            ToggleCanvasGroup(metricsCanvasGroup);
            ToggleCanvasGroup(loadMetricsCanvasGroup, true);
            loadMetricsButton.interactable = false;
        }

        public void DisplayMetricsPanel()
        {
            loadMetricsButton.interactable = true;
            ToggleCanvasGroup(metricsCanvasGroup, true);
            ToggleCanvasGroup(loadMetricsCanvasGroup);
        }

        public void SetSaveFileNames(List<string> saveFileNames)
        {
            saveFileDropdown.ClearOptions();
            saveFileDropdown.AddOptions(saveFileNames);
        }

        public int SaveFileIndex => saveFileDropdown.value;
        public string SaveFile => saveFileDropdown.options[SaveFileIndex].text;

        private static void ToggleCanvasGroup(CanvasGroup group, bool value = false)
        {
            group.alpha = value ? 1f : 0f;
            group.blocksRaycasts = value;
            group.interactable = value;
        }
    }
}
