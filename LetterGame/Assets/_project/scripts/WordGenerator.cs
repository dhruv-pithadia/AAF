
using System.IO;
using UnityEngine;

namespace LetterQuest
{
    public class WordGenerator : MonoBehaviour
    {
        private const string FileExtension = ".txt";
        private readonly string[] _difficulties = { "easy", "medium", "hard" };
        private string[] _currentWords;
        private TextSerializer _serializer;
        private int _currentDifficulty = 0;

        private void Start()
        {
            _serializer = GetComponent<TextSerializer>();
            string filePath = Path.Combine(Application.persistentDataPath,
                _difficulties[_currentDifficulty] + FileExtension);
            _currentWords = _serializer.LoadTextFile(filePath);
            Shuffle(_currentWords);
            foreach (var line in _currentWords)
            {
                Debug.Log(line);
            }
        }

        private void Shuffle(string[] text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                string tmpWord = text[i];
                int index = Random.Range(i, text.Length);
                text[i] = text[index];
                text[index] = tmpWord;
            }
        }
    }
}
