
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

            if (sceneToLoad == "Exit")
            {
                Invoke(nameof(Quit), 1f);
            }
            else if (sceneToLoad != string.Empty)
            {
                Invoke(nameof(Load), 0.5f);
            }
            else
            {
                Debug.LogError("[SceneTransition]: scene to load is null or empty");
            }
        }

        private void Load()
        {
            SceneManager.LoadScene(sceneToLoad);
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
