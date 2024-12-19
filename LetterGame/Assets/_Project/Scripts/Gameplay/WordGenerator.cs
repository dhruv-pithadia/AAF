
using LetterQuest.Gameplay.IO;

namespace LetterQuest.Gameplay.Words
{
    public class WordGenerator
    {
        private readonly TextSerializer _textSerializer = new();
        private string[] _currentWords;
        private int _currentWordIndex;

        public WordGenerator(int difficulty)
        {
            AssignWordsUsingDifficulty(difficulty);
        }

        #region Public Methods

        public string GetNextWord()
        {
            if (_currentWordIndex >= _currentWords.Length) return string.Empty;
            var result = _currentWords[_currentWordIndex];
            _currentWordIndex++;
            return result;
        }

        public void Dispose()
        {
            _currentWords = null;
        }

        #endregion

        #region Private Methods

        private void AssignWordsUsingDifficulty(int difficulty)
        {
            _currentWords = _textSerializer.LoadTextFile(difficulty);
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
