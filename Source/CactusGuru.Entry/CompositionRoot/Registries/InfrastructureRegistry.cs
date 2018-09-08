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
            For<IPublisher<Genus>>().Use<Publisher<Genus>>();
            For<ITerminator<Genus>>().Use<Terminator<Genus>>();
            For<IPublisher<Taxon>>().Use<Publisher<Taxon>>();
            For<ITerminator<Taxon>>().Use<Terminator<Taxon>>();
            For<IPublisher<Collector>>().Use<Publisher<Collector>>();
            For<ITerminator<Collector>>().Use<Terminator<Collector>>();
            For<IPublisher<Supplier>>().Use<Publisher<Supplier>>();
            For<ITerminator<Supplier>>().Use<Terminator<Supplier>>();
            For<IPublisher<CollectionItem>>().Use<Publisher<CollectionItem>>();
            For<ITerminator<CollectionItem>>().Use(ctx => CollectionItemTerminator(ctx.GetInstance<IUnitOfWork>(), ctx.GetInstance<ICollectionItemRepository>()));
            For<IPublisher<CollectionItemImage>>().Use<SimplePublisher<CollectionItemImage>>();
            For<ITerminator<CollectionItemImage>>().Use<SimpleTerminator<CollectionItemImage>>();
        }

        private ITerminator<CollectionItem> CollectionItemTerminator(IUnitOfWork uow, ICollectionItemRepository repo)
        {
            return new Terminator<CollectionItem>(uow, repo, new NullInquiry<CollectionItem>());
        }
    }
}
