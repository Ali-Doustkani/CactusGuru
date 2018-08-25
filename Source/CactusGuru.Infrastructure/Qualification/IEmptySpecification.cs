namespace CactusGuru.Infrastructure.Qualification
{
    public interface IEmptySpecification : ISpecification
    {
        ISpecification SetProperty(string propertyName);
    }
}
