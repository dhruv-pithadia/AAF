using JetBrains.Annotations;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;


public class letter : MonoBehaviour
{
    private TMP_Text LetterText;
    public void onSpawn(string text)
    {
        LetterText = GetComponentInChildren<TMP_Text>();
        LetterText.text = text;
    }

    public void ondeSpawn()
    {

    }
}