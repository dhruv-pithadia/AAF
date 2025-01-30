
using UnityEngine;
using LetterQuest.Gameplay.Input;
using LetterQuest.Gameplay.Letters;

namespace LetterQuest.Gameplay.Core
{
    public class LeapPointerBehaviour : MonoBehaviour
    {
        private const float HoverTimer = 0.25f;
        private const int HitsNeededCount = 4;
        private LetterBlockGrab _letterBlockGrab;
        private Transform _handTransform;
        private LetterBlock _heldBlock;
        private float _timer;
        private int hitCount;
        private bool _isHeld;

        #region Unity Methods

        private void Awake()
        {
            _handTransform = transform;
            _letterBlockGrab = FindFirstObjectByType<LetterBlockGrab>();
        }

        private void OnDisable()
        {
            if (ReferenceEquals(_heldBlock, null)) return;
            ReleaseLetter();
        }

        private void Update()
        {
            if (_isHeld && _letterBlockGrab.IsPointing)
            {
                HeldLetterTick();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _timer = 1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (_letterBlockGrab.IsInUse || other.CompareTag("Letter") == false) return;
            _timer -= Time.deltaTime;
            if (_timer > 0f) return;
            HoldLetter(other.GetComponent<LetterBlock>());
        }

        #endregion

        private void HoldLetter(LetterBlock letterBlock)
        {
            _heldBlock = letterBlock;
            _letterBlockGrab.OnGrabLetter(letterBlock);
            _letterBlockGrab.Pointing();
            _timer = HoverTimer;
            _isHeld = true;
        }

        private void HeldLetterTick()
        {
            _timer -= Time.deltaTime;
            _heldBlock.MoveLetter(_handTransform.position);
            if (_timer > 0f) return;

            _timer = HoverTimer;
            if (InputDetection.IsOverUi(_heldBlock.GetPosition(), "LetterSlot")) hitCount++;
            else hitCount = 0;

            if (hitCount < HitsNeededCount) return;
            ReleaseLetter();
        }

        private void ReleaseLetter()
        {
            _isHeld = false;
            _letterBlockGrab.OnReleaseLetter();
            _heldBlock = null;
            hitCount = 0;
        }
    }
}
