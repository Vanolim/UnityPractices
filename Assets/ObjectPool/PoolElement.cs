namespace ObjectPool
{
    public class PoolElement<T>
    {
        public readonly T Object;
        public bool IsUsed;

        public PoolElement(T @object)
        {
            Object = @object;
            IsUsed = false;
        }
    }
}