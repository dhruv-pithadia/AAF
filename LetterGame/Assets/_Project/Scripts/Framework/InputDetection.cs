
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace LetterQuest.Framework.Input
{
    [CreateAssetMenu(menuName = "LetterQuest/InputDetection", fileName = "InputDetection")]
    public class InputDetection : ScriptableObject, InputActions.IUIActions
    {
        private InputActions _inputActions;
        private static EventSystem _eventSystem;
        private static Vector2 _mousePosition;
        private static Camera _camera;

        #region Public Methods

        public void Initialize(Camera camera)
        {
            _camera = camera;
            _mousePosition = Vector2.zero;
            _eventSystem = EventSystem.current;
            _inputActions ??= new InputActions();
            _inputActions.Enable();
            _inputActions.UI.Enable();
            _inputActions.UI.SetCallbacks(this);
        }

        public void Dispose()
        {
            _inputActions?.UI.RemoveCallbacks(this);
            _inputActions?.UI.Disable();
            _inputActions?.Disable();
            _inputActions = null;
        }

        public static Vector3 GetMouseWorldPosition()
        {
            Vector3 result = _mousePosition;
            result.z = -_camera.transform.position.z;
            result = _camera.ScreenToWorldPoint(result);
            return result;
        }

        public static List<RaycastResult> GetHandOverUi(Vector3 position)
        {
            var eventData = new PointerEventData(EventSystem.current)
            {
                position = _camera.WorldToScreenPoint(position)
            };
            return GetUiRaycastData(eventData);
        }

        public static List<RaycastResult> GetUiRaycastData(PointerEventData eventData)
        {
            var results = new List<RaycastResult>();
            _eventSystem.RaycastAll(eventData, results);
            return results;
        }

        #endregion

        #region Event Callbacks

        public void OnPoint(InputAction.CallbackContext context)
        {
            if (context.action.triggered == false) return;
            _mousePosition = context.ReadValue<Vector2>();
        }

        public void OnClick(InputAction.CallbackContext context) { }

        #endregion
    }
}
