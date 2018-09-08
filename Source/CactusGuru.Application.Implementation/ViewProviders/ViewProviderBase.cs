namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class ViewProviderBase
    {
        protected IServiceLocator Begin()
        {
            return ServiceLocationBase.Instance.Begin();
        }
    }
}
