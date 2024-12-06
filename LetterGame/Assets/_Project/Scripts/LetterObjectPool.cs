
namespace LetterQuest
{
    public class LetterObjectPool : ObjectPooler<Letter>
    {
        private void Awake() => Initialize();
        private void OnDestroy() => Dispose();

        public Letter GetLetter() => ObjectPool.Get();
        public void ReturnLetter(Letter obj) => ObjectPool.Release(obj);
    }
}
