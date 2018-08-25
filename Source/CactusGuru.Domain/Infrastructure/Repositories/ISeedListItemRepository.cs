using System;
using CactusGuru.Domain.Greenhouse.Sales;
using CactusGuru.Infrastructure.Persistance;

namespace CactusGuru.Domain.Persistance.Repositories
{
    public interface ISeedListItemRepository : IRepository<SeedListItemBase>
    {
        void DeleteBySeedListId(Guid id);
    }
}
