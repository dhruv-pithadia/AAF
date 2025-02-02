
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Letters.Data
{
    [CreateAssetMenu(fileName = "LetterPositioning", menuName = "LetterQuest/Letter Positioning")]
    public class LetterPositioning : ScriptableObject
    {
        [SerializeField] private List<Vector3> qwertyPositions;
        [SerializeField] private List<Vector3> alphabetPositions;
        [SerializeField] private List<Vector3> categoryPositions;
        [SerializeField] private List<Vector3> hardLevelPositions;
        [SerializeField] private List<Vector3> medLevelPositions;
        [SerializeField] private List<Vector3> easyLevelPositions;
        [SerializeField] private int[] qwertyIndicies;
        [SerializeField] private Vector3 smallScale;
        [SerializeField] private Vector3 mediumScale;
        [SerializeField] private Vector3 largeScale;
        private int[] _randomIndicies;

        #region Public Methods

        public int[] GetQwertyIndicies() => qwertyIndicies;

        public int[] GetRandomIndicies(string word)
        {
            SetupRandomIndicies(word.Length * 2);
            AssignRandomIndicies(word);
            return ShuffleIndicies(_randomIndicies);
        }

        public Vector3 GetPositionAt(PositionMode mode, int index)
        {
            return mode switch
            {
                PositionMode.Qwerty => qwertyPositions[index],
                PositionMode.Easy => easyLevelPositions[index],
                PositionMode.Medium => medLevelPositions[index],
                PositionMode.Hard => hardLevelPositions[index],
                PositionMode.Categories => categoryPositions[index],
                _ => alphabetPositions[index]
            };
        }

        public Vector3 GetScale(PositionMode mode)
        {
            return mode switch
            {
                PositionMode.Medium => mediumScale,
                PositionMode.Easy => largeScale,
                _ => smallScale
            };
        }

        #endregion

        #region Private Methods

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

        private bool IsDuplicate(int entry)
        {
            for (var i = 0; i < _randomIndicies.Length; i++)
            {
                if (_randomIndicies[i] != entry) continue;
                return true;
            }

            return false;
        }

        private static int GetRandomAlphabetIndex() => Random.Range(65, 91);

        private static int[] ShuffleIndicies(int[] indicies)
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

        #endregion
    }

    public enum PositionMode : byte
    {
        Easy,
        Medium,
        Hard,
        Alphabet,
        Qwerty,
        Categories
    }
}
