
using UnityEngine;

namespace LetterQuest.Gameplay.Words.Data
{
    [CreateAssetMenu(fileName = "WordContainer", menuName = "LetterQuest/Word Container")]
    public class WordContainer : ScriptableObject
    {
        private string _wordText;
        private CurrentWord _currentWord;

        public string GetWord() => _wordText;
        public int GetWordLength() => _wordText.Length;
        public bool DoesLetterMatch(int index, char letter) => _wordText[index] == letter;

        public void Initialize(CurrentWord currentWord)
        {
            _currentWord = currentWord;
        }
        
        public void SetWord(string word, bool playAnimation = false)
        {
            _currentWord.SetWord(word);
            _wordText = word.ToUpper();
            if (playAnimation == false) return;
            PlayWordAnimation();
        }

        public void PlayWordAnimation()
        {
            _currentWord.PlayAnimation();
        }
    }
}
