
using UnityEngine;
using System.Collections.Generic;
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
        [SerializeField] private WordContainer wordContainer;
        [SerializeField] private MetricsContainer metrics;
        private List<LetterSlotUi> _letterSlots;
        private AudioOneShot _audioOneShot;
        private EventBus _eventBus;
        private float _startTime;
        private float _breakTime;

        #region Public Methods

        public void Initialize(GameObject parent, EventBus bus)
        {
            _eventBus = bus;
            _letterSlots = Object.FindFirstObjectByType<LetterSlotContainer>().GetLetterSlots();
            _audioOneShot = parent.AddComponent<AudioOneShot>();
            for (var i = 0; i < _letterSlots.Count; i++)
            {
                _letterSlots[i].LetterSlotUpdateEvent += OnLetterSlotUpdate;
            }
        }

        public void Dispose()
        {
            for (var i = 0; i < _letterSlots.Count; i++)
            {
                _letterSlots[i].LetterSlotUpdateEvent -= OnLetterSlotUpdate;
            }
        }

        public void StartBreak()
        {
            _breakTime = Time.realtimeSinceStartup;
        }

        public void EndBreak()
        {
            _startTime += Time.realtimeSinceStartup - _breakTime;
        }

        public char FindUnassignedSlotLetter()
        {
            for (var i = 0; i < _letterSlots.Count; i++)
            {
                if (_letterSlots[i].IsAssigned) continue;
                _letterSlots[i].AssignSlotBorderColor(Color.white);
                _letterSlots[i].SetBlinkAnimation(true);
                return wordContainer.GetLetterAt(i);
            }

            return '0';
        }

        public void PlaceUiSlots()
        {
            _startTime = Time.realtimeSinceStartup;
            TurnOffAllSlots();
            TurnOnSlots(wordContainer.GetWordLength());
        }

        public void ResetUiSlots()
        {
            for (var i = 0; i < _letterSlots.Count; i++)
            {
                if (_letterSlots[i].IsAssigned == false) continue;
                _letterSlots[i].ResetLetterSlotText();
            }
        }

        #endregion

        #region Private Methods

        private bool DoesLetterMatchAtIndex(int i, char l) => wordContainer.DoesLetterMatch(i, l);

        private void OnLetterSlotUpdate(int index)
        {
            var isMatch = DoesLetterMatchAtIndex(index, _letterSlots[index].GetLetter());
            if (isMatch == false) metrics.Handler.IncrementInvalidLetters();

            if (IsWordSpelledCorrect(wordContainer.GetWordLength()) == false)
            {
                _letterSlots[index].SetBlinkAnimation(isMatch == false);
                _audioOneShot.PlayOneShot(isMatch ? 2 : 1);
            }
            else
            {
                _letterSlots[index].SetBlinkAnimation(false);
                _audioOneShot.PlayOneShot(3);
                _eventBus.OnWordCompleted();
            }

            metrics.Handler.AddGrabTime(_startTime);
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
            for (var i = 0; i < _letterSlots.Count; i++)
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
