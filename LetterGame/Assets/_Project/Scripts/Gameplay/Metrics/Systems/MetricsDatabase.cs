
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using LetterQuest.Gameplay.Metrics.Data;

namespace LetterQuest.Gameplay.Metrics.Database
{
    public class MetricsDatabase
    {
        private readonly List<MetricsData> loadedData = new();

        public MetricsData[] GetData() => loadedData.ToArray();

        public static void Save(string savePath, MetricsData data)
        {
            if (string.IsNullOrEmpty(savePath)) return;
            //Debug.Log($"[MetricsDatabase]: Save @ {savePath}");
            File.WriteAllText(savePath, JsonUtility.ToJson(data));
        }

        public MetricsData GetFirstLoadedData()
        {
            return loadedData.Count == 0 ? null : loadedData[0];
        }

        public int LoadAll(string folderPath, int maxLoadCount = 16)
        {
            if (Directory.Exists(folderPath) == false) return 0;
            var directoryInfo = new DirectoryInfo(folderPath);
            if (directoryInfo.Exists == false) return 0;

            var count = 0;
            var files = directoryInfo.GetFiles();
            for (var i = 0; i < files.Length; i++)
            {
                if (count == maxLoadCount) break;
                if (LoadMetricsFromDatabase(files[i].FullName) == false) continue;
                count++;
            }

            return count;
        }

        public void Clear()
        {
            loadedData.Clear();
        }

        private bool LoadMetricsFromDatabase(string filePath)
        {
            if (filePath.Contains(".json") == false || File.Exists(filePath) == false) return false;
            return AddLoadedMetrics(JsonUtility.FromJson<MetricsData>(File.ReadAllText(filePath)));
        }

        private bool AddLoadedMetrics(MetricsData data)
        {
            if (loadedData.Contains(data)) return false;
            loadedData.Add(data);
            return true;
        }
    }
}
