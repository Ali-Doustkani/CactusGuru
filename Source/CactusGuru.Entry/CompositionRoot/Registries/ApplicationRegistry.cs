using CactusGuru.Application.Implementation;
using CactusGuru.Application.Implementation.Services;
using CactusGuru.Application.Implementation.ViewProviders;
using CactusGuru.Application.Implementation.ViewProviders.ImageGallery;
using CactusGuru.Application.Implementation.ViewProviders.ImageList;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Domain.Persistance.Repositories;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class ApplicationRegistry : Registry
    {
        public ApplicationRegistry()
        {
            For<IDataEntryViewProvider>().Use<GenusViewProvider>().Named("genus");
            For<IDataEntryViewProvider>().Use<TaxonViewProvider>().Named("taxon");
            For<IDataEntryViewProvider>().Use<CollectorViewProvider>().Named("collector");
            For<IDataEntryViewProvider>().Use<SupplierViewProvider>().Named("supplier");

            For<AssemblerBase<CollectionItem, CollectionItemDto>>()
              .Use<CollectionItemAssembler>();
               //.Ctor<IFormatter<CollectionItem>>().IsNamedInstance("taxonField");


            For<AssemblerBase<CollectionItem, Application.ViewProviders.LabelPrinting.CollectionItemDto>>()
                .Use<Application.Implementation.ViewProviders.LabelPrinting.CollectionItemAssembler>()
                .Ctor<IFormatter<CollectionItem>>("referenceInfoFormatter").IsNamedInstance("labelPrintRef")
                .Ctor<IFormatter<CollectionItem>>("itemFormatter").IsNamedInstance("labelPrintSpec");

            For<AssemblerBase<Taxon, Application.ViewProviders.LabelPrinting.TaxonDto>>()
                .Use<Application.Implementation.ViewProviders.LabelPrinting.TaxonAssembler>()
                .Ctor<IFormatter<Taxon>>("speciesFormatter").IsNamedInstance("labelPrintSpec");


            // For<AssemblerBase<Taxon, Application.ViewProviders.LabelPrinting.TaxonDto>>().Use(ctx => CreateLabelPrintTaxonAssembler(ctx));

            For<AssemblerBase<CollectionItemImage, Application.ViewProviders.ImageList.ImageDto>>().Use(ctx => CreateImageListItemAssembler(ctx));
            For<FileSaver>().Use(ctx => CreateFileSaver(ctx));
            For<InstagramPackageMaker>().Use(ctx => CreateInstagramFileSaver(ctx));
        }

        //private AssemblerBase<CollectionItem, Application.ViewProviders.LabelPrinting.CollectionItemDto> CreateLabelPrintItemAssembler(IContext ctx)
        //{
        //    return new Application.Implementation.ViewProviders.LabelPrinting.CollectionItemAssembler(
        //        ctx.GetInstance<IFormatter<CollectionItem>>(),
        //        ctx.GetInstance<IFormatter<CollectionItem>>("labelPrintRef"),
        //        ctx.GetInstance<IFormatter<CollectionItem>>("labelPrintSpec"));
        //}

        //private AssemblerBase<Taxon, Application.ViewProviders.LabelPrinting.TaxonDto> CreateLabelPrintTaxonAssembler(IContext ctx)
        //{
        //    return new Application.Implementation.ViewProviders.LabelPrinting.TaxonAssembler(
        //        ctx.GetInstance<IFormatter<Taxon>>("labelPrintSpec"),
        //        ctx.GetInstance<IFormatter<Taxon>>());
        //}



        private AssemblerBase<CollectionItemImage, Application.ViewProviders.ImageList.ImageDto> CreateImageListItemAssembler(IContext ctx)
        {
            return new ImageAssembler(ctx.GetInstance<ICollectionItemRepository>(), ctx.GetInstance<IFormatter<CollectionItem>>("taxonField"));
        }

        private FileSaver CreateFileSaver(IContext ctx)
        {
            return new FileSaver(ctx.GetInstance<ICollectionItemImageRepository>(),
                ctx.GetInstance<ICollectionItemRepository>(),
                ctx.GetInstance<IFormatter<CollectionItem>>("codeFormatter"));
        }

        private InstagramPackageMaker CreateInstagramFileSaver(IContext ctx)
        {
            return new InstagramPackageMaker(ctx.GetInstance<ICollectionItemImageRepository>(),
                ctx.GetInstance<ICollectionItemRepository>(),
                ctx.GetInstance<IFormatter<CollectionItem>>("codeFormatter"),
                ctx.GetInstance<IFormatter<DateTime>>("monthNumber"));
        }
    }
}
