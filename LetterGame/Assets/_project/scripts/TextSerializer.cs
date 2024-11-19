using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;
using System;

public class TextSerializer : MonoBehaviour
{

    public void CreateTextFile(string path)
    {
        if (File.Exists(path)) return;
        Debug.Log("Text File is being created");
        string content = "Hello\nWorld\nTable\nCoffee\nMovies\nBooks\nAnimals\nDolphin";
        File.AppendAllText(path, content);
        Debug.Log($"Text File has been created @ {path}");
    }

    public string[] LoadTextFile(string path)
    {
        if (!File.Exists(path)) return new string[0];
        Debug.Log("Text File is being loaded");
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        string[] lines = content.Split(new[] { "\n" }, StringSplitOptions.None);

        reader.Close();

        return lines;

    }
}
