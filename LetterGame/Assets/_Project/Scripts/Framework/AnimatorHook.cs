
using UnityEngine;

namespace LetterQuest.Framework.Animation
{
    public class AnimatorHook : MonoBehaviour
    {
        [SerializeField] private string triggerName;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            if (ReferenceEquals(animator, null) == false) return;
            animator = GetComponent<Animator>();
            if (ReferenceEquals(animator, null) == false) return;
            animator = GetComponentInChildren<Animator>();
        }

        public void Play()
        {
            animator.SetTrigger(triggerName);
            //Debug.Log($"[AnimatorHook]: playing trigger - {triggerName}");
        }
    }
}
