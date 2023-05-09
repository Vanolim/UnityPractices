using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ObjectPool
{
    public abstract class Pool<T> where T : MonoBehaviour
    {
        private readonly ObjectFactory<T> _objectFactory;
        private List<PoolElement<T>> _pool;

        public Pool(ObjectFactory<T> objectFactory, int initialCapacity)
        {
            _objectFactory = objectFactory;
            Initialize(initialCapacity);
        }

        private void Initialize(int initialCapacity)
        {
            _pool = new List<PoolElement<T>>();
            for (int i = 0; i < initialCapacity; i++)
            {
                SpawnElement();
            }
        }

        private void SpawnElement()
        {
            T element = _objectFactory.Create();
            _pool.Add(new PoolElement<T>(element.GetComponent<T>()));
        }

        public T GetElement()
        {
            PoolElement<T> pElement = _pool.FirstOrDefault(p => p.IsUsed == false);
            if (pElement != null)
            {
                T element = pElement.Object;
                pElement.IsUsed = true;
                return element;
            }
            
            SpawnElement();
            return GetElement();
        }

        public void ReturnToPool(T element)
        {
            PoolElement<T> pElement = _pool.FirstOrDefault(p => p.Object == element);
            if (pElement != null)
            {
                pElement.IsUsed = false;
            }
            else
            {
                Debug.LogWarning("Object is not element of pull");
            }
        }
    }
}