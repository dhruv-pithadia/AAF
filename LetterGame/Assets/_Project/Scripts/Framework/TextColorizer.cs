
using TMPro;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace LetterQuest.Framework.Misc
{
    public class TextColorizer : MonoBehaviour
    {
        [SerializeField] private float colorizerSpeed = 0.05f;
        private TMP_Text _tmpText;
        private TMP_TextInfo _textInfo;
        private Coroutine _coroutine;
        private WaitForSeconds _twoSecWait;
        private WaitForSeconds _characterWait;

        private void Awake()
        {
            _tmpText = GetComponent<TMP_Text>();
            _tmpText.enableWordWrapping = true;
            _tmpText.ForceMeshUpdate();
            _textInfo = _tmpText.textInfo;
            _twoSecWait = new WaitForSeconds(2f);
            _characterWait = new WaitForSeconds(colorizerSpeed);
        }

        private void OnEnable()
        {
            _coroutine = StartCoroutine(RandomTextColorFill());
        }

        private void OnDisable()
        {
            if (_coroutine == null) return;
            StopCoroutine(_coroutine);
            _coroutine = null;
        }

        private IEnumerator RandomTextColorFill()
        {
            var currentCharacter = 0;
            var characterCount = _tmpText.text.Length;

            var c0 = new Color32(0, 255, 0, 255);
            var c1 = new Color32(0, 0, 0, 127);

            while (true)
            {
                if (currentCharacter == characterCount)
                {
                    yield return _twoSecWait;
                    break;
                }

                if (currentCharacter == 0)
                {
                    c0 = new Color32((byte)Random.Range(0, 255),
                        (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 255);
                }

                var matIndex = _textInfo.characterInfo[currentCharacter].materialReferenceIndex;
                var vertexIndex = _textInfo.characterInfo[currentCharacter].vertexIndex;
                var newVertexColors = _textInfo.meshInfo[matIndex].colors32;

                if (_textInfo.characterInfo[currentCharacter].isVisible)
                {
                    c0 = newVertexColors[vertexIndex + 0] = c0;
                    newVertexColors[vertexIndex + 1] = c1;
                    newVertexColors[vertexIndex + 2] = c0;
                    newVertexColors[vertexIndex + 3] = c1;
                    _tmpText.UpdateVertexData();
                }

                currentCharacter = (currentCharacter + 1) % characterCount;
                yield return _characterWait;
            }
        }
    }
}
