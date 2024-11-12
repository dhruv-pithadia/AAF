using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] private string SceneToLoad = string.Empty;
    public void loadscene()
    {
        if (SceneToLoad == "Exit")
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit()
#endif
        }
        else if (SceneToLoad != string.Empty)
        {
            SceneManager.LoadScene(SceneToLoad);
        }

    }
}
