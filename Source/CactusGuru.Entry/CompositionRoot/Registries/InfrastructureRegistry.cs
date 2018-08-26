using CactusGuru.Domain.Greenhouse;
using CactusGuru.Entry.Infrastructure.Logging;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;
using CactusGuru.Persistance;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    internal class InfrastructureRegistry : Registry
    {
        public InfrastructureRegistry()
        {
            For<EventAggregator>().Singleton().Use<EventAggregator>();
            For<IPublisher<Genus>>().Use<Publisher<Genus>>();
            For<ITerminator<Genus>>().Use<Terminator<Genus>>();
            For<IPublisher<Taxon>>().Use<Publisher<Taxon>>();
            For<ITerminator<Taxon>>().Use<Terminator<Taxon>>();
            For<IPublisher<Collector>>().Use<Publisher<Collector>>();
            For<ITerminator<Collector>>().Use<Terminator<Collector>>();
            For<IPublisher<Supplier>>().Use<Publisher<Supplier>>();
            For<ITerminator<Supplier>>().Use<Terminator<Supplier>>();
            For<IPublisher<CollectionItem>>().Use<Publisher<CollectionItem>>();
            For<ITerminator<CollectionItem>>().Use(ctx => CollectionItemTerminator(ctx.GetInstance<IUnitOfWork>()));
            For<IPublisher<CollectionItemImage>>().Use<SimplePublisher<CollectionItemImage>>();
            For<ITerminator<CollectionItemImage>>().Use<SimpleTerminator<CollectionItemImage>>();
        }

        private ITerminator<CollectionItem> CollectionItemTerminator(IUnitOfWork uow)
        {
            return new Terminator<CollectionItem>(uow, new NullInquiry<CollectionItem>());
        }

      
    }
}
