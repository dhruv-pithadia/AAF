
using TMPro;
using UnityEngine;
using LetterQuest.Framework.Ui;
using LetterQuest.Gameplay.Core;
using LetterQuest.Gameplay.Events;

namespace LetterQuest.Gameplay.Ui
{
    public class BreakUi : CanvasGroupHandler
    {
        [SerializeField] private EventBus eventBus;
        [SerializeField] private BreakAlerts breakAlerts;
        [SerializeField] private TMP_Text breakText;

        private void Start()
        {
            breakAlerts.Initialize(breakText);
        }

        private void LateUpdate()
        {
            if (breakAlerts.Tick(Time.deltaTime) == false) return;
            Invoke(nameof(HideBreakAlert), 30f);
        }

        private void OnDisable()
        {
            DisposeInvoke();
        }

        public void Pause(bool showUi)
        {
            if (showUi) ShowUi();
            DisposeInvoke();
            HideBreakAlert();
            eventBus.OnBreak(true);
        }

        public void Resume()
        {
            eventBus.OnBreak(false);
            HideUi();
        }

        private void HideBreakAlert()
        {
            breakText.gameObject.SetActive(false);
        }

        private void DisposeInvoke()
        {
            if (IsInvoking(nameof(HideBreakAlert)) == false) return;
            CancelInvoke(nameof(HideBreakAlert));
        }
    }
}
