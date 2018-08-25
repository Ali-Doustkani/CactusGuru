namespace CactusGuru.Infrastructure.Persistance
{
    public interface IUnitOfWork
    {
        T CreateRepository<T>();
        void SaveChanges();
    }
}
