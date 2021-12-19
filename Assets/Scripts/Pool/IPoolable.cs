public interface IPoolable
{
    void ReturnToPool();
    void RequestFromPool();
}