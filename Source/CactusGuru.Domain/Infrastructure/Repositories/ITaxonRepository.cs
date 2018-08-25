using System;
using System.Collections.Generic;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Domain.Persistance.Repositories
{
    public interface ITaxonRepository : IRepository<Taxon>, ISimilarityRepository
    {
        IEnumerable<Taxon> GetByGeneraId(Guid guid);
    }
}
