
using TMPro;
using UnityEngine;

namespace LetterQuest
{
    public class Letter : MonoBehaviour
    {
        private TMP_Text _letterText;

        public void OnSpawn(Vector3 pos, int ascii)
        {
            var asciiChar = (char)ascii;
            //Debug.Log($"[Letter]: On Spawn - {c}");
            _letterText = GetComponentInChildren<TMP_Text>();
            _letterText.text = asciiChar.ToString();
            transform.position = pos;
        }

        public void OnDespawn()
        {
            Debug.Log("[Letter]: On Despawn");
            transform.position = Vector3.zero;
            _letterText.text = string.Empty;
        }
    }
}
