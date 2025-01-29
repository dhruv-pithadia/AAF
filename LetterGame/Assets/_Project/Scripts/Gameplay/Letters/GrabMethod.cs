
using UnityEngine;
using LetterQuest.Gameplay.Metrics;
using LetterQuest.Gameplay.Letters.Manager;

namespace LetterQuest.Gameplay.Letters
{
    [CreateAssetMenu(fileName = "GrabMethod", menuName = "LetterQuest/Grab Method")]
    public class GrabMethod : ScriptableObject
    {
        [field: SerializeField] public bool IsPointing { get; private set; }
        [field: SerializeField] public bool IsInUse { get; private set; } = false;
        [SerializeField] private MetricsContainer metrics;
        private LetterManager _letterManager;
        private float _startTime;
        private int _attempts;

        private void OnDisable()
        {
            _letterManager = null;
            IsInUse = false;
        }

        public void Initialize(LetterManager letterManager)
        {
            _letterManager = letterManager;
        }

        public void Grabbing()
        {
            IsPointing = false;
        }

        public void Pointing()
        {
            IsPointing = true;
        }

        public void OnGrabLetter(LetterBlock block)
        {
            if (IsInUse) return;
            _attempts++;
            IsInUse = true;
            _startTime = Time.realtimeSinceStartup;
            _letterManager.OnLetterGrabbed(block);
        }

        public void OnReleaseLetter()
        {
            if (IsInUse == false) return;
            
            IsInUse = false;
            if (_letterManager.OnLetterReleased().OnDragEnd())
            {
                metrics.Handler.AddGrabTime(_startTime);
                if (_attempts == 1) metrics.Handler.IncrementCorrectGrabs();
                _attempts = 0;
                return;
            }

            metrics.Handler.IncrementGrabFails();
        }
    }
}
