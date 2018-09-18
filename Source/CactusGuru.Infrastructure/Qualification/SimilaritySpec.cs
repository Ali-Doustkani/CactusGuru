using CactusGuru.Infrastructure.Persistance;

namespace CactusGuru.Infrastructure.Qualification
{
    public class SimilaritySpec : ISpecification
    {
        public SimilaritySpec(ISimilarityRepository repo, IDomainDictionary dictionary)
        {
            _repo = repo;
            _dictionary = dictionary;
        }

        private readonly ISimilarityRepository _repo;
        private readonly IDomainDictionary _dictionary;

        public Error Satisfy(DomainEntity domainEntity)
        {
            if (_repo.HasSimilar(domainEntity))
                return new Error($"{_dictionary.Translate(domainEntity.GetType().Name)} already exists.");
            return Error.Empty;
        }
    }
}
