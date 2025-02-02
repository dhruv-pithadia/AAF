
using UnityEngine;
using LetterQuest.Gameplay.Letters.Manager;

namespace LetterQuest.Core.Hints
{
    public class HintSystem : MonoBehaviour
    {
        private LetterManager _letterManager;

        private void Start()
        {
            _letterManager = FindFirstObjectByType<LetterManager>();
        }

        public void ShowHint()
        {
            _letterManager.HighlightNextLetter();
        }
    }
}
