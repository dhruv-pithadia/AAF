
using UnityEngine;
using LetterQuest.Gameplay.Metrics;

namespace LetterQuest.Gameplay.Database
{
    public class MetricsDatabase : MonoBehaviour
    {
        [SerializeField] private UserLogin userLogin;
        [SerializeField] private PlayerMetrics playerMetrics;

        private void Start()
        {
            userLogin.Initialize();
        }

        private void OnDisable()
        {
            userLogin.Dispose();
        }

        public void SaveMetricsToDatabase()
        {
            var folderPath = userLogin.GetFolderPath();
            Debug.Log($"[MetricsSerializer]: saving to - {userLogin.GetSavePath()}");
        }

        public void LoadMetricsFromDatabase(string filePath)
        {
            Debug.Log($"[MetricsSerializer]: loading from - {filePath}");
        }
    }
}
