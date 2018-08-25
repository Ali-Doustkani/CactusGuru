using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Domain.Persistance.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>, ISimilarityRepository
    {
    }
}
