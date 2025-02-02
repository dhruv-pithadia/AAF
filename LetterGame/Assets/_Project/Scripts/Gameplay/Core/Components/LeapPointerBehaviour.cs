
using UnityEngine;
using LetterQuest.Gameplay.Letters;

namespace LetterQuest.Gameplay.Core
{
    public class LeapPointerBehaviour : MonoBehaviour
    {
        [SerializeField] private UiEventHandler uiEventHandler;
        private const float HoverTimer = 0.25f;
        private const int HitsNeededCount = 4;
        private LetterBlockGrab _blockGrab;
        private Transform _handTransform;
        private LetterBlock _heldBlock;
        private float _timer;
        private int hitCount;
        private bool _isHeld;

        #region Unity Methods

        private void Awake()
        {
            _handTransform = transform;
            uiEventHandler.Initialize(Camera.main);
            _blockGrab = FindFirstObjectByType<LetterBlockGrab>();
        }

        private void OnDestroy()
        {
            uiEventHandler.Dispose();
            if (ReferenceEquals(_heldBlock, null)) return;
            ReleaseLetter();
        }

        private void Update()
        {
            if (_isHeld && _blockGrab.IsPointing)
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
            if (_blockGrab.IsInUse || other.CompareTag("Letter") == false) return;
            _timer -= Time.deltaTime;
            if (_timer > 0f) return;
            HoldLetter(other.GetComponent<LetterBlock>());
        }

        #endregion

        private void HoldLetter(LetterBlock letterBlock)
        {
            _heldBlock = letterBlock;
            _blockGrab.OnGrabLetter(letterBlock);
            _blockGrab.Pointing();
            _timer = HoverTimer;
            _isHeld = true;
        }

        private void HeldLetterTick()
        {
            _timer -= Time.deltaTime;
            _heldBlock.MoveLetter(_handTransform.position);
            if (_timer > 0f) return;

            _timer = HoverTimer;
            if (uiEventHandler.IsOverUi(_heldBlock.GetPosition(), "LetterSlot")) hitCount++;
            else hitCount = 0;

            if (hitCount < HitsNeededCount) return;
            ReleaseLetter();
        }

        private void ReleaseLetter()
        {
            _isHeld = false;
            _blockGrab.OnReleaseLetter();
            _heldBlock = null;
            hitCount = 0;
        }
    }
}
