
using TMPro;
using UnityEngine;
using LetterQuest.Framework.Audio;

namespace LetterQuest.Gameplay.Core
{
    [CreateAssetMenu(fileName = "BreakAlerts", menuName = "LetterQuest/Break Alerts")]
    public class BreakAlerts : ScriptableObject
    {
        private const float BreakTime = 600f;
        private TMP_Text _breakText;
        private float _breakTimer;

        private void OnEnable()
        {
            _breakTimer = BreakTime;
        }

        public void Initialize(TMP_Text breakText)
        {
            _breakText = breakText;
        }

        public bool Tick(float deltaTime)
        {
            _breakTimer -= deltaTime;
            if (_breakTimer > 0f) return false;
            EncourageBreak();
            return true;
        }

        private void EncourageBreak()
        {
            _breakTimer = BreakTime;
            _breakText.gameObject.SetActive(true);
            AudioManager.Instance.PlaySoundEffect(4);
        }
    }
}
