
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Letters.Data
{
    [CreateAssetMenu(fileName = "LetterPositioning", menuName = "LetterQuest/Letter Positioning")]
    public class LetterPositioning : ScriptableObject
    {
        public List<Vector3> alphabetPositions = new();
        public List<Vector3> qwertyPositions = new();
        public List<Vector3> easyLevelPositions = new();
        public List<Vector3> medLevelPositions = new();
        public List<Vector3> hardLevelPositions = new();

        [SerializeField] private Vector3 smallScale;
        [SerializeField] private Vector3 mediumScale;
        [SerializeField] private Vector3 largeScale;
        [SerializeField] private int[] qwertyIndicies;
        private int[] _randomIndicies;

        #region Public Methods

        public int[] GetQwertyIndicies() => qwertyIndicies;

        public int[] GetRandomIndicies(string word)
        {
            SetupRandomIndicies(word.Length * 2);
            AssignRandomIndicies(word);
            return ShuffleIndicies(_randomIndicies);
        }

        public Vector3 GetPositionAt(LetterPositioningMode mode, int index)
        {
            return mode switch
            {
                LetterPositioningMode.Qwerty => qwertyPositions[index],
                LetterPositioningMode.Easy => easyLevelPositions[index],
                LetterPositioningMode.Medium => medLevelPositions[index],
                LetterPositioningMode.Hard => hardLevelPositions[index],
                _ => alphabetPositions[index]
            };
        }

        public Vector3 GetScale(LetterPositioningMode mode)
        {
            return mode switch
            {
                LetterPositioningMode.Medium => mediumScale,
                LetterPositioningMode.Easy => largeScale,
                _ => smallScale
            };
        }

        #endregion

        #region Private Methods

        private static int GetRandomAlphabetIndex() => Random.Range(65, 91);

        private void SetupRandomIndicies(int length)
        {
            if (ReferenceEquals(_randomIndicies, null) || _randomIndicies.Length != length)
            {
                _randomIndicies = new int[length];
            }

            for (var i = 0; i < _randomIndicies.Length; i++)
            {
                _randomIndicies[i] = 0;
            }
        }

        private void AssignRandomIndicies(string word)
        {
            for (var i = 0; i < word.Length; i++)
            {
                if (IsDuplicate(word[i])) continue;
                _randomIndicies[i] = word[i];
            }

            for (var i = 0; i < _randomIndicies.Length; i++)
            {
                while (_randomIndicies[i] == 0)
                {
                    var randomIndex = GetRandomAlphabetIndex();
                    if (IsDuplicate(randomIndex) == false) _randomIndicies[i] = randomIndex;
                }
            }
        }

        private int[] ShuffleIndicies(int[] indicies)
        {
            for (var i = 0; i < indicies.Length; i++)
            {
                var tmpIndex = indicies[i];
                var index = Random.Range(i, indicies.Length);
                indicies[i] = indicies[index];
                indicies[index] = tmpIndex;
            }

            return indicies;
        }

        private bool IsDuplicate(int entry)
        {
            for (var i = 0; i < _randomIndicies.Length; i++)
            {
                if (_randomIndicies[i] == entry) return true;
            }

            return false;
        }

        #endregion
    }

    public enum LetterPositioningMode : byte
    {
        Easy,
        Medium,
        Hard,
        Alphabet,
        Qwerty
    }
}
