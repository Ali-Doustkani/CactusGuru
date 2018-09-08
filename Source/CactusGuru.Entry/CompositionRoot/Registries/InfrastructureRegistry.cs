using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    internal class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<EventAggregator>().Use<EventAggregator>().Singleton();
            For<Terminator<CollectionItem>>().Use(ctx => CollectionItemTerminator(ctx.GetInstance<ICollectionItemRepository>()));
        }

        private Terminator<CollectionItem> CollectionItemTerminator(ICollectionItemRepository repo)
        {
            return new Terminator<CollectionItem>(repo, new NullInquiry<CollectionItem>());
        }
    }
}
