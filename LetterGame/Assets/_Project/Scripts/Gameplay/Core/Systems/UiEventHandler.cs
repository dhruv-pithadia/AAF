
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Core
{
    [CreateAssetMenu(fileName = "UiEventHandler", menuName = "LetterQuest/Ui Event Handler")]
    public class UiEventHandler : ScriptableObject
    {
        private Camera _camera;
        private EventSystem _eventSystem;

        public void Initialize(Camera camera)
        {
            _camera = camera;
            _eventSystem = EventSystem.current;
        }

        public void Dispose()
        {
            _camera = null;
            _eventSystem = null;
        }

        public bool IsOverUi(Vector3 position, string layerName)
        {
            var results = GetUiRaycastAtPosition(position);
            if (results is not { Count: > 0 }) return false;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer != LayerMask.NameToLayer(layerName)) continue;
                return true;
            }

            return false;
        }

        public GameObject GetUiAtPosition(Vector3 position, string layerName)
        {
            var results = GetUiRaycastAtPosition(position);
            if (results is not { Count: > 0 }) return null;

            for (var i = 0; i < results.Count; i++)
            {
                if (results[i].gameObject.layer != LayerMask.NameToLayer(layerName)) continue;
                return results[i].gameObject;
            }

            return null;
        }

        private List<RaycastResult> GetUiRaycastAtPosition(Vector3 position)
        {
            var results = new List<RaycastResult>();
            var eventData = new PointerEventData(_eventSystem)
            {
                position = _camera.WorldToScreenPoint(position)
            };
            _eventSystem.RaycastAll(eventData, results);
            return results;
        }
    }
}
