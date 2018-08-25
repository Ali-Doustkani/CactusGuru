namespace CactusGuru.Domain.Greenhouse.Formatting
{
    public class NullFormatter<T> : IFormatter<T>
    {
        public string Format(T domainEntity)
        {
            return string.Empty;
        }
    }
}
