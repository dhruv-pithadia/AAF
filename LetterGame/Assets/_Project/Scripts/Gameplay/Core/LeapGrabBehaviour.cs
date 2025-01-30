
using UnityEngine;
using Leap.PhysicalHands;
using LetterQuest.Gameplay.Letters;

namespace LetterQuest.Gameplay.Hands
{
    public class LeapGrabBehaviour : MonoBehaviour
    {
        private LetterBlockGrab _letterBlockGrab;

        private void Awake()
        {
            _letterBlockGrab = FindFirstObjectByType<LetterBlockGrab>();
        }

        public void OnGrabEnterEvent(ContactHand hand, Rigidbody obj)
        {
            if (_letterBlockGrab.IsInUse || obj.CompareTag("Letter") == false) return;
            _letterBlockGrab.OnGrabLetter(obj.GetComponent<LetterBlock>());
            _letterBlockGrab.Grabbing();
        }

        public void OnGrabExitEvent(ContactHand hand, Rigidbody obj)
        {
            if (_letterBlockGrab.IsPointing || _letterBlockGrab.IsInUse == false || obj.CompareTag("Letter") == false) return;
            _letterBlockGrab.OnReleaseLetter();
        }
    }
}
