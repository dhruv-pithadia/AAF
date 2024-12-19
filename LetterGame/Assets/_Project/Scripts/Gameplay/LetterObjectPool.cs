
using LetterQuest.Framework.Utilities;

namespace LetterQuest.Gameplay.Letters
{
    public class LetterObjectPool : ObjectPooler<LetterBlock>
    {
        public LetterBlock GetLetter() => ObjectPool.Get();
        public void ReturnLetter(LetterBlock obj) => ObjectPool.Release(obj);
    }
}
