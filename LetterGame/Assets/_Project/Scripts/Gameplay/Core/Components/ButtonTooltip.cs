
using UnityEngine;
using LetterQuest.Gameplay.Ui;
using UnityEngine.EventSystems;

namespace LetterQuest
{
    public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private string tooltip;
        [SerializeField] private TextLabelUi tooltipText;

        public void OnPointerEnter(PointerEventData eventData)
        {
            tooltipText.ShowText(tooltip);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            tooltipText.HideText();
        }
    }
}
