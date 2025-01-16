
using Leap;
using UnityEngine;
using Leap.PhysicalHands;
using LetterQuest.Gameplay.Letters;
using LetterQuest.Gameplay.Metrics;
using LetterQuest.Gameplay.Letters.Manager;

namespace LetterQuest.Gameplay.Hands
{
    public class GrabBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerMetrics playerMetrics;
        private LeapServiceProvider _serviceProvider;
        private LetterManager _letterManager;
        private float _startTime;
        private int _attempts;

        private void Awake()
        {
            _letterManager = FindFirstObjectByType<LetterManager>();
            _serviceProvider = FindFirstObjectByType<LeapServiceProvider>();
        }

        private void Start()
        {
            if (_serviceProvider.IsConnected()) return;
            _serviceProvider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        public void OnGrabEnterEvent(ContactHand hand, Rigidbody obj)
        {
            _attempts++;
            if (_attempts > 1) playerMetrics.SimpleMetrics.IncrementGrabFails();
            if (obj.CompareTag("Letter") == false) return;
            
            if (_attempts == 1) playerMetrics.SimpleMetrics.IncrementSuccessfulGrabs();
            _letterManager.OnLetterGrabbed(obj.GetComponent<LetterBlock>());
            _startTime = Time.realtimeSinceStartup;
            _attempts = 0;
        }

        public void OnGrabExitEvent(ContactHand hand, Rigidbody obj)
        {
            if (obj.CompareTag("Letter") == false) return;
            var letterBlock = _letterManager.OnLetterReleased();
            if (letterBlock.OnDragEnd(hand.palmBone.transform.position))
                playerMetrics.AdvancedMetrics.AddGrabTime(_startTime);
        }
    }
}
