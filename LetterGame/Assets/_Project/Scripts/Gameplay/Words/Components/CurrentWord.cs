
using TMPro;
using UnityEngine;
using LetterQuest.Gameplay.Animation;
using LetterQuest.Gameplay.Words.Data;

namespace LetterQuest.Gameplay.Words
{
    public class CurrentWord : MonoBehaviour
    {
        [SerializeField] private TMP_Text currentWordText;
        [SerializeField] private WordContainer wordContainer;
        [SerializeField] private AnimatorHook wordAnimatorHook;

        private void Awake()
        {
            wordContainer.Initialize(this);
        }

        public void SetWord(string word)
        {
            currentWordText.text = word;
        }

        public void PlayAnimation()
        {
            wordAnimatorHook.Play();
        }
    }
}
