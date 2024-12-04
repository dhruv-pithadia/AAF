
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace LetterQuest
{
    public class LetterManager : MonoBehaviour
    {
        [SerializeField] private LetterObjectPool letterObjectPool;

        [FormerlySerializedAs("LetterPositions")]
        [SerializeField] private List<Transform> letterPositions = new();

        [SerializeField] private int maxLetters = 26;
        private List<Letter> _letterList;

        private void Start()
        {
            for (int i = 0; i < letterPositions.Count; i++)
            {
                var letter = letterObjectPool.GetLetter();
                letter.transform.position = letterPositions[i].position;
                letter.OnSpawn("B");
            }
        }
    }
}
