
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay
{
    public class LetterManager : MonoBehaviour
    {
        private const int MaxLetters = 26;
        [SerializeField] private LetterObjectPool letterObjPool;
        [SerializeField] private List<Transform> letterPositions = new();
        private readonly List<LetterBlock> _letterList = new();

        #region Unity Methods

        private void Start()
        {
            int ascii = 65; //  65 = A | 90 = Z
            for (var i = 0; i < MaxLetters; i++)
            {
                var letter = letterObjPool.GetLetter();
                letter.OnSpawn(letterPositions[i].position, ascii);
                _letterList.Add(letter);
                ascii++;
            }
        }

        private void LateUpdate()
        {
            for (var i = 0; i < _letterList.Count; i++)
            {
                if (_letterList[i].IsDragging == false) continue;
                //  TODO: do drag movement here
            }
        }

        private void OnDisable()
        {
            _letterList?.Clear();
        }

        #endregion

        #region Public Methods

        public void RemoveLetters()
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
