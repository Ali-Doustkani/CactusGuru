namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class ViewProviderBase
    {
        protected T Get<T>()
        {
            return ServiceLocatorBase.Instance.GetInstance<T>();
        }
    }
}
