using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels
{
    public class GenusViewModel : WorkingViewModel
    {
        public string FormattedName
        {
            get { return Inner<GenusDto>().Name; }
            set { Inner<GenusDto>().Name = value; }
        }
    }
}
