
using TMPro;
using UnityEngine;
using LetterQuest.Gameplay.Input;
using LetterQuest.Gameplay.Events;
using LetterQuest.Gameplay.Letters.Ui;

namespace LetterQuest.Gameplay.Letters.Manager
{
    [RequireComponent(typeof(LetterObjectPool))]
    public class LetterManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private EventBus eventBus;
        [SerializeField] private InputDetection inputDetection;
        
        [field: SerializeField] private LetterBlockPlacer BlockPlacer { get; set; }
        [field: SerializeField] private LetterSlotHandler SlotHandler { get; set; }
        private string _currentWord;

        #region Unity Methods

        private void Start()
        {
            eventBus.WordSetEvent += OnWordSetEvent;
            eventBus.WordResetEvent += OnWordCompleteEvent;
            SlotHandler.Initialize(gameObject, eventBus, FindFirstObjectByType<LetterUiContainer>());
            BlockPlacer.Initialize(GetComponent<LetterObjectPool>());
            inputDetection.Initialize(Camera.main);
        }

        private void LateUpdate()
        {
            BlockPlacer.Tick();
        }

        private void OnDisable()
        {
            SlotHandler.Dispose();
            BlockPlacer.Dispose();
            inputDetection.Dispose();
            eventBus.WordSetEvent -= OnWordSetEvent;
            eventBus.WordCompleteEvent -= OnWordCompleteEvent;
        }

        #endregion

        #region Public Methods

        public void SetArrangement(TMP_Dropdown change) => BlockPlacer.UpdateBlockArrangement(_currentWord, change.value);
        public void OnLetterGrabbed(LetterBlock block) => BlockPlacer.GrabBlock(block);
        public LetterBlock OnLetterReleased() => BlockPlacer.ReleaseBlock();

        #endregion

        #region Private Methods
        
        private void OnWordSetEvent(string word)
        {
            BlockPlacer.PlaceLetters(word);
            _currentWord = word;
        }

        private void OnWordCompleteEvent()
        {
            SlotHandler.ResetAllSlots();
            BlockPlacer.RemoveLetters();
        }

        #endregion
    }
}
