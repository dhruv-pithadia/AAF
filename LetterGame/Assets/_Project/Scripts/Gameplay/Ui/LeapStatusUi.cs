
using Leap;
using UnityEngine;

namespace LetterQuest.Gameplay.Ui
{
    public class LeapStatusUi : MonoBehaviour
    {
        [SerializeField] private StatusLabelUi leapStatusLabel;
        private const string LeapDisconnectedMsg = "Leap Motion Controller Disconnected";
        private const string LeapConnectedMsg = "Leap Motion Controller Connected";
        private LeapServiceProvider _leapServiceProvider;
        private float _timer;

        #region Unity Methods

        private void Awake()
        {
            _leapServiceProvider = FindFirstObjectByType<LeapServiceProvider>();
            _leapServiceProvider.OnDeviceChanged += OnLeapChanged;
        }

        private void LateUpdate()
        {
            _timer -= Time.deltaTime;
            if (_timer > 0) return;
            CheckLeapConnection();
            _timer = 3f;
        }

        private void OnDisable()
        {
            _leapServiceProvider.OnDeviceChanged -= OnLeapChanged;
        }

        #endregion

        #region Private Methods

        private void OnLeapChanged(Device obj)
        {
            CheckLeapConnection();
        }

        private void CheckLeapConnection()
        {
            if (_leapServiceProvider.IsConnected() == false)
            {
                leapStatusLabel.UpdateStatus(LeapDisconnectedMsg, Color.red);
            }
            else
            {
                leapStatusLabel.UpdateStatus(LeapConnectedMsg, Color.green);
            }
        }

        #endregion
    }
}
