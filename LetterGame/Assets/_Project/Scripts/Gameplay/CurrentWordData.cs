
using UnityEngine;
using Random = UnityEngine.Random;

namespace LetterQuest.Gameplay.Words.Data
{
    [CreateAssetMenu(fileName = "Current Word Data", menuName = "LetterQuest/CurrentWordData")]
    public class CurrentWordData : ScriptableObject
    {
        [SerializeField] private int[] qwertyIndicies;
        public delegate void WordAssigned();
        public event WordAssigned WordAssignedEvent;
        private int[] randomIndicies;
        private string currentWord;

        #region Public Methods

        public string GetText() => currentWord;
        public string GetTextUpperCase() => currentWord.ToUpper();
        public int[] GetQwertyIndicies() => qwertyIndicies;

        public int[] GetRandomIndicies()
        {
            var wordUpperCase = GetTextUpperCase();
            SetupRandomIndicies(wordUpperCase.Length * 2);
            AssignRandomIndicies(wordUpperCase);
            return ShuffleIndicies(randomIndicies);
        }

        public void SetCurrentWord(string word)
        {
            currentWord = word;
            WordAssignedEvent?.Invoke();
        }

        #endregion

        #region Private Methods

        private int GetRandomAlphabetIndex() => Random.Range(65, 91);

        private void SetupRandomIndicies(int length)
        {
            if (ReferenceEquals(randomIndicies, null) || randomIndicies.Length != length)
            {
                randomIndicies = new int[length];
            }

            for (var i = 0; i < randomIndicies.Length; i++)
            {
                randomIndicies[i] = 0;
            }
        }

        private void AssignRandomIndicies(string word)
        {
            for (var i = 0; i < word.Length; i++)
            {
                if (IsDuplicate(word[i])) continue;
                randomIndicies[i] = word[i];
            }

            for (var i = 0; i < randomIndicies.Length; i++)
            {
                while (randomIndicies[i] == 0)
                {
                    var randomIndex = GetRandomAlphabetIndex();
                    if (IsDuplicate(randomIndex) == false) randomIndicies[i] = randomIndex;
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
            for (var i = 0; i < randomIndicies.Length; i++)
            {
                if (randomIndicies[i] == entry) return true;
            }

            return false;
        }

        #endregion
    }
}
