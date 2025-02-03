
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using LetterQuest.Core.Login;
using LetterQuest.Framework.Ui;
using LetterQuest.Gameplay.Events;

namespace LetterQuest.Gameplay.Ui
{
    public class LoginUi : CanvasGroupHandler
    {
        [SerializeField] private Button enterBtn;
        [SerializeField] private EventBus eventBus;
        [SerializeField] private UserLogin userLogin;
        [SerializeField] private GameObject forgotPasswordBtn;
        [SerializeField] private TMP_InputField passwordCopyInput;
        [SerializeField] private TMP_InputField passwordInput;
        [SerializeField] private TMP_InputField usernameInput;
        [SerializeField] private TMP_Text errorText;

        private void Start()
        {
            HideAllUi();
            userLogin.CheckLogin();
        }

        public void CreateNewAccount()
        {
            var user = usernameInput.text;
            var password = passwordInput.text;
            var pwCopy = passwordCopyInput.text;

            if (string.IsNullOrEmpty(user))
            {
                SetErrorText("Username is empty");
                return;
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(pwCopy))
            {
                SetErrorText("Password is empty");
                return;
            }

            if (string.Equals(password, pwCopy) == false)
            {
                SetErrorText("Passwords do not match");
                return;
            }

            if (userLogin.CreateAccount(user, password))
            {
                eventBus.OnLoginSuccess();
                HideAllUi();
                return;
            }

            SetErrorText("Username already exists");
        }

        public void Login()
        {
            var user = usernameInput.text;
            var password = passwordInput.text;

            if (string.IsNullOrEmpty(user))
            {
                SetErrorText("Username is empty");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                SetErrorText("Password is empty");
                return;
            }

            if (userLogin.Login(user, password))
            {
                eventBus.OnLoginSuccess();
                HideAllUi();
                return;
            }

            SetErrorText("Invalid username or password");
        }

        public void ForgotPassword()
        {
            var user = usernameInput.text;
            var password = passwordInput.text;
            var pwCopy = passwordCopyInput.text;

            if (string.IsNullOrEmpty(user))
            {
                SetErrorText("Username is empty");
                return;
            }

            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(pwCopy))
            {
                SetErrorText("Password is empty");
                return;
            }

            if (string.Equals(password, pwCopy) == false)
            {
                SetErrorText("Passwords do not match");
                return;
            }

            if (userLogin.ReplacePassword(user, password))
            {
                eventBus.OnLoginSuccess();
                HideAllUi();
                return;
            }

            SetErrorText("Username does not exist");
        }

        public void Logout()
        {
            userLogin.LoginAsGuest();
        }

        public void ShowLoginUi()
        {
            ShowAndClearUi();
            forgotPasswordBtn.SetActive(true);
            usernameInput.gameObject.SetActive(true);
            passwordInput.gameObject.SetActive(true);
            passwordCopyInput.gameObject.SetActive(false);
            enterBtn.onClick.RemoveAllListeners();
            enterBtn.onClick.AddListener(Login);
            SetErrorText("Login");
        }

        public void ShowAccountUi()
        {
            ShowAndClearUi();
            forgotPasswordBtn.SetActive(false);
            usernameInput.gameObject.SetActive(true);
            passwordInput.gameObject.SetActive(true);
            passwordCopyInput.gameObject.SetActive(true);
            enterBtn.onClick.RemoveAllListeners();
            enterBtn.onClick.AddListener(CreateNewAccount);
            SetErrorText("Create Account");
        }

        public void ShowForgotPasswordUi()
        {
            ShowAndClearUi();
            forgotPasswordBtn.SetActive(false);
            usernameInput.gameObject.SetActive(true);
            passwordInput.gameObject.SetActive(true);
            passwordCopyInput.gameObject.SetActive(true);
            enterBtn.onClick.RemoveAllListeners();
            enterBtn.onClick.AddListener(ForgotPassword);
            SetErrorText("Forgot Password");
        }

        public void HideAllUi()
        {
            SetErrorText(string.Empty);
            ResetInputFields();
            HideUi();
        }

        private void ShowAndClearUi()
        {
            ShowUi();
            ResetInputFields();
        }

        private void ResetInputFields()
        {
            usernameInput.text = string.Empty;
            passwordInput.text = string.Empty;
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
