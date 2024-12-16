
using UnityEngine;
using Leap.PhysicalHands;
using LetterQuest.Gameplay;

namespace LetterQuest
{
    public class GrabBehaviour : MonoBehaviour
    {
        public void OnGrabEnterEvent(ContactHand hand, Rigidbody obj)
        {
            if (obj.CompareTag("Letter") == false) return;
            obj.GetComponent<LetterBlock>().OnDragStart();
        }

        public void OnGrabExitEvent(ContactHand hand, Rigidbody obj)
        {
            if (obj.CompareTag("Letter") == false) return;
            obj.GetComponent<LetterBlock>().OnDragEnd(hand.palmBone.transform.position);
        }
    }
}
