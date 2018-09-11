using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels
{
    public class SupplierViewModel : WorkingViewModel
    {
        public string FullName
        {
            get { return Inner<SupplierDto>().FullName; }
            set { Inner<SupplierDto>().FullName = value; }
        }

        public string Acronym
        {
            get { return Inner<SupplierDto>().Acronym; }
            set { Inner<SupplierDto>().Acronym = value; }
        }

        public string FormattedName
        {
            get { return Inner<SupplierDto>().FormattedName; }
            set { Inner<SupplierDto>().FormattedName = value; }
        }

        public string Website
        {
            get { return Inner<SupplierDto>().Website; }
            set { Inner<SupplierDto>().Website = value; }
        }
    }
}
