
using UnityEngine;
using LetterQuest.Gameplay.Data;
using LetterQuest.Gameplay.Input;
using System.Collections.Generic;
using LetterQuest.Gameplay.Letters.Data;

namespace LetterQuest.Gameplay.Letters
{
    [System.Serializable]
    public class LetterBlockPlacer
    {
        [SerializeField] private LetterPositioningMode mode = LetterPositioningMode.Alphabet;
        [SerializeField] private LetterPositioning letterPositions;
        [SerializeField] private GameDifficulty gameDifficulty;
        private readonly List<LetterBlock> _letterList = new();
        private const int MaxLetters = 26;
        private LetterObjectPool _letterObjPool;
        private LetterBlock _grabbedLetter;

        #region Public Methods

        public void Initialize(LetterObjectPool letterObjectPool)
        {
            _letterObjPool = letterObjectPool;
            SetArrangementMode();
        }

        public void Tick()
        {
            for (var i = 0; i < _letterList.Count; i++)
            {
                if (_letterList[i].IsDragging == false) continue;
                _letterList[i].MoveLetter(InputDetection.GetMouseWorldPosition());
                break;
            }
        }

        public void Dispose()
        {
            _letterList?.Clear();
        }

        public LetterBlock ReleaseBlock()
        {
            for (var i = 0; i < _letterList.Count; i++)
            {
                if (_letterList[i].IsDetectable) continue;
                _letterList[i].EnableCollision();
            }
            return _grabbedLetter;
        }

        public void GrabBlock(LetterBlock letterBlock)
        {
            _grabbedLetter = letterBlock;
            for (var i = 0; i < _letterList.Count; i++)
            {
                if (ReferenceEquals(_letterList[i], _grabbedLetter)) continue;
                _letterList[i].DisableCollision();
            }
            _grabbedLetter.OnDragStart();
        }

        public void UpdateBlockArrangement(string word, int change = 0)
        {
            RemoveLetters();
            SetArrangementMode(change);
            PlaceLetters(word);
        }

        public void PlaceLetters(string word)
        {
            switch (mode)
            {
                case LetterPositioningMode.Alphabet:
                    PlaceLettersAlphabetically();
                    break;
                case LetterPositioningMode.Qwerty:
                    PlaceLetters(letterPositions.GetQwertyIndicies());
                    break;
                default:
                    PlaceLetters(letterPositions.GetRandomIndicies(word));
                    break;
            }
        }

        public void RemoveLetters()
        {
            if (ReferenceEquals(_letterList, null)) return;
            if (_letterList.Count <= 0) return;

            for (var i = _letterList.Count - 1; i >= 0; i--)
            {
                var letter = _letterList[i];
                _letterList.RemoveAt(i);
                letter.OnDespawn();
                _letterObjPool.ReturnLetter(letter);
            }
        }

        #endregion

        #region Private Methods

        private void SetArrangementMode(int change = 0)
        {
            switch (change)
            {
                case 0:
                    mode = (LetterPositioningMode)gameDifficulty.Value;
                    break;
                case 1:
                    mode = LetterPositioningMode.Alphabet;
                    break;
                default:
                    mode = LetterPositioningMode.Qwerty;
                    break;
            }
        }

        private void PlaceLettersAlphabetically()
        {
            var ascii = 65; //  65 = A | 90 = Z
            for (var i = 0; i < MaxLetters; i++)
            {
                var letter = _letterObjPool.GetLetter();
                letter.OnSpawn(letterPositions.GetPositionAt(mode, i), ascii);
                letter.transform.localScale = letterPositions.GetScale(mode);
                _letterList.Add(letter);
                ascii++;
            }
        }

        private void PlaceLetters(int[] asciiArray)
        {
            for (var i = 0; i < asciiArray.Length; i++)
            {
                if (i >= MaxLetters) break;
                var letter = _letterObjPool.GetLetter();
                letter.OnSpawn(letterPositions.GetPositionAt(mode, i), asciiArray[i]);
                letter.transform.localScale = letterPositions.GetScale(mode);
                _letterList.Add(letter);
            }
        }

        #endregion
    }
}
