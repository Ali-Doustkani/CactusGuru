using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels
{
    public class CollectorViewModel : WorkingViewModel
    {
        public CollectorViewModel(CollectorDto dto)
            : base(dto)
        { }

        public string FullName
        {
            get { return Inner<CollectorDto>().FullName; }
            set { Inner<CollectorDto>().FullName = value; }
        }

        public string Acronym
        {
            get { return Inner<CollectorDto>().Acronym; }
            set { Inner<CollectorDto>().Acronym = value; }
        }

        public string FormattedName
        {
            get { return Inner<CollectorDto>().FormattedName; }
            set { Inner<CollectorDto>().FormattedName = value; }
        }

        public string Website
        {
            get { return Inner<CollectorDto>().Website; }
            set { Inner<CollectorDto>().Website = value; }
        }
    }
}
