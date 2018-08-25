namespace CactusGuru.Infrastructure
{
    public interface IResolver
    {
        T Resolve<T>();
        T Resolve<T>(string key);
    }
}
