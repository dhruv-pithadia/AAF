
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LetterQuest.Gameplay
{
    public class LetterBlock : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private MeshRenderer meshRenderer;
        public bool IsDragging { get; private set; }
        private Transform _letterTransform;
        private TMP_Text _letterText;
        private Vector3 _originalPos;
        private int _asciiCode;

        #region Unity Methods

        private void Awake()
        {
            _letterTransform = transform;
            _letterText = GetComponentInChildren<TMP_Text>();
        }

        #endregion

        #region Public Methods

        public void MoveLetter(Vector3 position) => _letterTransform.position = position;

        public void OnSpawn(Vector3 position, int ascii)
        {
            _asciiCode = ascii;
            _originalPos = position;
            AssignLetterText(ConvertAsciiToString());
            MoveLetter(_originalPos);
        }

        public void OnDespawn()
        {
            MoveLetter(Vector3.zero);
            AssignLetterText(string.Empty);
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
            meshRenderer.enabled = false;
        }

        private void FinishLetterDrag(PointerEventData eventData)
        {
            IsDragging = false;
            meshRenderer.enabled = true;
            AssignLetterToUiSlot(eventData);
            MoveLetter(_originalPos);
        }

        private void AssignLetterToUiSlot(PointerEventData eventData)
        {
            var raycastData = InputDetection.GetUiRaycastData(eventData);

            for (var i = 0; i < raycastData.Count; i++)
            {
                if (raycastData[i].gameObject.layer != LayerMask.NameToLayer("LetterSlot")) continue;
                raycastData[i].gameObject.GetComponent<LetterSlot>().SetLetterSlotText(ConvertAsciiToString());
            }
        }

        #endregion
    }
}
