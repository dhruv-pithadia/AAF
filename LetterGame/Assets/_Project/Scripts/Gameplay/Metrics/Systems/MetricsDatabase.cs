
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics.Database
{
    public class MetricsDatabase
    {
        private readonly List<MetricsData> loadedData = new();

        #region Public Methods

        public MetricsData[] GetData() => loadedData.ToArray();

        public int LoadAll(string folderPath, int maxLoadCount = 16)
        {
            if (Directory.Exists(folderPath) == false) return 0;
            var directoryInfo = new DirectoryInfo(folderPath);

            var count = 0;
            var files = directoryInfo.GetFiles();
            for (var i = 0; i < files.Length; i++)
            {
                if (count == maxLoadCount) break;
                if (TryLoadMetrics(files[i].FullName) == false) continue;
                count++;
            }

            return count;
        }

        public void Clear()
        {
            loadedData.Clear();
        }

        public static void Save(string savePath, MetricsData data)
        {
            if (string.IsNullOrEmpty(savePath)) return;
            File.WriteAllText(savePath, JsonUtility.ToJson(data, true));
        }

        public static MetricsData Load(string folder, string file)
        {
            var fileName = Path.Combine(folder, file);
            return string.IsNullOrEmpty(fileName) ? null : LoadMetricsFromDatabase(fileName);
        }

        public static List<string> LoadAllNames(string folderPath)
        {
            var fileNames = new List<string> { "Empty" };
            if (Directory.Exists(folderPath) == false) return fileNames;
            var directoryInfo = new DirectoryInfo(folderPath);

            var files = directoryInfo.GetFiles();
            for (var i = 0; i < files.Length; i++)
            {
                fileNames.Add(files[i].Name);
            }

            return fileNames;
        }

        #endregion

        #region Private Methods

        private bool TryLoadMetrics(string filePath) => AddLoadedMetrics(LoadMetricsFromDatabase(filePath));

        private bool AddLoadedMetrics(MetricsData data)
        {
            if (ReferenceEquals(data, null) || loadedData.Contains(data)) return false;
            loadedData.Add(data);
            return true;
        }

        private static MetricsData LoadMetricsFromDatabase(string filePath)
        {
            if (filePath.Contains(".json") == false || File.Exists(filePath) == false) return null;
            return JsonUtility.FromJson<MetricsData>(File.ReadAllText(filePath));
        }

        #endregion
    }
}
