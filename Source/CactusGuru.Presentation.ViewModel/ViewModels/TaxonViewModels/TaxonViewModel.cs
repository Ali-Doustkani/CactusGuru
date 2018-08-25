using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels
{
    public class TaxonViewModel : WorkingViewModel
    {
        public TaxonViewModel(TaxonDto dto)
            : base(dto)
        { }

        public string Name => Inner<TaxonDto>().Name;

        public GenusDto Genus
        {
            get { return Inner<TaxonDto>().Genus; }
            set { Inner<TaxonDto>().Genus = value; }
        }

        public string Species
        {
            get { return Inner<TaxonDto>().Species; }
            set { Inner<TaxonDto>().Species = value; }
        }

        public string Cultivar
        {
            get { return Inner<TaxonDto>().Cultivar; }
            set { Inner<TaxonDto>().Cultivar = value; }
        }

        public string Forma
        {
            get { return Inner<TaxonDto>().Forma; }
            set { Inner<TaxonDto>().Forma = value; }
        }

        public string SubSpecies
        {
            get { return Inner<TaxonDto>().SubSpecies; }
            set { Inner<TaxonDto>().SubSpecies = value; }
        }

        public string Variety
        {
            get { return Inner<TaxonDto>().Variety; }
            set { Inner<TaxonDto>().Variety = value; }
        }

        public override string FilterTarget => Genus.Name + Species + Cultivar + Forma + SubSpecies + Variety;

        protected override void NotifyAll()
        {
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Genus));
            OnPropertyChanged(nameof(Species));
            OnPropertyChanged(nameof(Forma));
            OnPropertyChanged(nameof(SubSpecies));
            OnPropertyChanged(nameof(Variety));
        }
    }
}
