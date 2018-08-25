namespace CactusGuru.Infrastructure.Persistance
{
    public interface ISimilarityRepository
    {
        bool HasSimilar(DomainEntity domainEntity);
    }
}
