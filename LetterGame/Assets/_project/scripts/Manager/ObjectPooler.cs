using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

namespace LetterQuest.common
{
    public abstract class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] protected int MaxSize;
        [SerializeField] protected int InitialSize;
        [SerializeField] protected bool CollectionCheck;

        UnityEngine.Pool.ObjectPool<T> ObjectPool;

        protected void Initialize()
        {
            ObjectPool = new UnityEngine.Pool.ObjectPool<T>(create, ongetcallback, onreleasecallback, ondestroycallback, CollectionCheck, InitialSize, MaxSize);

        }

        protected void Initialize(int MaxSize, int InitialSize, bool CollectionCheck)
        {
            this.MaxSize = MaxSize;
            this.InitialSize = InitialSize;
            this.CollectionCheck = CollectionCheck;
            ObjectPool = new UnityEngine.Pool.ObjectPool<T>(create, ongetcallback, onreleasecallback, ondestroycallback, CollectionCheck, InitialSize, MaxSize);

        }

        protected virtual T create()
        {
            return Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }

        protected virtual void ongetcallback(T obj)
        {
            obj.gameObject.SetActive(true);
        }

        protected virtual void onreleasecallback(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        private static void ondestroycallback(T obj)
        {
            if (obj == null) return;
            Destroy(obj.gameObject);
        }

        public void release(T obj)
        {
            ObjectPool.Release(obj);
        }

        protected void dispose()
        {
            if (ObjectPool == null) return;
            ObjectPool.Dispose();
            ObjectPool = null;
        }



    }



}
