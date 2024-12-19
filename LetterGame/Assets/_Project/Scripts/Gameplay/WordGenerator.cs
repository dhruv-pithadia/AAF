
using LetterQuest.Gameplay.IO;

namespace LetterQuest.Gameplay.Words
{
    public class WordGenerator
    {
        private readonly TextSerializer _textSerializer = new();
        private string[] _currentWords;
        private int _currentDifficulty;
        private int _currentWordIndex;

        #region Public Methods

        public string GetNextWord()
        {
            if (_currentWordIndex >= _currentWords.Length) return string.Empty;
            var result = _currentWords[_currentWordIndex];
            _currentWordIndex++;
            return result;
        }

        public void SetWordDifficulty(int difficulty = 0)
        {
            _currentDifficulty = difficulty;
            AssignWordsUsingDifficulty();
        }

        public bool GoToNextDifficulty()
        {
            _currentDifficulty++;
            if (_currentDifficulty > 2) return false;
            AssignWordsUsingDifficulty();
            return true;
        }

        public void Dispose()
        {
            _currentWords = null;
        }

        #endregion

        #region Private Methods

        private void AssignWordsUsingDifficulty()
        {
            _currentWords = _textSerializer.LoadTextFile(_currentDifficulty);
            Shuffle(_currentWords);
            _currentWordIndex = 0;
        }

        private void Shuffle(string[] text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                var tmpWord = text[i];
                var index = UnityEngine.Random.Range(i, text.Length);
                text[i] = text[index];
                text[index] = tmpWord;
            }
        }

        #endregion
    }
}
