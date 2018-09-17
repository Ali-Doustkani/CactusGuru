namespace CactusGuru.Infrastructure.Persistance
{
    public interface IReader<T>
    {
        bool Read();
        T Value { get; }
    }
}
