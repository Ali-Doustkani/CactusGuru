using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.CodeGenerating;
using CactusGuru.Domain.Greenhouse.Factories;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Greenhouse.Formatting.CollectionItems;
using CactusGuru.Domain.Greenhouse.Qualification.Inquiries;
using CactusGuru.Domain.Greenhouse.Qualification.Validators;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Entry.Infrastructure;
using CactusGuru.Infrastructure;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class DomainRegistry : RegistryBase
    {
        public DomainRegistry(IResolver resolver)
            : base(resolver)
        {
            For<ICollectionItemCodeGenerator>().Use<SequentialCodeGenerator>();
            For<IFormatter<Genus>>().Use<GenusCapitalFormatter>();
            For<IFormatter<Taxon>>().Use(TaxonSpeciesFormatter).Named("labelPrintSpec");
            For<IFormatter<Taxon>>().Use<TaxonFormatter>();
            For<IFormatter<CollectionItem>>().Use<CodeFormatter>().Named("codeFormatter");
            For<IFormatter<CollectionItem>>().Use<RefCollectionItemFormatter>().Named("labelPrintRef");
            For<IFormatter<CollectionItem>>().Use(CollectionItemLabelFormatter).Named("labelPrintSpec");
            For<IFormatter<CollectionItem>>().Use<TaxonFieldFormatter>().Named("taxonField");
            For<IFormatter<CollectionItem>>().Use<CollectionItemFormatter>();

            // Factories
            For<IFactory<Genus>>().Use<SimpleFactory<Genus>>();
            For<IFactory<Taxon>>().Use<SimpleFactory<Taxon>>();
            For<IFactory<Collector>>().Use<SimpleFactory<Collector>>();
            For<IFactory<Supplier>>().Use<SimpleFactory<Supplier>>();
            For<IFactory<CollectionItem>>().Use<CollectionItemFactory>();
            For<IFactory<CollectionItemImage>>().Use<CollectionItemImageFactory>();

            // Validators
            For<ValidatorBase<Collector>>().Use(() => Validator<Collector>.Create(
                Res<EmptySpecification>().SetProperty(nameof(Collector.Acronym)),
                Res<EmptySpecification>().SetProperty(nameof(Collector.FullName)),
                SimilaritySpec<ICollectorRepository>()));

            For<ValidatorBase<Genus>>().Use(() => Validator<Genus>.Create(
                Res<EmptySpecification>().SetProperty(nameof(Genus.Title)),
                SimilaritySpec<IGenusRepository>()));

            For<ValidatorBase<Supplier>>().Use(() => Validator<Supplier>.Create(
                Res<EmptySpecification>().SetProperty(nameof(Supplier.FullName)),
                SimilaritySpec<ISupplierRepository>()));

            For<ValidatorBase<Taxon>>().Use(() => Validator<Taxon>.Create(
                Res<EmptySpecification>().SetProperty(nameof(Taxon.Genus)),
                Res<EmptySpecification>().SetProperty(nameof(Taxon.Species)),
                SimilaritySpec<ITaxonRepository>()));

            For<ValidatorBase<CollectionItem>>().Use(() => new CollectionItemValidator(Res<EmptySpecification>()));

            // Inquiries
            For<InquiryBase<Collector>>().Use(CollectorInquiry);
            For<InquiryBase<Supplier>>().Use(SupplierInquiry);
            For<InquiryBase<Taxon>>().Use(TaxonInquiry);
            For<InquiryBase<CollectionItemImage>>().Use(() => new NullInquiry<CollectionItemImage>());

            Scan(x =>
            {
                x.AssemblyContainingType<Genus>();
                x.ConnectImplementationsToTypesClosing(typeof(IFormatter<>));
                x.ConnectImplementationsToTypesClosing(typeof(IFactory<>));
                x.ConnectImplementationsToTypesClosing(typeof(ITerminator<>));
                x.ConnectImplementationsToTypesClosing(typeof(ValidatorBase<>));
                x.ConnectImplementationsToTypesClosing(typeof(InquiryBase<>));
                x.WithDefaultConventions();
            });

            Scan(x =>
            {
                x.AssemblyContainingType<DomainDictionary>();
                x.WithDefaultConventions();
            });
        }

        private SimilaritySpec SimilaritySpec<TRepo>()
            where TRepo : ISimilarityRepository
        {
            return new SimilaritySpec(Res<TRepo>(), Res<IDomainDictionary>());
        }

        private InquiryBase<Collector> CollectorInquiry()
        {
            return new CollectorInquiry(Res<IUnitOfWork>(), Res<IFormatter<CollectionItem>>("codeFormatter"));
        }

        private InquiryBase<Supplier> SupplierInquiry()
        {
            return new SupplierInquiry(Res<IUnitOfWork>(), Res<IFormatter<CollectionItem>>("codeFormatter"));
        }

        private InquiryBase<Taxon> TaxonInquiry()
        {
            return new TaxonInquiry(Res<IUnitOfWork>(), Res<IFormatter<CollectionItem>>("codeFormatter"));
        }

        private IFormatter<CollectionItem> CollectionItemLabelFormatter()
        {
            return new LabelFormatter(new TaxonFormatter(new NullFormatter<Genus>()));
        }

        private IFormatter<Taxon> TaxonSpeciesFormatter()
        {
            return new TaxonFormatter(new NullFormatter<Genus>());
        }
    }
}
