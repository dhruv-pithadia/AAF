
using UnityEngine;
using LetterQuest.Gameplay.Ui;
using LetterQuest.Gameplay.Input;
using System.Collections.Generic;
using LetterQuest.Gameplay.Settings;
using LetterQuest.Gameplay.Words.Data;
using LetterQuest.Gameplay.Letters.Data;

namespace LetterQuest.Gameplay.Letters
{
    [System.Serializable]
    public class LetterBlockHandler
    {
        [SerializeField] private PositionMode currentMode = PositionMode.Easy;
        [SerializeField] private LetterPositioning letterPositions;
        [SerializeField] private GameDifficulty gameDifficulty;
        [SerializeField] private InputDetection inputDetection;
        [SerializeField] private WordContainer wordContainer;

        private const int MaxLetters = 26;
        private readonly List<LetterBlock> _letterBlocks = new();
        private LetterObjectPool _letterPool;
        private LetterSpaceUi _letterSpaceUi;
        private LetterBlock _heldLetter;

        #region Public Methods

        public void Initialize(GameObject parent)
        {
            _letterSpaceUi = Object.FindFirstObjectByType<LetterSpaceUi>();
            _letterPool = parent.GetComponent<LetterObjectPool>();
            SetArrangementMode();
        }

        public void Tick()
        {
            for (var i = 0; i < _letterBlocks.Count; i++)
            {
                if (_letterBlocks[i].IsDragging == false) continue;
                _letterBlocks[i].MoveLetter(inputDetection.GetMouseWorldPosition());
                break;
            }
        }

        public void Dispose()
        {
            _letterBlocks?.Clear();
        }

        public void UpdateBlockArrangement(int change = 0)
        {
            RemoveBlocks();
            SetArrangementMode(change);
            PlaceBlocks();
        }

        public void PlaceBlocks()
        {
            switch (currentMode)
            {
                case PositionMode.Categories:
                case PositionMode.Alphabet:
                    PlaceLetterBlocks();
                    break;
                case PositionMode.Qwerty:
                    PlaceLetterBlocks(letterPositions.GetQwertyIndicies());
                    break;
                default:
                    PlaceLetterBlocks(letterPositions.GetRandomIndicies(wordContainer.GetWord()));
                    break;
            }
        }

        public void RemoveBlocks()
        {
            if (ReferenceEquals(_letterBlocks, null)) return;
            if (_letterBlocks.Count <= 0) return;

            for (var i = _letterBlocks.Count - 1; i >= 0; i--)
            {
                var letter = _letterBlocks[i];
                _letterBlocks.RemoveAt(i);
                letter.OnDespawn();
                _letterPool.ReturnLetter(letter);
            }
        }

        public void HighlightBlock(char letter)
        {
            for (int i = 0; i < _letterBlocks.Count; i++)
            {
                if (letter >= 97) letter = (char)(letter - 32);
                if (_letterBlocks[i].IsLetter(letter) == false) continue;
                _letterBlocks[i].BlinkAnimation(true);
            }
        }

        public void GrabBlock(LetterBlock letterBlock)
        {
            _heldLetter = letterBlock;
            for (var i = 0; i < _letterBlocks.Count; i++)
            {
                if (ReferenceEquals(_letterBlocks[i], _heldLetter)) continue;
                _letterBlocks[i].DisableCollision();
            }

            _heldLetter.OnGrabbed();
        }

        public bool ReleaseBlock()
        {
            for (var i = 0; i < _letterBlocks.Count; i++)
            {
                if (_letterBlocks[i].IsDetectable) continue;
                _letterBlocks[i].EnableCollision();
            }

            return _heldLetter.OnReleased();
        }

        #endregion

        #region Private Methods

        private void SetArrangementMode(int change = 0)
        {
            currentMode = change switch
            {
                0 => (PositionMode)gameDifficulty.Value,
                1 => PositionMode.Alphabet,
                2 => PositionMode.Qwerty,
                _ => PositionMode.Categories
            };

            if (currentMode != PositionMode.Categories) _letterSpaceUi.TurnOnMainUi();
            else _letterSpaceUi.TurnOnSubUi();
        }

        private void PlaceLetterBlocks()
        {
            var ascii = 65;
            var letterSize = letterPositions.GetScale(currentMode);
            for (var i = 0; i < MaxLetters; i++)
            {
                var letter = _letterPool.GetLetter();
                letter.OnSpawn(letterPositions.GetPositionAt(currentMode, i), ascii);
                letter.transform.localScale = letterSize;
                _letterBlocks.Add(letter);
                ascii++;
            }
        }

        private void PlaceLetterBlocks(int[] asciiArray)
        {
            var letterSize = letterPositions.GetScale(currentMode);
            for (var i = 0; i < asciiArray.Length; i++)
            {
                if (i >= MaxLetters) break;
                var letter = _letterPool.GetLetter();
                letter.OnSpawn(letterPositions.GetPositionAt(currentMode, i), asciiArray[i]);
                letter.transform.localScale = letterSize;
                _letterBlocks.Add(letter);
            }
        }

        #endregion
    }
}
