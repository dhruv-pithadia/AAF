
using System;
using System.IO;
using UnityEngine;

namespace LetterQuest.Gameplay
{
    public class TextSerializer
    {
        private const string FileExtension = ".txt";
        private readonly string[] _difficulties = { "easy", "medium", "hard" };

        public TextSerializer()
        {
            for (var i = 0; i < _difficulties.Length; i++)
            {
                CreateTextFile(i);
            }
        }

        #region Public Methods

        public string[] LoadTextFile(int difficulty)
        {
            string filePath = CreateFilePath(difficulty);
            return File.Exists(filePath) ? ReadAndSplitTextFile(filePath) : Array.Empty<string>();
        }

        #endregion

        #region Private Methods
        
        private void CreateTextFile(int difficulty)
        {
            string filePath = CreateFilePath(difficulty);
            if (File.Exists(filePath)) return;

            File.AppendAllText(filePath, GetWordsOfDifficulty(difficulty));
        }
        
        private string CreateFilePath(int difficulty)
        {
            difficulty = Mathf.Clamp(difficulty, 0, 2);
            return Path.Combine(Application.persistentDataPath, _difficulties[difficulty] + FileExtension);
        }
        
        private string[] ReadAndSplitTextFile(string filePath)
        {
            var reader = new StreamReader(filePath);
            string content = reader.ReadToEnd();
            string[] lines = content.Split(new[] { "\n" }, StringSplitOptions.None);
            reader.Close();
            return lines;
        }
        
        private string GetWordsOfDifficulty(int difficulty)
        {
            var results = _difficulties[difficulty] switch
            {
                "easy" => "Hello\nWorld\nTable\nApple\nSnake\nBooks\nQuick\nJumbo\nDucky\nHawks",
                "medium" => "Iceberg\nAbility\nCabinet\nAccount\nBattery\nCentury\nAnimals\nDeliver\nMarried\nPopular",
                "hard" => "Watermelon\nUniversity\nManagement\nTechnology\nGovernment\nExperience\nActivities\nEverything\nProtection\nDiscussion",
                _ => string.Empty
            };

            return results;
        }

        #endregion
    }
}
