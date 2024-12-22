
using UnityEngine;

namespace LetterQuest.Framework.Animation
{
    public class AnimatorHook : MonoBehaviour
    {
        [SerializeField] private string triggerName;
        [SerializeField] private Animator animator;

        public void Play()
        {
            animator.SetTrigger(triggerName);
        }

        public void Play(bool value)
        {
            animator.SetBool(triggerName, value);
        }
    }
}
