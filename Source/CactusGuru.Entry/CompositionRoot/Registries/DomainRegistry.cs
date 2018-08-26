using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.CodeGenerating;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Greenhouse.Formatting.CollectionItems;
using CactusGuru.Domain.Greenhouse.Qualification.Inquiries;
using CactusGuru.Domain.Greenhouse.Qualification.Validators;
using CactusGuru.Domain.Persistance.Repositories;
using CactusGuru.Infrastructure.ObjectCreation;
using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Qualification;
using StructureMap;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class DomainRegistry : Registry
    {
        public DomainRegistry()
        {
            For<ICollectionItemCodeGenerator>().Use<SequentialCodeGenerator>();

            For<IFormatter<CollectionItem>>().Use<CollectionItemFormatter>();
            For<IFormatter<CollectionItem>>().Add<CodeFormatter>().Named("codeFormatter");
            For<IFormatter<CollectionItem>>().Add<RefCollectionItemFormatter>().Named("labelPrintRef");
            //For<IFormatter<CollectionItem>>().Use(CollectionItemLabelFormatter).Named("labelPrintSpec");
            //    return new LabelFormatter(new TaxonFormatter(new NullFormatter<Genus>()));
           
            For<IFormatter<CollectionItem>>().Add<TaxonFieldFormatter>().Named("taxonField");
         

            // Factories
            For<IFactory<Genus>>().Use<SimpleFactory<Genus>>();
            For<IFactory<Taxon>>().Use<SimpleFactory<Taxon>>();
            For<IFactory<Collector>>().Use<SimpleFactory<Collector>>();
            For<IFactory<Supplier>>().Use<SimpleFactory<Supplier>>();

            // Validators
            For<ValidatorBase<Collector>>().Use(ctx => Validator<Collector>.Create(
                ctx.GetInstance<EmptySpecification>().SetProperty(nameof(Collector.Acronym)),
                ctx.GetInstance<EmptySpecification>().SetProperty(nameof(Collector.FullName)),
                SimilaritySpec<ICollectorRepository>(ctx)));

            For<ValidatorBase<Genus>>().Use(ctx => Validator<Genus>.Create(
                ctx.GetInstance<EmptySpecification>().SetProperty(nameof(Genus.Title)),
                SimilaritySpec<IGenusRepository>(ctx)));

            For<ValidatorBase<Supplier>>().Use(ctx => Validator<Supplier>.Create(
                ctx.GetInstance<EmptySpecification>().SetProperty(nameof(Supplier.FullName)),
                SimilaritySpec<ISupplierRepository>(ctx)));

            For<ValidatorBase<Taxon>>().Use(ctx => Validator<Taxon>.Create(
                ctx.GetInstance<EmptySpecification>().SetProperty(nameof(Taxon.Genus)),
                ctx.GetInstance<EmptySpecification>().SetProperty(nameof(Taxon.Species)),
                SimilaritySpec<ITaxonRepository>(ctx)));

            For<ValidatorBase<CollectionItem>>().Use(ctx => new CollectionItemValidator(ctx.GetInstance<EmptySpecification>()));

            // Inquiries
            // For<InquiryBase<Collector>>().Use(CollectorInquiry);
            For<InquiryBase<Collector>>().Use<CollectorInquiry>().Ctor<IFormatter<CollectionItem>>().IsNamedInstance("codeFormatter");
            //For<InquiryBase<Supplier>>().Use(SupplierInquiry);
            For<InquiryBase<Supplier>>().Use<SupplierInquiry>().Ctor<IFormatter<CollectionItem>>().IsNamedInstance("codeFormatter");
            //For<InquiryBase<Taxon>>().Use(TaxonInquiry);
            For<InquiryBase<Taxon>>().Use<TaxonInquiry>().Ctor<IFormatter<CollectionItem>>().IsNamedInstance("codeFormatter");
            For<InquiryBase<CollectionItemImage>>().Use(() => new NullInquiry<CollectionItemImage>());
        }

        private SimilaritySpec SimilaritySpec<TRepo>(IContext ctx)
            where TRepo : ISimilarityRepository
        {
            return new SimilaritySpec(ctx.GetInstance<TRepo>(), ctx.GetInstance<IDomainDictionary>());
        }

        //private InquiryBase<Collector> CollectorInquiry(IContext ctx)
        //{
        //    return new CollectorInquiry(ctx.GetInstance<IUnitOfWork>(), ctx.GetInstance<IFormatter<CollectionItem>>("codeFormatter"));
        //}

        //private InquiryBase<Supplier> SupplierInquiry(IContext ctx)
        //{
        //    return new SupplierInquiry(ctx.GetInstance<IUnitOfWork>(), ctx.GetInstance<IFormatter<CollectionItem>>("codeFormatter"));
        //}

        //private InquiryBase<Taxon> TaxonInquiry(IContext ctx)
        //{
        //    return new TaxonInquiry(ctx.GetInstance<IUnitOfWork>(), ctx.GetInstance<IFormatter<CollectionItem>>("codeFormatter"));
        //}

        //private IFormatter<CollectionItem> CollectionItemLabelFormatter()
        //{
        //    return new LabelFormatter(new TaxonFormatter(new NullFormatter<Genus>()));
        //}
    }
}
