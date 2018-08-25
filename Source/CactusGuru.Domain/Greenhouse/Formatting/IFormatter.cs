namespace CactusGuru.Domain.Greenhouse.Formatting
{
    public interface IFormatter<T>
    {
        string Format(T domainEntity);
    }
}
