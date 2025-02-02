
using UnityEngine;
using Leap.PhysicalHands;
using LetterQuest.Gameplay.Letters;

namespace LetterQuest.Gameplay.Hands
{
    public class LeapGrabBehaviour : MonoBehaviour
    {
        private LetterBlockGrab _blockGrab;

        private void Awake()
        {
            _blockGrab = FindFirstObjectByType<LetterBlockGrab>();
        }

        public void OnGrabEnterEvent(ContactHand hand, Rigidbody obj)
        {
            if (_blockGrab.IsInUse || obj.CompareTag("Letter") == false) return;
            _blockGrab.OnGrabLetter(obj.GetComponent<LetterBlock>());
            _blockGrab.Grabbing();
        }

        public void OnGrabExitEvent(ContactHand hand, Rigidbody obj)
        {
            if (_blockGrab.IsInUse == false || _blockGrab.IsPointing || obj.CompareTag("Letter") == false) return;
            _blockGrab.OnReleaseLetter();
        }
    }
}
