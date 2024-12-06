
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LetterQuest.Core
{
    public class ScreenTransition : MonoBehaviour
    {
        [SerializeField] private string sceneToLoad = string.Empty;

        public void LoadScene()
        {
            if (sceneToLoad == "Exit")
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
            else if (sceneToLoad != string.Empty)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
            else
            {
                Debug.LogError("[SceneTransition]: scene to load is null or empty");
            }
        }
    }
}
