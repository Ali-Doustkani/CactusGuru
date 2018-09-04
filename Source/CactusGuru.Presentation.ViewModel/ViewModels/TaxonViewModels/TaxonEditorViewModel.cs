using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels
{
    public class TaxonEditorViewModel : SimpleEditorViewModel<TaxonViewModel>
    {
        public TaxonEditorViewModel(IDataEntryViewProvider dataProvider,
            INavigationService navigation,
            IDialogService dialogService,
            EventAggregator eventAggregator)
            : base(dataProvider, new TaxonViewModelFactory(), dialogService, "تاکسون ها")
        {
            _dataProvider = (ITaxonViewProvider)dataProvider;
            _navigation = navigation;
            _eventAggregator = eventAggregator;
            GotoGeneraCommand = new RelayCommand(GotoGenera);
        }

        private readonly ITaxonViewProvider _dataProvider;
        private readonly INavigationService _navigation;
        private readonly EventAggregator _eventAggregator;

        public ICommand GotoGeneraCommand { get; private set; }

        public ObservableCollection<GenusDto> Genera { get; set; }

        protected override void AddImp()
        {
            base.AddImp();
            _eventAggregator.NotifyOthers(WorkingItem.InnerObject, OperationType.Add);
        }

        protected override void EditImp()
        {
            base.EditImp();
            _eventAggregator.NotifyOthers(WorkingItem.InnerObject, OperationType.Update);
        }

        protected override TaxonViewModel DeleteImp()
        {
            var deletedItem = base.DeleteImp();
            if (deletedItem != null)
                _eventAggregator.NotifyOthers(deletedItem.InnerObject, OperationType.Delete);
            return deletedItem;
        }

        protected override void PrepareForLoad()
        {
            LoadGenera();
        }

        private void LoadGenera()
        {
            Genera = new ObservableCollection<GenusDto>(_dataProvider.GetGenera());
            OnPropertyChanged(nameof(Genera));
        }

        private void GotoGenera()
        {
            _navigation.GotoGenera();
            LoadGenera();
        }
    }
}
