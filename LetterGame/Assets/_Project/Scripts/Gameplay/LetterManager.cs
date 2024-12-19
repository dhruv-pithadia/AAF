
using UnityEngine;
using LetterQuest.Gameplay.Data;
using System.Collections.Generic;
using LetterQuest.Framework.Input;
using LetterQuest.Gameplay.Words.Data;
using LetterQuest.Gameplay.Letters.Data;

namespace LetterQuest.Gameplay.Letters.Manager
{
    [RequireComponent(typeof(LetterObjectPool))]
    public class LetterManager : MonoBehaviour
    {
        [SerializeField] private LetterPositioningMode mode = LetterPositioningMode.Alphabet;
        [SerializeField] private LetterPositioning letterPositions;
        [SerializeField] private CurrentWordData currentWordData;
        [SerializeField] private GameDifficulty gameDifficulty;
        [SerializeField] private InputDetection inputDetection;
        private readonly List<LetterBlock> _letterList = new();
        private LetterObjectPool letterObjPool;
        private const int MaxLetters = 26;

        #region Unity Methods

        private void Start()
        {
            inputDetection.Initialize(Camera.main);
            letterObjPool = GetComponent<LetterObjectPool>();
            currentWordData.WordAssignedEvent += OnWordAssigned;
            SetupPositioningMode();
            PlaceLetters();
        }

        private void LateUpdate()
        {
            for (var i = 0; i < _letterList.Count; i++)
            {
                if (_letterList[i].IsDragging == false) continue;
                _letterList[i].MoveLetter(InputDetection.GetMouseWorldPosition());
                break;
            }
        }

        private void OnDisable()
        {
            currentWordData.WordAssignedEvent -= OnWordAssigned;
            inputDetection.Dispose();
            _letterList?.Clear();
        }

        #endregion

        #region Private Methods

        private void SetupPositioningMode()
        {
            mode = (LetterPositioningMode)gameDifficulty.Value;
        }

        private void OnWordAssigned()
        {
            RemoveLetters();
            PlaceLetters();
        }

        private void PlaceLetters()
        {
            switch (mode)
            {
                case LetterPositioningMode.Alphabet:
                    PlaceLettersAlphabetically();
                    break;
                case LetterPositioningMode.Qwerty:
                    PlaceLetters(currentWordData.GetQwertyIndicies());
                    break;
                default:
                    PlaceLetters(currentWordData.GetRandomIndicies());
                    break;
            }
        }

        private void PlaceLetters(int[] asciiArray)
        {
            for (var i = 0; i < asciiArray.Length; i++)
            {
                if (i >= MaxLetters) break;
                var letter = letterObjPool.GetLetter();
                letter.OnSpawn(letterPositions.GetPositionAt(mode, i), asciiArray[i]);
                _letterList.Add(letter);
            }
        }

        private void PlaceLettersAlphabetically()
        {
            var ascii = 65; //  65 = A | 90 = Z
            for (var i = 0; i < MaxLetters; i++)
            {
                var letter = letterObjPool.GetLetter();
                letter.OnSpawn(letterPositions.GetPositionAt(mode, i), ascii);
                _letterList.Add(letter);
                ascii++;
            }
        }

        private void RemoveLetters()
        {
            if (ReferenceEquals(_letterList, null)) return;
            if (_letterList.Count <= 0) return;

            for (var i = _letterList.Count - 1; i >= 0; i--)
            {
                var letter = _letterList[i];
                _letterList.RemoveAt(i);
                letter.OnDespawn();
                letterObjPool.ReturnLetter(letter);
            }
        }

        #endregion
    }
}
