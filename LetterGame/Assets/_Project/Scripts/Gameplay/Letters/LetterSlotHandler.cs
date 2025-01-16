
using UnityEngine;
using LetterQuest.Framework.Audio;
using LetterQuest.Gameplay.Events;
using LetterQuest.Gameplay.Metrics;
using LetterQuest.Gameplay.Letters.Ui;

namespace LetterQuest.Gameplay.Letters
{
    [System.Serializable]
    public class LetterSlotHandler
    {
        [SerializeField] private PlayerMetrics playerMetrics;
        private LetterSlotUi[] _letterSlots;
        private AudioOneShot _audioOneShot;
        private EventBus _eventBus;
        private float _startTime;
        private char[] _currentWord;

        #region Public Methods

        public void Initialize(GameObject parent, EventBus bus, LetterUiContainer container)
        {
            _eventBus = bus;
            _letterSlots = container.GetLetterSlots();
            _audioOneShot = parent.AddComponent<AudioOneShot>();
            _eventBus.WordSetEvent += OnNewWord;
            for (var i = 0; i < _letterSlots.Length; i++)
            {
                _letterSlots[i].LetterSlotUpdateEvent += OnLetterSlotUpdate;
            }
        }

        public void Dispose()
        {
            _eventBus.WordSetEvent -= OnNewWord;
            for (var i = 0; i < _letterSlots.Length; i++)
            {
                _letterSlots[i].LetterSlotUpdateEvent -= OnLetterSlotUpdate;
            }
        }

        public void ResetAllSlots()
        {
            for (var i = 0; i < _letterSlots.Length; i++)
            {
                if (_letterSlots[i].IsAssigned == false) continue;
                _letterSlots[i].ResetLetterSlotText();
            }
        }

        #endregion

        #region Private Methods

        private void AssignCurrentWord(string word) => _currentWord = word.ToCharArray();
        private bool DoesLetterMatchAtIndex(int i) => _currentWord[i] == _letterSlots[i].GetLetter();

        private void OnNewWord(string word)
        {
            _startTime = Time.realtimeSinceStartup;
            TurnOffAllSlots();
            AssignCurrentWord(word);
            TurnOnSlots(_currentWord.Length);
        }

        private void OnLetterSlotUpdate(int slotIndex)
        {
            var isMatch = DoesLetterMatchAtIndex(slotIndex);
            if (isMatch == false) playerMetrics.SimpleMetrics.IncrementInvalidPlacements();

            if (IsWordSpelledCorrect() == false)
            {
                _letterSlots[slotIndex].SetBlinkAnimation(isMatch == false);
                _audioOneShot.PlayOneShot(isMatch ? 2 : 1);
            }
            else
            {
                _letterSlots[slotIndex].SetBlinkAnimation(false);
                _audioOneShot.PlayOneShot(3);
                _eventBus.OnWordCompleted();
            }

            playerMetrics.AdvancedMetrics.AddLetterTime(_startTime);
            _startTime = Time.realtimeSinceStartup;
        }

        private bool IsWordSpelledCorrect()
        {
            var result = true;
            for (var i = 0; i < _currentWord.Length; i++)
            {
                if (DoesLetterMatchAtIndex(i))
                {
                    _letterSlots[i].AssignSlotBorderColor(Color.green);
                }
                else
                {
                    result = false;
                    if (_letterSlots[i].IsAssigned == false) continue;
                    _letterSlots[i].AssignSlotBorderColor(Color.red);
                }
            }

            return result;
        }

        private void TurnOffAllSlots()
        {
            for (var i = 0; i < _letterSlots.Length; i++)
            {
                _letterSlots[i].gameObject.SetActive(false);
            }
        }

        private void TurnOnSlots(int count)
        {
            for (var i = 0; i < count; i++)
            {
                _letterSlots[i].gameObject.SetActive(true);
            }
        }

        #endregion
    }
}
