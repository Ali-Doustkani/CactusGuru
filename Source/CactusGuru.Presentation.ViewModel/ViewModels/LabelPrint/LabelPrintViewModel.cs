using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Presentation.ViewModel.Framework;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint
{
    public class LabelPrintViewModel : BaseViewModel
    {
        public void Set(CollectionItemDto item)
        {
            Id = item.Id;
            Name = $"{item.Code} - {item.Name}";
            Code = item.Code;
            Genus = item.Genus;
            ReferenceInfo = item.ReferenceInfo;
            Species = item.Species;
            OnPropertyChanged(nameof(Name));
        }

        public void Set(TaxonDto taxon)
        {
            Id = taxon.Id;
            Name = taxon.Name;
            Genus = taxon.Genus;
            Species = taxon.Species;
            OnPropertyChanged(nameof(Name));
        }

        private int _count;
        public int Count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged(nameof(Count));
            }
        }

        public string Name { get; private set; }

        public Guid Id { get; private set; }

        public string Code { get; private set; }

        public string Genus { get; private set; }

        public string ReferenceInfo { get; private set; }

        public string Species { get; private set; }
    }
}