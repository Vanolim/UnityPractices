using UnityEngine;

namespace ObjectPool
{
    public abstract class ObjectFactory<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        private T _template;

        public T Create() => Instantiate(_template);
        
        public T Create(Transform container) => Instantiate(_template, container);

        private void OnValidate()
        {
            if (_template != null && _template.GetComponent<T>() == null)
            {
                _template = null;
                Debug.LogError("Templane must contain" + typeof(T) + "component");
            }
        }
    }
}