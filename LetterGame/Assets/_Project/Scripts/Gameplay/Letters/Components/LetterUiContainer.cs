
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Letters.Ui
{
    public class LetterSlotContainer : MonoBehaviour
    {
        [SerializeField] private List<LetterSlotUi> letterSlotUis = new();

        public List<LetterSlotUi> GetLetterSlots() => letterSlotUis;
    }
}
