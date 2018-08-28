using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Persistance;

namespace CactusGuru.Entry.CompositionRoot
{
    public class TranslatorFactory : ITranslatorFactory
    {
        public TranslatorBase<TDomain, TData> Of<TDomain, TData>() where TData : new()
        {
            return ObjectFactory.Instance.GetInstance<TranslatorBase<TDomain, TData>>();
        }
    }
}
