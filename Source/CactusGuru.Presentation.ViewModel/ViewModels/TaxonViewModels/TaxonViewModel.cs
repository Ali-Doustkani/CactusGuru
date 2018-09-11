using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels
{
    public class TaxonViewModel : WorkingViewModel
    {
        public TaxonViewModel(TaxonDto dto)
            : base(dto)
        { }

        public string FormattedName => Inner<TaxonDto>().Name;

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
    }
}
