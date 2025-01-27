
using System;
using System.IO;
using System.Text;
using UnityEngine;

namespace LetterQuest.Gameplay.Database
{
    [CreateAssetMenu(fileName = "UserLogin", menuName = "LetterQuest/User Login")]
    public class UserLogin : ScriptableObject
    {
        [SerializeField] private bool isLoggedIn;
        private const string PasswordFile = "password.txt";
        private const string GuestName = "guest";
        private const string Footer = " ).json";
        private const string Header = "( ";
        private StringBuilder _stringBuilder;
        private string _folderPath;
        private string _userName;

        private void OnDisable()
        {
            isLoggedIn = false;
        }

        #region Public Methods

        public void Initialize()
        {
            _stringBuilder = new StringBuilder();
            if (isLoggedIn) return;
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
            isLoggedIn = false;
            if (DoesUserExist(CreateFolderPath(username.ToLower()))) return isLoggedIn;
            CreatePasswordFile(password);
            isLoggedIn = true;
            return isLoggedIn;
        }

        public bool Login(string username, string password)
        {
            isLoggedIn = false;
            if (DoesUserExist(CreateFolderPath(username.ToLower())) == false) return isLoggedIn;
            isLoggedIn = DoesPasswordMatch(Path.Combine(_folderPath, PasswordFile), password);
            return isLoggedIn;
        }
        
        public void LoginAsGuest()
        {
            isLoggedIn = true;
            if (DoesUserExist(CreateFolderPath(GuestName))) return;
            Directory.CreateDirectory(_folderPath);
        }
        
        public string GetSavePath()
        {
            _stringBuilder.Clear();
            var time = DateTime.Now;
            _stringBuilder.Append(Header);
            _stringBuilder.Append(time.ToString("yyyy-MM-dd"));
            _stringBuilder.Append("_");
            _stringBuilder.Append(time.ToString("HH:mm:ss"));
            _stringBuilder.Append(Footer);
            return Path.Combine(_folderPath, _stringBuilder.ToString());
        }

        #endregion

        #region Private Methods

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
