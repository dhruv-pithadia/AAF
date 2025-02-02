
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using LetterQuest.Gameplay.Core;
using LetterQuest.Gameplay.Animation;
using LetterQuest.Gameplay.Letters.Ui;

namespace LetterQuest.Gameplay.Letters
{
    public class LetterBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private UiEventHandler uiEventHandler;
        public bool IsDragging { get; private set; }
        private LetterBlockGrab _letterBlockGrab;
        private AnimatorHook _animatorHook;
        private Transform _letterTransform;
        private BoxCollider _boxCollider;
        private TMP_Text _letterText;
        private Vector3 _originalPos;
        private int _asciiCode;
        private float _timer;

        #region Unity Methods

        private void Awake()
        {
            _letterTransform = transform;
            canvas.worldCamera = Camera.main;
            _boxCollider = GetComponent<BoxCollider>();
            _animatorHook = GetComponent<AnimatorHook>();
            _letterText = GetComponentInChildren<TMP_Text>();
            _letterBlockGrab = FindFirstObjectByType<LetterBlockGrab>();
        }

        #endregion

        #region Public Methods

        public bool IsLetter(char letter) => letter == (char)_asciiCode;
        public bool IsDetectable => _boxCollider.enabled;
        public Vector3 GetPosition() => _letterTransform.position;
        public void EnableCollision() => _boxCollider.enabled = true;
        public void DisableCollision() => _boxCollider.enabled = false;
        public void MoveLetter(Vector3 position) => _letterTransform.position = position;

        public void OnSpawn(Vector3 position, int ascii)
        {
            _asciiCode = ascii;
            _originalPos = position;
            MoveLetter(_originalPos);
            AssignLetterText(ConvertAsciiToString());
        }

        public void OnDespawn()
        {
            AssignLetterText(string.Empty);
            MoveLetter(Vector3.zero);
        }

        public void BlinkAnimation(bool invoke = false)
        {
            if (IsInvoking(nameof(StopAnimation)))
            {
                if (invoke) return;
                CancelInvoke(nameof(StopAnimation));
            }

            _animatorHook.Play(true);
            if (invoke == false) return;
            Invoke(nameof(StopAnimation), 6f);
        }

        public void StopAnimation()
        {
            _animatorHook.Play(false);
        }

        public void OnGrabbed()
        {
            BlinkAnimation();
            meshRenderer.enabled = false;
        }

        public bool OnReleased()
        {
            StopAnimation();
            meshRenderer.enabled = true;
            var uiSlot = uiEventHandler.GetUiAtPosition(_letterTransform.position, "LetterSlot");
            _letterTransform.SetPositionAndRotation(_originalPos, Quaternion.identity);
            if (ReferenceEquals(uiSlot, null)) return false;
            uiSlot.GetComponent<LetterSlotUi>().SetLetterSlotText(ConvertAsciiToString());
            return true;
        }

        #endregion

        #region Interface Methods

        public void OnPointerDown(PointerEventData eventData)
        {
            IsDragging = true;
            _letterBlockGrab.OnGrabLetter(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _letterBlockGrab.OnReleaseLetter();
            IsDragging = false;
        }

        #endregion

        #region Private Methods

        private void AssignLetterText(string text) => _letterText.text = text;
        private string ConvertAsciiToString() => ((char)_asciiCode).ToString();

        #endregion
    }
}
