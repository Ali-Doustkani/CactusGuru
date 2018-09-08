using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace CactusGuru.Application.Implementation.ViewProviders
{
    public class CollectorViewProvider : CommonDataEntryViewProvider<Collector, CollectorDto>, ICollectorViewProvider
    {
        public override IEnumerable<TransferObjectBase> GetList()
        {
            return base.GetList().Cast<CollectorDto>().OrderBy(x => x.FullName);
        }

        public bool HasSimilar(CollectorDto dto)
        {
            var collector = new Collector
            {
                Acronym = dto.Acronym,
                FullName = dto.FullName,
                Id = dto.Id
            };
            return Get<ICollectorRepository>().HasSimilar(collector);
        }
    }
}
