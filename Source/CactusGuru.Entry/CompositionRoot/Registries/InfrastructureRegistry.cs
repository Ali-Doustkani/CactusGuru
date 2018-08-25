using CactusGuru.Application.Implementation.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Entry.Infrastructure.Logging;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Infrastructure.Logging;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Persistance.Merging;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    internal class InfrastructureRegistry : RegistryBase
    {
        public InfrastructureRegistry(IResolver resolver)
            : base(resolver)
        {
            For<ILogger>().Use<LogForNet>();
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
            For<ITerminator<CollectionItem>>().Use(CollectionItemTerminator);
            For<IPublisher<CollectionItemImage>>().Use<SimplePublisher<CollectionItemImage>>();
            For<ITerminator<CollectionItemImage>>().Use<SimpleTerminator<CollectionItemImage>>();
        }

        private ITerminator<CollectionItem> CollectionItemTerminator()
        {
            return new Terminator<CollectionItem>(Res<IUnitOfWork>(), new NullInquiry<CollectionItem>());
        }
    }
}
