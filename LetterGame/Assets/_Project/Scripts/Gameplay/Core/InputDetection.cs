
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Input
{
    [CreateAssetMenu(fileName = "InputDetection", menuName = "LetterQuest/Input Detection")]
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

        public static bool IsOverUi(Vector3 position, string layerName)
        {
            var results = GetUiRaycastAtPosition(position);
            if (results.Count <= 0) return false;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer != LayerMask.NameToLayer(layerName)) continue;
                return true;
            }

            return false;
        }

        public static GameObject GetUiAtPosition(Vector3 position, string layerName)
        {
            var results = GetUiRaycastAtPosition(position);
            if (results.Count <= 0) return null;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer != LayerMask.NameToLayer(layerName)) continue;
                return results[i].gameObject;
            }

            return null;
        }

        private static List<RaycastResult> GetUiRaycastAtPosition(Vector3 position)
        {
            var eventData = new PointerEventData(_eventSystem)
            {
                position = _camera.WorldToScreenPoint(position)
            };
            return GetUiRaycastData(eventData);
        }

        private static List<RaycastResult> GetUiRaycastData(PointerEventData eventData)
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

        public void OnClick(InputAction.CallbackContext context)
        {
        }

        #endregion
    }
}
