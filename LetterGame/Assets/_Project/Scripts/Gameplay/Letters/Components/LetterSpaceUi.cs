
using UnityEngine;

namespace LetterQuest.Gameplay.Ui
{
    public class LetterSpaceUi : MonoBehaviour
    {
        [SerializeField] private GameObject mainLetterSpace;
        [SerializeField] private GameObject subLetterSpace;

        private void OnEnable()
        {
            TurnOnMainUi();
        }

        public void TurnOnMainUi()
        {
            mainLetterSpace.gameObject.SetActive(true);
            subLetterSpace.gameObject.SetActive(false);
        }

        public void TurnOnSubUi()
        {
            mainLetterSpace.gameObject.SetActive(false);
            subLetterSpace.gameObject.SetActive(true);
        }
    }
}
