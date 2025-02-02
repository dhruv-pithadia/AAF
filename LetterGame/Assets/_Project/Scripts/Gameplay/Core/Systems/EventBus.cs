
using UnityEngine;

namespace LetterQuest.Gameplay.Events
{
    [CreateAssetMenu(fileName = "EventBus", menuName = "LetterQuest/Event Bus")]
    public class EventBus : ScriptableObject
    {
        public delegate void NotificationDelegate();
        public delegate void ToggleDelegate(bool toggle);

        public event NotificationDelegate LoginSuccessEvent;
        public event NotificationDelegate WordCompleteEvent;
        public event NotificationDelegate WordResetEvent;
        public event NotificationDelegate WordSetEvent;
        public event ToggleDelegate BreakEvent;

        public void OnBreak(bool toggle) => BreakEvent?.Invoke(toggle);
        public void OnWordSet() => WordSetEvent?.Invoke();
        public void OnWordReset() => WordResetEvent?.Invoke();
        public void OnWordCompleted() => WordCompleteEvent?.Invoke();
        public void OnLoginSuccess() => LoginSuccessEvent?.Invoke();
    }
}
