
namespace LetterQuest.Gameplay.Words.Data
{
    [System.Serializable]
    public class WordTrackingData
    {
        private int maxWordCount;
        private int currentWordCount;

        public WordTrackingData(int currentWordCount, int maxWordCount)
        {
            this.maxWordCount = maxWordCount;
            this.currentWordCount = currentWordCount;
        }

        public void Tick() => currentWordCount++;
        public float GetPercent => (float)currentWordCount / maxWordCount;
        public string GetText() => $"{currentWordCount} / {maxWordCount}";
    }
}
