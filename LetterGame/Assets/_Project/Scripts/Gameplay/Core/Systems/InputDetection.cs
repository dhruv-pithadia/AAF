
using UnityEngine;
using UnityEngine.InputSystem;

namespace LetterQuest.Gameplay.Input
{
    [CreateAssetMenu(fileName = "InputDetection", menuName = "LetterQuest/Input Detection")]
    public class InputDetection : ScriptableObject, InputActions.IUIActions
    {
        private Camera _camera;
        private InputActions _inputActions;
        private Vector2 _mousePosition;

        #region Public Methods

        public void Initialize(Camera camera)
        {
            _camera = camera;
            _mousePosition = Vector2.zero;
            _inputActions ??= new InputActions();
            _inputActions.Enable();
            _inputActions.UI.Enable();
            _inputActions.UI.SetCallbacks(this);
        }

        public void Dispose()
        {
            _inputActions?.UI.RemoveCallbacks(this);
            _mousePosition = Vector2.zero;
            _inputActions?.UI.Disable();
            _inputActions?.Disable();
            _inputActions = null;
            _camera = null;
        }

        public Vector3 GetMouseWorldPosition()
        {
            Vector3 result = _mousePosition;
            result.z = -_camera.transform.position.z;
            result = _camera.ScreenToWorldPoint(result);
            return result;
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
