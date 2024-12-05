
namespace LetterQuest
{
    public class LetterObjectPool : ObjectPooler<Letter>
    {
        private void Awake()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            Dispose();
        }

        protected override void OnGetCallback(Letter obj)
        {
            base.OnGetCallback(obj);
            obj.OnSpawn(obj.name);
        }

        protected override void OnReleaseCallback(Letter obj)
        {
            base.OnReleaseCallback(obj);
            obj.OnDespawn();
        }

        public Letter GetLetter() => ObjectPool.Get();
        public void ReturnLetter(Letter obj) => ObjectPool.Release(obj);
    }
}