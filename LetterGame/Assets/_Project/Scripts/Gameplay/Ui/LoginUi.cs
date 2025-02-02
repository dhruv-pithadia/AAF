
using TMPro;
using UnityEngine;
using LetterQuest.Core.Login;
using LetterQuest.Framework.Ui;
using LetterQuest.Gameplay.Events;

namespace LetterQuest.Gameplay.Ui
{
    public class LoginUi : CanvasGroupHandler
    {
        [SerializeField] private UserLogin userLogin;
        [SerializeField] private EventBus eventBus;
        [SerializeField] private TMP_InputField usernameInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_Text errorText;
        [SerializeField] private GameObject loginBtn;
        [SerializeField] private GameObject createAccountBtn;
        [SerializeField] private GameObject forgotPasswordBtn;

        private void Start()
        {
            HideAllUi();
            userLogin.CheckLogin();
        }

        public void CreateNewAccount()
        {
            if (userLogin.CreateAccount(usernameInputField.text, passwordInputField.text))
            {
                eventBus.OnLoginSuccess();
                HideAllUi();
                return;
            }

            SetErrorText("Username already exists");
        }

        public void Login()
        {
            if (userLogin.Login(usernameInputField.text, passwordInputField.text))
            {
                eventBus.OnLoginSuccess();
                HideAllUi();
                return;
            }

            SetErrorText("Invalid username or password");
        }

        public void Logout()
        {
            userLogin.LoginAsGuest();
        }

        public void ShowLoginUi()
        {
            ShowUi();
            loginBtn.SetActive(true);
            createAccountBtn.SetActive(false);
            forgotPasswordBtn.SetActive(true);
        }

        public void ShowAccountUi()
        {
            ShowUi();
            loginBtn.SetActive(false);
            createAccountBtn.SetActive(true);
            forgotPasswordBtn.SetActive(false);
        }

        public void HideAllUi()
        {
            SetErrorText(string.Empty);
            usernameInputField.text = string.Empty;
            passwordInputField.text = string.Empty;
            HideUi();
        }

        private void SetErrorText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                errorText.text = string.Empty;
                errorText.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                errorText.transform.parent.gameObject.SetActive(true);
                errorText.text = text;
            }
        }
    }
}
