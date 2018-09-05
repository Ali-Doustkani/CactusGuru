using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;

namespace CactusGuru.Domain.Persistance.Repositories
{
    public interface ICollectorRepository : IRepository<Collector>, ISimilarityRepository
    { }
}
