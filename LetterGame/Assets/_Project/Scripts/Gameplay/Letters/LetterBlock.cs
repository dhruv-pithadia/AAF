
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using LetterQuest.Gameplay.Core;
using System.Collections.Generic;
using LetterQuest.Gameplay.Input;
using LetterQuest.Gameplay.Animation;
using LetterQuest.Gameplay.Letters.Ui;

namespace LetterQuest.Gameplay.Letters
{
    public class LetterBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private Canvas canvas;
        public bool IsDragging { get; private set; }
        private AnimatorHook _animatorHook;
        private Transform _letterTransform;
        private BoxCollider _boxCollider;
        private TMP_Text _letterText;
        private Vector3 _originalPos;
        private HandDetection _holder;
        private float _timer;
        private int _asciiCode;
        private bool _isHeld;

        #region Unity Methods

        private void Awake()
        {
            _letterTransform = transform;
            canvas.worldCamera = Camera.main;
            _boxCollider = GetComponent<BoxCollider>();
            _animatorHook = GetComponent<AnimatorHook>();
            _letterText = GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            if (_isHeld == false) return;
            MoveLetter(_holder.GrabPoint());
        }

        #endregion

        #region Public Methods

        public void MoveLetter(Vector3 position) => _letterTransform.position = position;

        public void OnSpawn(Vector3 position, int ascii)
        {
            _asciiCode = ascii;
            _originalPos = position;
            MoveLetter(_originalPos);
            AssignLetterText(ConvertAsciiToString());
        }

        public bool IsDetectable => _boxCollider.enabled;
        public void EnableCollision() => _boxCollider.enabled = true;
        public void DisableCollision() => _boxCollider.enabled = false;

        public void OnDespawn()
        {
            AssignLetterText(string.Empty);
            MoveLetter(Vector3.zero);
        }

        public void OnHeldStart(HandDetection holder)
        {
            _holder = holder;
            _isHeld = true;
        }

        public void OnHeldEnd()
        {
            _isHeld = false;
            _holder = null;
        }

        public void OnDragStart()
        {
            meshRenderer.enabled = false;
            _animatorHook.Play(true);
        }

        public bool OnDragEnd()
        {
            _animatorHook.Play(false);
            meshRenderer.enabled = true;
            var result = AssignLetterToUiSlot(InputDetection.GetHandOverUi(_letterTransform.position));
            _letterTransform.SetPositionAndRotation(_originalPos, Quaternion.identity);
            return result;
        }

        #endregion

        #region Interface Methods

        public void OnPointerDown(PointerEventData eventData) => BeginLetterDrag();
        public void OnPointerUp(PointerEventData eventData) => FinishLetterDrag(eventData);

        #endregion

        #region Private Methods

        private string ConvertAsciiToString() => ((char)_asciiCode).ToString();
        private void AssignLetterText(string text) => _letterText.text = text;

        private void BeginLetterDrag()
        {
            IsDragging = true;
            OnDragStart();
        }

        private void FinishLetterDrag(PointerEventData eventData)
        {
            IsDragging = false;
            DoUiRaycast(eventData);
            _animatorHook.Play(false);
            meshRenderer.enabled = true;
            MoveLetter(_originalPos);
        }

        private void DoUiRaycast(PointerEventData eventData)
        {
            AssignLetterToUiSlot(InputDetection.GetUiRaycastData(eventData));
        }

        private bool AssignLetterToUiSlot(List<RaycastResult> results)
        {
            for (var i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer != LayerMask.NameToLayer("LetterSlot")) continue;
                results[i].gameObject.GetComponent<LetterSlotUi>().SetLetterSlotText(ConvertAsciiToString());
                return true;
            }

            return false;
        }

        #endregion
    }
}
