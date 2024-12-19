
using UnityEngine;

namespace LetterQuest.Gameplay.Data
{
    [CreateAssetMenu(fileName = "Game Difficulty", menuName = "LetterQuest/Game Difficulty")]
    public class GameDifficulty : ScriptableObject
    {
        [field: SerializeField] public int Value { get; private set; } = 0;

        public void AssignGameDifficulty(int difficulty)
        {
            Value = Mathf.Clamp(difficulty, 0, 2);
        }
    }
}
