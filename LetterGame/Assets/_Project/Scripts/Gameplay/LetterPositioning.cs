
using UnityEngine;
using System.Collections.Generic;

namespace LetterQuest.Gameplay.Letters.Data
{
    [CreateAssetMenu(fileName = "Letter Positioning", menuName = "LetterQuest/Letter Positioning")]
    public class LetterPositioning : ScriptableObject
    {
        public List<Vector3> alphabetPositions = new();
        public List<Vector3> qwertyPositions = new();
        public List<Vector3> easyLevelPositions = new();
        public List<Vector3> medLevelPositions = new();
        public List<Vector3> hardLevelPositions = new();

        [SerializeField] private Vector3 smallScale;
        [SerializeField] private Vector3 mediumScale;
        [SerializeField] private Vector3 largeScale;

        public Vector3 GetPositionAt(LetterPositioningMode mode, int index)
        {
            return mode switch
            {
                LetterPositioningMode.Qwerty => qwertyPositions[index],
                LetterPositioningMode.Easy => easyLevelPositions[index],
                LetterPositioningMode.Medium => medLevelPositions[index],
                LetterPositioningMode.Hard => hardLevelPositions[index],
                _ => alphabetPositions[index]
            };
        }

        public Vector3 GetScale(LetterPositioningMode mode)
        {
            return mode switch
            {
                LetterPositioningMode.Medium => mediumScale,
                LetterPositioningMode.Easy => largeScale,
                _ => smallScale
            };
        }
    }

    public enum LetterPositioningMode : byte
    {
        Easy,
        Medium,
        Hard,
        Alphabet,
        Qwerty
    }
}
