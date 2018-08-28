using CactusGuru.Infrastructure.Persistance;

namespace CactusGuru.Persistance
{
    public interface ITranslatorFactory
    {
        TranslatorBase<TDomain, TData> Of<TDomain, TData>() where TData : new();
    }
}
