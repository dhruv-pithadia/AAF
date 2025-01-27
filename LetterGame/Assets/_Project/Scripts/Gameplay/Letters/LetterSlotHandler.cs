
using UnityEngine;
using LetterQuest.Framework.Audio;
using LetterQuest.Gameplay.Events;
using LetterQuest.Gameplay.Metrics;
using LetterQuest.Gameplay.Words.Data;
using LetterQuest.Gameplay.Letters.Ui;

namespace LetterQuest.Gameplay.Letters
{
    [System.Serializable]
    public class LetterSlotHandler
    {
        [SerializeField] private PlayerMetrics playerMetrics;
        [SerializeField] private WordContainer wordContainer;
        private LetterSlotUi[] _letterSlots;
        private AudioOneShot _audioOneShot;
        private EventBus _eventBus;
        private float _startTime;

        #region Public Methods

        public void Initialize(GameObject parent, EventBus bus)
        {
            _eventBus = bus;
            _letterSlots = Object.FindFirstObjectByType<LetterUiContainer>().GetLetterSlots();
            _audioOneShot = parent.AddComponent<AudioOneShot>();
            for (var i = 0; i < _letterSlots.Length; i++)
            {
                _letterSlots[i].LetterSlotUpdateEvent += OnLetterSlotUpdate;
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _letterSlots.Length; i++)
            {
                _letterSlots[i].LetterSlotUpdateEvent -= OnLetterSlotUpdate;
            }
        }

        public void PlaceUiSlots()
        {
            _startTime = Time.realtimeSinceStartup;
            TurnOffAllSlots();
            TurnOnSlots(wordContainer.GetWordLength());
        }

        public void ResetUiSlots()
        {
            for (var i = 0; i < _letterSlots.Length; i++)
            {
                if (_letterSlots[i].IsAssigned == false) continue;
                _letterSlots[i].ResetLetterSlotText();
            }
        }

        #endregion

        #region Private Methods

        private bool DoesLetterMatchAtIndex(int i, char l) => wordContainer.DoesLetterMatch(i, l);

        private void OnLetterSlotUpdate(int slotIndex)
        {
            var isMatch = DoesLetterMatchAtIndex(slotIndex, _letterSlots[slotIndex].GetLetter());
            if (isMatch == false) playerMetrics.SimpleMetrics.IncrementInvalidPlacements();

            if (IsWordSpelledCorrect(wordContainer.GetWordLength()) == false)
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

        private bool IsWordSpelledCorrect(int length)
        {
            var result = true;
            for (var i = 0; i < length; i++)
            {
                if (DoesLetterMatchAtIndex(i, _letterSlots[i].GetLetter()))
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
