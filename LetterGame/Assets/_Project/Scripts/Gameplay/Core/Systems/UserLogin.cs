
using System;
using System.IO;
using System.Text;
using UnityEngine;
using LetterQuest.Gameplay.Settings;

namespace LetterQuest.Core.Login
{
    [CreateAssetMenu(fileName = "UserLogin", menuName = "LetterQuest/User Login")]
    public class UserLogin : ScriptableObject
    {
        [field: SerializeField] public bool IsLoggedIn { get; private set; } = false;
        [SerializeField] private GameDifficulty gameDifficulty;
        private const string PasswordFile = "password.txt";
        private const string GuestName = "guest";
        private const string Extension = ".json";
        private StringBuilder _stringBuilder;
        private string _folderPath;
        private string _userName;

        #region Public Methods

        public void CheckLogin()
        {
            if (IsLoggedIn) return;
            LoginAsGuest();
        }

        public void Dispose()
        {
            _stringBuilder?.Clear();
            _stringBuilder = null;
        }

        public string GetUserName() => _userName;
        public string GetFolderPath() => _folderPath;

        public bool CreateAccount(string username, string password)
        {
            IsLoggedIn = false;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
            if (string.Equals(username, GuestName)) return false;
            if (DoesUserExist(CreateFolderPath(username.ToLower()))) return false;
            CreatePasswordFile(password);
            IsLoggedIn = true;
            return true;
        }

        public bool Login(string username, string password)
        {
            IsLoggedIn = false;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return false;
            if (DoesUserExist(CreateFolderPath(username.ToLower())) == false) return false;
            IsLoggedIn = DoesPasswordMatch(Path.Combine(_folderPath, PasswordFile), password);
            return IsLoggedIn;
        }

        public void LoginAsGuest()
        {
            _folderPath = string.Empty;
            _userName = GuestName;
            IsLoggedIn = false;
        }

        public string GetSavePath()
        {
            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_folderPath)) return string.Empty;
            return string.Equals(_userName, GuestName)
                ? string.Empty
                : Path.Combine(GetSaveFolderName(), GetSaveFileName());
        }

        #endregion

        #region Private Methods
        
        private string GetSaveFolderName()
        {
            var saveFolder = Path.Combine(_folderPath, GetSaveFolder(gameDifficulty.Value));
            if (Directory.Exists(saveFolder) == false)
            {
                Directory.CreateDirectory(saveFolder);
            }
            return saveFolder;
        }

        private string GetSaveFileName()
        {
            var time = DateTime.Now;
            _stringBuilder ??= new StringBuilder();
            _stringBuilder.Append(_userName);
            _stringBuilder.Append("_");
            _stringBuilder.Append(time.ToString("yyyy-MM-dd"));
            _stringBuilder.Append("_");
            _stringBuilder.Append(time.ToString("HH;mm;ss"));
            _stringBuilder.Append(Extension);
            return _stringBuilder.ToString();
        }

        private static string GetSaveFolder(int difficulty)
        {
            var folder = difficulty switch
            {
                2 => "Hard",
                1 => "Medium",
                _ => "Easy"
            };
            return folder;
        }

        private void CreatePasswordFile(string password)
        {
            Directory.CreateDirectory(_folderPath);
            File.AppendAllText(Path.Combine(_folderPath, PasswordFile), password);
        }

        private string CreateFolderPath(string username)
        {
            _userName = username;
            _folderPath = Path.Combine(Application.persistentDataPath, _userName);
            return _folderPath;
        }

        private static bool DoesUserExist(string filePath)
        {
            return Directory.Exists(filePath);
        }

        private static bool DoesPasswordMatch(string filePath, string password)
        {
            return File.Exists(filePath) && string.Equals(password, ReadTextFile(filePath));
        }

        private static string ReadTextFile(string filePath)
        {
            var reader = new StreamReader(filePath);
            var content = reader.ReadToEnd();
            reader.Close();
            return content;
        }

        #endregion
    }
}
