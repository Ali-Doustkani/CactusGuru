using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.DataSourceManagement;
using CactusGuru.Presentation.ViewModel.NavigationService;

namespace CactusGuru.Presentation.ViewModel.ViewModels.GenusViewModels
{
    public class GenusEditorViewModel : SimpleEditorViewModel<GenusViewModel>
    {
        public GenusEditorViewModel(IDataEntryViewProvider dataProvider,
            IDialogService dialogService)
            : base(dataProvider, new GenusViewModelFactory(), dialogService)
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
