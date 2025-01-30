
using UnityEngine;
using LetterQuest.Gameplay.Metrics;
using LetterQuest.Gameplay.Letters.Manager;

namespace LetterQuest.Gameplay.Letters
{
    public class LetterBlockGrab : MonoBehaviour
    {
        [field: SerializeField] public bool IsPointing { get; private set; }
        [field: SerializeField] public bool IsInUse { get; private set; }
        [SerializeField] private MetricsContainer metrics;
        private LetterManager _letterManager;
        private float _startTime;
        private int _attempts;

        private void Awake()
        {
            IsInUse = false;
            _letterManager = FindFirstObjectByType<LetterManager>();
        }

        private void OnDisable()
        {
            _letterManager = null;
            IsInUse = false;
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
            if (_letterManager.OnLetterReleased())
            {
                metrics.Handler.AddLetterTime(_startTime);
                if (_attempts == 1) metrics.Handler.IncrementPerfectGrabs();
                _attempts = 0;
                return;
            }

            metrics.Handler.IncrementGrabFails();
        }
    }
}
