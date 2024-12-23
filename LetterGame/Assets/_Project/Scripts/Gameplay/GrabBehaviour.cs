
using UnityEngine;
using Leap.PhysicalHands;
using LetterQuest.Gameplay.Letters;
using LetterQuest.Gameplay.Letters.Manager;

namespace LetterQuest.Gameplay.Hands
{
    public class GrabBehaviour : MonoBehaviour
    {
        [SerializeField] private LetterManager letterManager;
        
        public void OnGrabEnterEvent(ContactHand hand, Rigidbody obj)
        {
            if (obj.CompareTag("Letter") == false) return;
            var letterBlock = obj.GetComponent<LetterBlock>();
            letterManager.TurnOffCollidersExcept(letterBlock);
            letterBlock.OnDragStart();
        }

        public void OnGrabExitEvent(ContactHand hand, Rigidbody obj)
        {
            if (obj.CompareTag("Letter") == false) return;
            letterManager.TurnOnColliders();
            obj.GetComponent<LetterBlock>().OnDragEnd(hand.palmBone.transform.position);
        }
    }
}
