
using LetterQuest.Utils;

namespace LetterQuest.Gameplay
{
    public class LetterObjectPool : ObjectPooler<LetterBlock>
    {
        private void Awake() => Initialize();
        private void OnDisable() => Dispose();

        public LetterBlock GetLetter() => ObjectPool.Get();
        public void ReturnLetter(LetterBlock obj) => ObjectPool.Release(obj);
    }
}
