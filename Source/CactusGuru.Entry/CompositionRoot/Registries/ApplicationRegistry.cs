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
using CactusGuru.Infrastructure;
using System;

namespace CactusGuru.Entry.CompositionRoot.Registries
{
    public class ApplicationRegistry : RegistryBase
    {
        public ApplicationRegistry(IResolver resolver)
            : base(resolver)
        {
            For<IDataEntryViewProvider>().Use<GenusViewProvider>().Named("genus");
            For<IDataEntryViewProvider>().Use<TaxonViewProvider>().Named("taxon");
            For<IDataEntryViewProvider>().Use<CollectorViewProvider>().Named("collector");
            For<IDataEntryViewProvider>().Use<SupplierViewProvider>().Named("supplier");
            For<AssemblerBase<CollectionItem, CollectionItemDto>>().Use(CreateImageGalleryCollectionItemAssembler);
            For<AssemblerBase<CollectionItem, Application.ViewProviders.LabelPrinting.CollectionItemDto>>().Use(CreateLabelPrintItemAssembler);
            For<AssemblerBase<Taxon, Application.ViewProviders.LabelPrinting.TaxonDto>>().Use(CreateLabelPrintTaxonAssembler);
            For<AssemblerBase<CollectionItemImage, Application.ViewProviders.ImageList.ImageDto>>().Use(CreateImageListItemAssembler);
            For<FileSaver>().Use(CreateFileSaver);
            For<InstagramPackageMaker>().Use(CreateInstagramFileSaver);

            Scan(x =>
            {
                x.AssemblyContainingType<CollectionItemViewProvider>();
                x.ConnectImplementationsToTypesClosing(typeof(AssemblerBase<,>));
                x.WithDefaultConventions();
            });
        }

        private AssemblerBase<CollectionItem, Application.ViewProviders.LabelPrinting.CollectionItemDto> CreateLabelPrintItemAssembler()
        {
            return new Application.Implementation.ViewProviders.LabelPrinting.CollectionItemAssembler(Res<IFormatter<CollectionItem>>(),
                Res<IFormatter<CollectionItem>>("labelPrintRef"),
                Res<IFormatter<Genus>>(),
                Res<IFormatter<CollectionItem>>("labelPrintSpec"));
        }

        private AssemblerBase<Taxon, Application.ViewProviders.LabelPrinting.TaxonDto> CreateLabelPrintTaxonAssembler()
        {
            return new Application.Implementation.ViewProviders.LabelPrinting.TaxonAssembler(Res<IFormatter<Genus>>(),
                Res<IFormatter<Taxon>>("labelPrintSpec"),
                Res<IFormatter<Taxon>>());
        }

        private AssemblerBase<CollectionItem, CollectionItemDto> CreateImageGalleryCollectionItemAssembler()
        {
            return new CollectionItemAssembler(Res<IFormatter<CollectionItem>>("taxonField"));
        }

        private AssemblerBase<CollectionItemImage, Application.ViewProviders.ImageList.ImageDto> CreateImageListItemAssembler()
        {
            return new ImageAssembler(Res<ICollectionItemRepository>(), Res<IFormatter<CollectionItem>>("taxonField"));
        }

        private FileSaver CreateFileSaver()
        {
            return new FileSaver(Res<ICollectionItemImageRepository>(),
                Res<ICollectionItemRepository>(),
                Res<IFormatter<CollectionItem>>("codeFormatter"));
        }

        private InstagramPackageMaker CreateInstagramFileSaver()
        {
            return new InstagramPackageMaker(Res<ICollectionItemImageRepository>(),
                Res<ICollectionItemRepository>(),
                Res<IFormatter<CollectionItem>>("codeFormatter"),
                Res<IFormatter<DateTime>>("monthNumber"));
        }
    }
}
