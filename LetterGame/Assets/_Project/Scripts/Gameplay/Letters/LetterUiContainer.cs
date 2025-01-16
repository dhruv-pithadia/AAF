
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Letters.Ui
{
    public class LetterUiContainer : MonoBehaviour
    {
        [SerializeField] private List<LetterSlotUi> letterSlotUis = new();
        
        public LetterSlotUi[] GetLetterSlots() => letterSlotUis.ToArray();
    }
}
