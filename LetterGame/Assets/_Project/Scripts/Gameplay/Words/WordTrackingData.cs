
namespace LetterQuest.Gameplay.Words.Data
{
    public class WordTrackingData
    {
        private readonly int _maxWordCount;
        private int _currentWordCount;

        public WordTrackingData(int currentWordCount, int maxWordCount)
        {
            _maxWordCount = maxWordCount;
            _currentWordCount = currentWordCount;
        }

        public void Tick() => _currentWordCount++;
        public float GetPercent => (float)_currentWordCount / _maxWordCount;
        public string GetText() => $"{_currentWordCount} / {_maxWordCount}";
    }
}
