
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using LetterQuest.Framework.Animation;
using LetterQuest.Framework.Input;
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
        private TMP_Text _letterText;
        private Vector3 _originalPos;
        private int _asciiCode;

        #region Unity Methods

        private void Awake()
        {
            _letterTransform = transform;
            canvas.worldCamera = Camera.main;
            _animatorHook = GetComponent<AnimatorHook>();
            _letterText = GetComponentInChildren<TMP_Text>();
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

        public void OnDespawn()
        {
            AssignLetterText(string.Empty);
            MoveLetter(Vector3.zero);
        }

        public void PlayHint()
        {
            _animatorHook.Play();
        }

        public void OnDragStart()
        {
            meshRenderer.enabled = false;
        }

        public void OnDragEnd(Vector3 position)
        {
            meshRenderer.enabled = true;
            AssignLetterToUiSlot(InputDetection.GetHandOverUi(position));
            MoveLetter(_originalPos);
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
            meshRenderer.enabled = true;
            MoveLetter(_originalPos);
        }

        private void DoUiRaycast(PointerEventData eventData)
        {
            AssignLetterToUiSlot(InputDetection.GetUiRaycastData(eventData));
        }

        private void AssignLetterToUiSlot(List<RaycastResult> results)
        {
            for (var i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer != LayerMask.NameToLayer("LetterSlot")) continue;
                results[i].gameObject.GetComponent<LetterSlotUi>().SetLetterSlotText(ConvertAsciiToString());
                break;
            }
        }

        #endregion
    }
}
