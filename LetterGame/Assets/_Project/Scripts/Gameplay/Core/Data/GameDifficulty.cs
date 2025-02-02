
using UnityEngine;

namespace LetterQuest.Gameplay.Settings
{
    [CreateAssetMenu(fileName = "GameDifficulty", menuName = "LetterQuest/Game Difficulty")]
    public class GameDifficulty : ScriptableObject
    {
        [field: SerializeField] public int Value { get; private set; }

        public void AssignGameDifficulty(int difficulty)
        {
            Value = Mathf.Clamp(difficulty, 0, 2);
        }
    }
}
