
using TMPro;
using UnityEngine;
using LetterQuest.Gameplay.Input;
using LetterQuest.Gameplay.Events;

namespace LetterQuest.Gameplay.Letters.Manager
{
    [RequireComponent(typeof(LetterObjectPool))]
    public class LetterManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EventBus eventBus;
        [SerializeField] private InputDetection inputDetection;
        [field: SerializeField] private LetterSlotHandler SlotHandler { get; set; }
        [field: SerializeField] private LetterBlockHandler BlockHandler { get; set; }

        #region Unity Methods

        private void Start()
        {
            eventBus.BreakEvent += OnBreak;
            eventBus.WordSetEvent += OnWordSet;
            eventBus.WordResetEvent += OnWordReset;
            SlotHandler.Initialize(gameObject, eventBus);
            inputDetection.Initialize(Camera.main);
            BlockHandler.Initialize(gameObject);
        }

        private void OnBreak(bool toggle)
        {
            if (toggle) Pause();
            else Resume();
        }

        private void LateUpdate()
        {
            BlockHandler.Tick();
        }

        private void OnDisable()
        {
            SlotHandler.Dispose();
            BlockHandler.Dispose();
            inputDetection.Dispose();
            eventBus.WordSetEvent -= OnWordSet;
            eventBus.WordResetEvent -= OnWordReset;
        }

        #endregion

        #region Public Methods

        public void SetLetterArrangement(TMP_Dropdown change) => BlockHandler.UpdateBlockArrangement(change.value);
        public void OnLetterGrabbed(LetterBlock block) => BlockHandler.GrabBlock(block);
        public LetterBlock OnLetterReleased() => BlockHandler.ReleaseBlock();

        #endregion

        #region Private Methods

        private void OnWordSet()
        {
            SlotHandler.PlaceUiSlots();
            BlockHandler.PlaceLetterBlocks();
        }

        private void OnWordReset()
        {
            BlockHandler.RemoveLetterBlocks();
            SlotHandler.ResetUiSlots();
        }
        
        private void Pause()
        {
            SlotHandler.StartBreak();
        }
        
        private void Resume()
        {
            SlotHandler.EndBreak();
        }

        #endregion
    }
}
