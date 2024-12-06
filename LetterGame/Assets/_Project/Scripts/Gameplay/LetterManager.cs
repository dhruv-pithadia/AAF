
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay
{
    public class LetterManager : MonoBehaviour
    {
        [SerializeField] private LetterObjectPool letterObjectPool;
        [SerializeField] private List<Transform> letterPositions = new();
        [SerializeField] private int maxLetters = 26;
        private readonly List<Letter> _letterList = new();

        #region Unity Methods

        private void Start()
        {
            int ascii = 65; //  65 = A | 90 = Z
            for (var i = 0; i < maxLetters; i++)
            {
                var letter = letterObjectPool.GetLetter();
                letter.OnSpawn(letterPositions[i].position, ascii);
                _letterList.Add(letter);
                ascii++;
            }
        }

        private void OnDestroy()
        {
            if (ReferenceEquals(_letterList, null)) return;
            if (_letterList.Count <= 0) return;
            _letterList.Clear();
        }

        #endregion
    }
}
