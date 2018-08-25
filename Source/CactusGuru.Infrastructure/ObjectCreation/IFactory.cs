namespace CactusGuru.Infrastructure.ObjectCreation
{
    public interface IFactory<T>
        where T : DomainEntity
    {
        T CreateNew();
        T CreateNew(FactoryArg argument);
    }
}
