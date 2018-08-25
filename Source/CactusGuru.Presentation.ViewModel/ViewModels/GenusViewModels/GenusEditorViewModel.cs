using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;

namespace CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels
{
    public class GeneraEditorViewModel : SimpleEditorViewModel<GenusViewModel>
    {
        public GeneraEditorViewModel(IDataEntryViewProvider dataProvider,
            IDialogService dialogService,
            IWindowController windowController)
            : base(dataProvider, new GenusViewModelFactory(), dialogService, windowController)
        {
            ItemSource = new FilterDataSource<GenusViewModel>();
        }

        public override string Title => "جنس ها";

        public string SelectedGenusName
        {
            get { return GetStringProperty(nameof(WorkingItem.Name)); }
            set { WorkingItem.Name = value; }
        }

        public override void NotifyAllPropertiesChanged()
        {
            OnPropertyChanged(nameof(SelectedGenusName));
        }
    }
}
