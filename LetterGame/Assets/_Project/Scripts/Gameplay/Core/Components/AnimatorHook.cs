
using UnityEngine;

namespace LetterQuest.Gameplay.Animation
{
    public class AnimatorHook : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string triggerName;

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
