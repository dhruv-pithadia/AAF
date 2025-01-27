
using UnityEngine;

namespace LetterQuest.Gameplay.Events
{
    [CreateAssetMenu(fileName = "EventBus", menuName = "LetterQuest/Event Bus")]
    public class EventBus : ScriptableObject
    {
        public delegate void NotificationDelegate();

        public event NotificationDelegate LoginSuccessEvent;
        public event NotificationDelegate WordCompleteEvent;
        public event NotificationDelegate WordResetEvent;
        public event NotificationDelegate WordSetEvent;

        public void OnWordSet() => WordSetEvent?.Invoke();
        public void OnWordReset() => WordResetEvent?.Invoke();
        public void OnWordCompleted() => WordCompleteEvent?.Invoke();
        public void OnLoginSuccess() => LoginSuccessEvent?.Invoke();
    }
}
