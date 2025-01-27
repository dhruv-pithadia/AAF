
using UnityEngine;
using Leap.PhysicalHands;
using LetterQuest.Gameplay.Letters;

namespace LetterQuest.Gameplay.Hands
{
    public class GrabBehaviour : MonoBehaviour
    {
        [SerializeField] private GrabMethod grabMethod;

        public void OnGrabEnterEvent(ContactHand hand, Rigidbody obj)
        {
            if (grabMethod.IsInUse || obj.CompareTag("Letter") == false) return;
            grabMethod.OnGrabLetter(obj.GetComponent<LetterBlock>());
            grabMethod.Grabbing();
        }

        public void OnGrabExitEvent(ContactHand hand, Rigidbody obj)
        {
            if (grabMethod.IsPointing || grabMethod.IsInUse == false || obj.CompareTag("Letter") == false) return;
            grabMethod.OnReleaseLetter();
        }
    }
}
