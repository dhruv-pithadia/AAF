using System.Collections.Generic;
using UnityEngine;

public class LetterManager : MonoBehaviour
{
    [SerializeField] private LetterObjectPool LetterObjectPool;
    private List<letter> LetterList;
    [SerializeField] private int MaxLetters = 26;
    [SerializeField] private List<Transform> LetterPositions = new List<Transform>();

    private void Start()
    {
        for (int i = 0; i < MaxLetters; i++)
        {
            var letter = LetterObjectPool.GetLetter();
            letter.transform.position = LetterPositions[i].position;
            letter.onSpawn("B");

        }
    }
}