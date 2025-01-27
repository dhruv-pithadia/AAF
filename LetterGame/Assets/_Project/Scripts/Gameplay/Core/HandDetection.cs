
using UnityEngine;
using LetterQuest.Gameplay.Input;
using LetterQuest.Gameplay.Letters;
using LetterQuest.Gameplay.Letters.Manager;

namespace LetterQuest.Gameplay.Core
{
    public class HandDetection : MonoBehaviour
    {
        [SerializeField] private GrabMethod grabMethod;
        private LetterBlock heldBlock;
        private Transform myTransform;
        private float _timer;
        private int _counter;
        private bool _isHeld;

        #region Unity Methods

        private void Awake()
        {
            myTransform = transform;
            grabMethod.Initialize(FindFirstObjectByType<LetterManager>());
        }

        private void OnDisable()
        {
            if (ReferenceEquals(heldBlock, null)) return;
            ReleaseLetter();
        }

        private void Update()
        {
            if (_isHeld == false || grabMethod.IsPointing == false) return;
            HeldLetterTick();
        }

        private void OnTriggerEnter(Collider other)
        {
            _timer = 1f;
        }

        private void OnTriggerStay(Collider other)
        {
            if (grabMethod.IsInUse || other.CompareTag("Letter") == false) return;
            _timer -= Time.deltaTime;
            if (_timer > 0f) return;
            HoldLetter(other.GetComponent<LetterBlock>());
        }

        #endregion

        public Vector3 GrabPoint() => myTransform.position;

        private void HoldLetter(LetterBlock letterBlock)
        {
            heldBlock = letterBlock;
            heldBlock.OnHeldStart(this);
            grabMethod.OnGrabLetter(letterBlock);
            grabMethod.Pointing();
            _timer = 0.2f;
            _isHeld = true;
        }

        private void HeldLetterTick()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0f) return;

            _timer = 0.2f;
            if (InputDetection.IsOverUiSlot(myTransform.position)) _counter++;
            else _counter = 0;

            if (_counter != 3) return;
            _counter = 0;
            ReleaseLetter();
        }

        private void ReleaseLetter()
        {
            _isHeld = false;
            heldBlock.OnHeldEnd();
            grabMethod.OnReleaseLetter();
            heldBlock = null;
        }
    }
}
