using CactusGuru.Application.Common;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels
{
    public class GenusViewModel : WorkingViewModel
    {
        public GenusViewModel(GenusDto genus)
            : base(genus)
        {}

        public string FormattedName
        {
            get { return Inner<GenusDto>().Name; }
            set { Inner<GenusDto>().Name = value; }
        }

        public override string FilterTarget => FormattedName;
    }
}
