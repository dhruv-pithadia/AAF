
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LetterQuest.Framework.Scenes
{
    public class SceneTransition : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad = string.Empty;

        public void LoadScene()
        {
            if (IsInvoking()) return;
            if (string.IsNullOrEmpty(sceneToLoad) == false) ValidateScenePath();
            else Invoke(nameof(Quit), 3f);
        }

        private void ValidateScenePath()
        {
            if (SceneUtility.GetBuildIndexByScenePath(sceneToLoad) == -1)
            {
                Debug.LogError($"[SceneTransition]: scene to load ( {sceneToLoad} ) does not exist");
            }
            else
            {
                Invoke(nameof(Load), 0.5f);
            }
        }

        private void Load()
        {
            SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
        }

        private void Quit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
