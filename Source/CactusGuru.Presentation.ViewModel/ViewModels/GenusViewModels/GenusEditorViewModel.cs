using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;

namespace CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels
{
    public class GenusEditorViewModel : SimpleEditorViewModel<GenusViewModel>
    {
        public GenusEditorViewModel(IGenusViewProvider dataProvider, IDialogService dialogService)
            : base(dataProvider, new GenusViewModelFactory(), dialogService, "جنس ها")
        {
            _dataProvider = dataProvider;
        }

        private readonly IGenusViewProvider _dataProvider;

        protected override void PrepareForLoad()
        {
            Rules.MakeSure(nameof(FormattedName)).IsNotEmpty().ValidatesForWhole(Similarity);
        }

        public string FormattedName
        {
            get { return WorkingItem?.FormattedName; }
            set
            {
                WorkingItem.FormattedName = value;
                Rules.Check(nameof(FormattedName), value);
            }
        }

        private string Similarity()
        {
            if (_dataProvider.HasSimilar((GenusDto)WorkingItem.InnerObject))
                return "مشخصات گونه تکراری است";
            return null;
        }
    }
}
