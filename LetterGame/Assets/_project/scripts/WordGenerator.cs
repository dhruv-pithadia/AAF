using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.IO;

public class WordGenerator : MonoBehaviour
{
    private string[] difficulties = new string[] { "easy", "medium", "hard" };
    private string[] CurrentWords;
    private TextSerializer serializer;
    private int CurrentDifficulty = 0;
    private string file_extension = ".txt";

    private void Start()
    {
        serializer = GetComponent<TextSerializer>();
        string file_path = Path.Combine(Application.persistentDataPath, difficulties[CurrentDifficulty] + file_extension);
        CurrentWords = serializer.LoadTextFile(file_path);
        Shuffle(CurrentWords);
        foreach (string line in CurrentWords)
        {
            Debug.Log(line);
        }
    }

    private void Shuffle(string[] text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            string tempword = text[i];
            int index = Random.Range(i, text.Length);
            text[i] = text[index];
            text[index] = tempword;
        }
    }
}
