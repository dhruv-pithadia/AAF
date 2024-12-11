
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LetterQuest.Gameplay
{
    public class Letter : MonoBehaviour, IPointerDownHandler, IPointerMoveHandler, IPointerUpHandler
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public bool IsDragging { get; private set; }
        private Transform _letterTransform;
        private TMP_Text _letterText;
        private Camera _camera;
        private Vector3 _originalPosition;

        private void Awake()
        {
            _camera = Camera.main;
            _letterTransform = transform;
            _letterText = GetComponentInChildren<TMP_Text>();
        }

        #region Public Methods

        public void OnSpawn(Vector3 position, int ascii)
        {
            _originalPosition = position;
            AssignLetterText(((char)ascii).ToString());
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
        public void OnPointerUp(PointerEventData eventData) => FinishLetterDrag();

        #endregion

        #region Private Methods

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

        private void FinishLetterDrag()
        {
            IsDragging = false;
            meshRenderer.enabled = true;
            _letterTransform.position = _originalPosition;
            //  TODO: if over word slot, place letter in slot
        }

        #endregion
    }
}
