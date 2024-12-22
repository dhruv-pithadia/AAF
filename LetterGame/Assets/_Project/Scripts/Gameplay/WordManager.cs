
using TMPro;
using UnityEngine;
using LetterQuest.Gameplay.Data;
using UnityEngine.SceneManagement;
using LetterQuest.Gameplay.Letters;
using LetterQuest.Gameplay.Words.Ui;
using LetterQuest.Gameplay.Words.Data;
using LetterQuest.Framework.Animation;
using LetterQuest.Framework.Audio;

namespace LetterQuest.Gameplay.Words.Manager
{
    public class WordManager : MonoBehaviour
    {
        [SerializeField] private int maxWordCount = 20;
        [SerializeField] private TMP_Text currentWordText;
        [SerializeField] private CurrentWordData currentWordData;
        [SerializeField] private GameDifficulty gameDifficulty;
        [SerializeField] private AnimatorHook animatorHook;
        [SerializeField] private CanvasGroup generalSettingsGroup;
        [SerializeField] private CanvasGroup audioSettingsGroup;
        [field: SerializeField] private LetterSlotHandler LetterSlotHandler { get; set; }
        [field: SerializeField] private ProgressBar ProgressBar { get; set; }
        private WordGenerator _wordGenerator;
        private const float Timer = 3f;

        #region Unity Methods

        public void Start()
        {
            LetterSlotHandler.Initialize(this);
            ProgressBar.Initialize(new WordTrackingData(0, maxWordCount));
            _wordGenerator = new WordGenerator(gameDifficulty.Value);
            AssignNextWord();
        }

        public void OnDisable()
        {
            LetterSlotHandler.Dispose();
            ProgressBar.Dispose();
            if (ReferenceEquals(_wordGenerator, null)) return;
            _wordGenerator.Dispose();
            _wordGenerator = null;
        }

        #endregion

        #region Public Methods

        public void ToggleGeneralSettings()
        {
            if (generalSettingsGroup.alpha == 1f)
            {
                generalSettingsGroup.alpha = 0f;
                generalSettingsGroup.blocksRaycasts = false;
                generalSettingsGroup.interactable = false;
            }
            else
            {
                generalSettingsGroup.alpha = 1f;
                generalSettingsGroup.blocksRaycasts = true;
                generalSettingsGroup.interactable = true;
            }
        }
        
        public void ToggleAudioSettings()
        {
            if (audioSettingsGroup.alpha == 1f)
            {
                audioSettingsGroup.alpha = 0f;
                audioSettingsGroup.blocksRaycasts = false;
                audioSettingsGroup.interactable = false;
            }
            else
            {
                audioSettingsGroup.alpha = 1f;
                audioSettingsGroup.blocksRaycasts = true;
                audioSettingsGroup.interactable = true;
            }
        }

        public void MusicVolumeChanged(float value)
        {
            AudioManager.Instance?.SetMusicVolume(value);
        }
        
        public void SfxVolumeChanged(float value)
        {
            AudioManager.Instance?.SetSfxVolume(value);
        }

        public void SkipWord()
        {
            SetupNextWord();
            AssignNextWord();
        }
        
        public void OnWordComplete()
        {
            SetupNextWord();
            animatorHook.Play();
            Invoke(nameof(AssignNextWord), Timer);
        }

        #endregion

        #region Private Methods

        private void SetupNextWord()
        {
            currentWordData.WordComplete();
            ProgressBar.WordCountTick();
        }

        private void AssignNextWord()
        {
            LetterSlotHandler.ResetAllSlots();
            currentWordData.SetCurrentWord(_wordGenerator.GetNextWord());

            if (currentWordData.GetText() == string.Empty)
            {
                //  TODO: Go to Player Metrics Screen
                currentWordText.text = "Game Over!";
                Invoke(nameof(EndLevel), Timer);
            }
            else
            {
                currentWordText.text = currentWordData.GetText();
                LetterSlotHandler.OnWordUpdate(currentWordData.GetTextUpperCase());
            }
        }

        private void EndLevel()
        {
            SceneManager.LoadScene("Splash", LoadSceneMode.Single);
        }

        #endregion
    }
}
