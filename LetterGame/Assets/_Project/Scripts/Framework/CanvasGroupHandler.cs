
using UnityEngine;

namespace LetterQuest.Framework.Ui
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class CanvasGroupHandler : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected void ToggleUi()
        {
            if (_canvasGroup.alpha == 1f)
            {
                _canvasGroup.alpha = 0f;
                _canvasGroup.blocksRaycasts = false;
                _canvasGroup.interactable = false;
            }
            else
            {
                _canvasGroup.alpha = 1f;
                _canvasGroup.blocksRaycasts = true;
                _canvasGroup.interactable = true;
            }
        }
    }
}
