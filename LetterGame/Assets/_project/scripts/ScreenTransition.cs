
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LetterQuest
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
                Application.Quit()
#endif
            }
            else if (sceneToLoad != string.Empty)
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}