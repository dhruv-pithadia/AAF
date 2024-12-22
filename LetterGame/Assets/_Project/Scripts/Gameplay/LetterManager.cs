
using TMPro;
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
            currentWordData.WordCompleteEvent += OnWordComplete;
            SetupPositioningMode();
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
            currentWordData.WordCompleteEvent -= OnWordComplete;
            currentWordData.WordAssignedEvent -= OnWordAssigned;
            inputDetection.Dispose();
            _letterList?.Clear();
        }

        #endregion

        public void PickLetterArrangement(TMP_Dropdown change)
        {
            RemoveLetters();
            if(change.value == 0) SetupPositioningMode();
            else if(change.value == 1) mode = LetterPositioningMode.Alphabet;
            else mode = LetterPositioningMode.Qwerty;
            PlaceLetters();
        }

        #region Private Methods

        private void SetupPositioningMode()
        {
            mode = (LetterPositioningMode)gameDifficulty.Value;
        }

        private void OnWordAssigned()
        {
            PlaceLetters();
        }

        private void PlaceLetters()
        {
            switch (mode)
            {
                case LetterPositioningMode.Alphabet:
                    //Debug.Log("[LetterManager]: Placing letters alphabetically");
                    PlaceLettersAlphabetically();
                    break;
                case LetterPositioningMode.Qwerty:
                    //Debug.Log("[LetterManager]: Placing letters qwerty");
                    PlaceLetters(currentWordData.GetQwertyIndicies());
                    break;
                default:
                    //Debug.Log("[LetterManager]: Placing letters randomly");
                    PlaceLetters(currentWordData.GetRandomIndicies());
                    break;
            }
        }

        private void PlaceLettersAlphabetically()
        {
            var ascii = 65; //  65 = A | 90 = Z
            for (var i = 0; i < MaxLetters; i++)
            {
                var letter = letterObjPool.GetLetter();
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
                var letter = letterObjPool.GetLetter();
                letter.OnSpawn(letterPositions.GetPositionAt(mode, i), asciiArray[i]);
                letter.transform.localScale = letterPositions.GetScale(mode);
                _letterList.Add(letter);
            }
        }

        private void OnWordComplete()
        {
            RemoveLetters();
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
