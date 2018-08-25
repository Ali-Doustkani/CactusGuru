namespace CactusGuru.Infrastructure.Persistance
{
    public interface IPublisher<T>
    {
        void Add(T domainEntity);
        void Update(T domainEntity);
    }
}
