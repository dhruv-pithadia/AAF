
using UnityEngine;

namespace LetterQuest.Gameplay.Events
{
    [CreateAssetMenu(fileName = "EventBus", menuName = "LetterQuest/Event Bus")]
    public class EventBus : ScriptableObject
    {
        public delegate void StringDelegate(string text);
        public delegate void NotificationDelegate();

        public event NotificationDelegate WordCompleteEvent;
        public event NotificationDelegate WordResetEvent;
        public event StringDelegate WordSetEvent;

        public void OnWordReset() => WordResetEvent?.Invoke();
        public void OnWordCompleted() => WordCompleteEvent?.Invoke();
        public void OnWordSet(string word) => WordSetEvent?.Invoke(word);
    }
}
