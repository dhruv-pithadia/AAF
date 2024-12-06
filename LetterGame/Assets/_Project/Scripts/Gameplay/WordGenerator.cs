
using Random = UnityEngine.Random;

namespace LetterQuest.Gameplay
{
    public class WordGenerator
    {
        private readonly TextSerializer _textSerializer = new();
        private string[] _currentWords;
        private int _currentWordIndex;

        #region Public Methods

        public string GetNextWord()
        {
            if (_currentWordIndex >= _currentWords.Length) return string.Empty;
            var result = _currentWords[_currentWordIndex];
            _currentWordIndex++;
            return result;
        }

        public void SetWordDifficulty(int difficulty)
        {
            _currentWords = _textSerializer.LoadTextFile(difficulty);
            Shuffle(_currentWords);
            _currentWordIndex = 0;
        }

        public void Dispose()
        {
            _currentWords = null;
        }

        #endregion

        #region Private Methods

        private void Shuffle(string[] text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                var tmpWord = text[i];
                var index = Random.Range(i, text.Length);
                text[i] = text[index];
                text[index] = tmpWord;
            }
        }

        #endregion
    }
}
