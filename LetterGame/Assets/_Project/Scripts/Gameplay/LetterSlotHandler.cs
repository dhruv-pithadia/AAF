
using UnityEngine;

namespace LetterQuest.Gameplay
{
    public class LetterSlotHandler : MonoBehaviour
    {
        [SerializeField] private LetterSlot[] letterSlots;

        private void Start()
        {
            for (int i = 0; i < letterSlots.Length; i++)
            {
                letterSlots[i].LetterSlotUpdateEvent += OnLetterSlotUpdate;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < letterSlots.Length; i++)
            {
                letterSlots[i].LetterSlotUpdateEvent -= OnLetterSlotUpdate;
            }
        }

        private void OnLetterSlotUpdate()
        {
            //  TODO: instead of slot length use current word length
            for (int i = 0; i < letterSlots.Length; i++)
            {
                if (letterSlots[i].IsAssigned() == false) return;
            }

            //  TODO: all letters are assigned if here, check that letters match

            //  TODO: if all letters match current word, proceed to next word
        }

        public void ResetAllSlots()
        {
            for (int i = 0; i < letterSlots.Length; i++)
            {
                if (letterSlots[i].IsAssigned() == false) continue;
                letterSlots[i].ResetLetterSlotText();
            }
        }
    }
}
