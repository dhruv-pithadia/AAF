
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace LetterQuest.Gameplay
{
    public class LetterBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public bool IsDragging { get; private set; }
        private Transform _letterTransform;
        private Vector3 _originalPosition;
        private EventSystem _eventSystem;
        private TMP_Text _letterText;
        private Camera _camera;
        private int _asciiCode;

        private void Awake()
        {
            _camera = Camera.main;
            _letterTransform = transform;
            _eventSystem = EventSystem.current;
            _letterText = GetComponentInChildren<TMP_Text>();
        }

        #region Public Methods

        public void OnSpawn(Vector3 position, int ascii)
        {
            _asciiCode = ascii;
            _originalPosition = position;
            AssignLetterText(ConvertAsciiToString());
            MoveLetter(_originalPosition);
        }

        public void OnDespawn()
        {
            MoveLetter(Vector3.zero);
            AssignLetterText(string.Empty);
        }

        #endregion

        #region Interface Methods

        public void OnPointerDown(PointerEventData eventData) => BeginLetterDrag();
        public void OnPointerMove(PointerEventData eventData) => LetterDragMovement(eventData.position);
        public void OnPointerUp(PointerEventData eventData) => FinishLetterDrag(eventData);

        #endregion

        #region Private Methods

        private string ConvertAsciiToString() => ((char)_asciiCode).ToString();
        private void AssignLetterText(string text) => _letterText.text = text;
        private void MoveLetter(Vector3 position) => _letterTransform.position = position;

        private void BeginLetterDrag()
        {
            IsDragging = true;
            meshRenderer.enabled = false;
        }

        private void LetterDragMovement(Vector3 mousePosition)
        {
            if (IsDragging == false) return;
            mousePosition.z = -_camera.transform.position.z;
            mousePosition = _camera.ScreenToWorldPoint(mousePosition);
            MoveLetter(mousePosition);
        }

        private void FinishLetterDrag(PointerEventData eventData)
        {
            IsDragging = false;
            meshRenderer.enabled = true;
            _letterTransform.position = _originalPosition;
            AssignLetterToUiSlot(eventData);
        }

        private void AssignLetterToUiSlot(PointerEventData eventData)
        {
            var raycastResults = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventData, raycastResults);

            for (var i = 0; i < raycastResults.Count; i++)
            {
                if (raycastResults[i].gameObject.layer != LayerMask.NameToLayer("LetterSlot")) continue;
                raycastResults[i].gameObject.GetComponent<LetterSlot>().SetLetterSlotText(ConvertAsciiToString());
            }
        }

        #endregion
    }
}
