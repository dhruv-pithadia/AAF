
using UnityEngine;
using LetterQuest.Framework.Ui;
using LetterQuest.Gameplay.Events;
using LetterQuest.Gameplay.Database;

namespace LetterQuest.Gameplay.Ui
{
    public class LoginStatusUi : CanvasGroupHandler
    {
        [SerializeField] private UserLogin userLogin;
        [SerializeField] private EventBus eventBus;
        [SerializeField] private StatusLabelUi loginStatusLabel;
        [SerializeField] private GameObject createAccountBtn;
        [SerializeField] private GameObject logoutBtn;
        [SerializeField] private GameObject loginBtn;

        private const string NoLoginMsg = "Not logged in";
        private const string LoginMsg = "Logged in as : ";
        private const string GuestName = "guest";

        #region Unity Methods

        private void Start()
        {
            eventBus.LoginSuccessEvent += OnLoginEvent;
            CheckUserLogin();
            ShowUi();
        }

        private void OnDisable()
        {
            eventBus.LoginSuccessEvent -= OnLoginEvent;
        }

        #endregion

        #region Public Methods

        public void CheckUserLogin()
        {
            var username = userLogin.GetUserName();
            if (string.Equals(username, GuestName))
            {
                loginStatusLabel.UpdateStatus(NoLoginMsg, Color.red);
                LoggedOutSetup();
            }
            else
            {
                loginStatusLabel.UpdateStatus(LoginMsg + username, Color.green);
                LoggedInSetup();
            }
        }

        #endregion

        #region Private Methods

        private void OnLoginEvent()
        {
            ShowUi();
            CheckUserLogin();
        }

        private void LoggedOutSetup()
        {
            createAccountBtn.SetActive(true);
            logoutBtn.SetActive(false);
            loginBtn.SetActive(true);
        }

        private void LoggedInSetup()
        {
            createAccountBtn.SetActive(false);
            logoutBtn.SetActive(true);
            loginBtn.SetActive(false);
        }

        #endregion
    }
}
