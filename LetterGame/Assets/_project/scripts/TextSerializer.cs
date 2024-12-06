
using System;
using System.IO;
using UnityEngine;

namespace LetterQuest
{
    public class TextSerializer : MonoBehaviour
    {
        public void CreateTextFile(string path)
        {
            Debug.Log("[TextSerializer]: Create Text File");
            if (File.Exists(path)) return;
            Debug.Log("Text File is being created");
            string content = "Hello\nWorld\nTable\nCoffee\nMovies\nBooks\nAnimals\nDolphin";
            File.AppendAllText(path, content);
            Debug.Log($"Text File has been created @ {path}");
        }

        public string[] LoadTextFile(string path)
        {
            Debug.Log("[TextSerializer]: Load Text File");
            if (!File.Exists(path)) return Array.Empty<string>();

            Debug.Log("Text File is being loaded");
            StreamReader reader = new StreamReader(path);
            string content = reader.ReadToEnd();
            string[] lines = content.Split(new[] { "\n" }, StringSplitOptions.None);
            reader.Close();

            return lines;
        }
    }
}
