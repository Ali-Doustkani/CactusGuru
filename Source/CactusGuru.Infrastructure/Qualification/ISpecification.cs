namespace CactusGuru.Infrastructure.Qualification
{
    public interface ISpecification
    {
        Error Satisfy(DomainEntity domainEntity);
    }  
}