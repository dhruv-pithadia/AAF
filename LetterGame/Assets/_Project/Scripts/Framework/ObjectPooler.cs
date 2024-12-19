
using UnityEngine;

namespace LetterQuest.Framework.Utilities
{
    public abstract class ObjectPooler<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private T prefab;
        [SerializeField] private int maxSize;
        [SerializeField] private int initialSize;
        [SerializeField] private bool collectionCheck;
        [SerializeField] private Transform objParent;
        protected UnityEngine.Pool.ObjectPool<T> ObjectPool;

        #region Unity Methods

        private void Awake()
        {
            ObjectPool = new UnityEngine.Pool.ObjectPool<T>(Create, OnGetCallback, OnReleaseCallback,
                OnDestroyCallback, collectionCheck, initialSize, maxSize);
        }

        private void OnDisable()
        {
            if (ReferenceEquals(ObjectPool, null)) return;
            ObjectPool.Dispose();
            ObjectPool = null;
        }

        #endregion

        #region Private Methods

        private T Create() => Instantiate(prefab, Vector3.zero, Quaternion.identity, objParent);
        private static void OnGetCallback(T obj) => obj.gameObject.SetActive(true);
        private static void OnReleaseCallback(T obj) => obj.gameObject.SetActive(false);

        private static void OnDestroyCallback(T obj)
        {
            if (ReferenceEquals(obj, null)) return;
            Debug.Log("[ObjectPooler]: On Destroy Callback");
            Destroy(obj.gameObject);
        }

        #endregion
    }
}
