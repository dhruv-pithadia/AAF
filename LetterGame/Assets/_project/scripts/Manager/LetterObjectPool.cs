using System.Collections.Generic;
using LetterQuest.common;
using UnityEngine;

public class LetterObjectPool : ObjectPooler<letter>
{
    // [SerializeField] private GameObject letterPrefab; // The letter prefab to be used
    // [SerializeField] private int poolSizePerLetter = 5; // Number of copies per letter in the pool

    // private Dictionary<char, Queue<GameObject>> letterPools = new Dictionary<char, Queue<GameObject>>();

    private void Awake()
    {
        Initialize();
        // Initialize pool for each letter from A to Z
        //     for (char letter = 'A'; letter <= 'Z'; letter++)
        //     {
        //         Queue<GameObject> pool = new Queue<GameObject>();
        //         for (int i = 0; i < poolSizePerLetter; i++)
        //         {
        //             GameObject letterObj = Instantiate(letterPrefab);
        //             letterObj.name = letter.ToString();
        //             letterObj.GetComponentInChildren<TextMesh>().text = letter.ToString();
        //             letterObj.SetActive(false);
        //             pool.Enqueue(letterObj);
        //         }
        //         letterPools.Add(letter, pool);
        //     }
    }

    private void OnDestroy()
    {
        dispose();
    }

    protected override letter create()
    {
        letter newLetter = base.create();
        // TODO FILL IN LETTER VARIABLES
        return newLetter;

    }

    protected override void ongetcallback(letter obj)
    {
        base.ongetcallback(obj);
        // obj.onSpawn();
    }

    protected override void onreleasecallback(letter obj)
    {
        base.onreleasecallback(obj);
        obj.ondeSpawn();
    }

    public letter GetLetter()
    {
        return create();

    }


    // public GameObject GetLetter(char letter)
    // {
    //     if (!letterPools.ContainsKey(letter))
    //     {
    //         Debug.LogWarning("Letter pool does not contain: " + letter);
    //         return null;
    //     }

    //     Queue<GameObject> pool = letterPools[letter];
    //     if (pool.Count > 0)
    //     {
    //         GameObject letterObj = pool.Dequeue();
    //         letterObj.SetActive(true);
    //         return letterObj;
    //     }
    //     else
    //     {
    //         // Expand pool if needed
    //         GameObject letterObj = Instantiate(letterPrefab);
    //         letterObj.name = letter.ToString();
    //         letterObj.GetComponentInChildren<TextMesh>().text = letter.ToString();
    //         letterObj.SetActive(true);
    //         return letterObj;
    //     }
    // }

    // public void ReturnToPool(GameObject letterObj)
    // {
    //     char letter = letterObj.name[0];
    //     letterObj.SetActive(false);
    //     if (letterPools.ContainsKey(letter))
    //     {
    //         letterPools[letter].Enqueue(letterObj);
    //     }
    // }
}