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
        public TaxonEditorViewModel(IDataEntryViewProvider dataProvider, INavigationService navigation, IDialogService dialogService, EventAggregator eventAggregator)
            : base(dataProvider, new TaxonViewModelFactory(), dialogService)
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

        public override string Title => "تاکسون ها";

        public ObservableCollection<GenusDto> Genera { get; set; }

        public GenusDto SelectedTaxonGenus
        {
            get { return WorkingItem?.Genus; }
            set { WorkingItem.Genus = value; }
        }

        public string SelectedTaxonSpecies
        {
            get { return GetStringProperty(nameof(WorkingItem.Species)); }
            set { WorkingItem.Species = value; }
        }

        public string SelectedTaxonCultivar
        {
            get { return GetStringProperty(nameof(WorkingItem.Cultivar)); }
            set { WorkingItem.Cultivar = value; }
        }

        public string SelectedTaxonForma
        {
            get { return GetStringProperty(nameof(WorkingItem.Forma)); }
            set { WorkingItem.Forma = value; }
        }

        public string SelectedTaxonSubSpecies
        {
            get { return GetStringProperty(nameof(WorkingItem.SubSpecies)); }
            set { WorkingItem.SubSpecies = value; }
        }

        public string SelectedTaxonVariety
        {
            get { return GetStringProperty(nameof(WorkingItem.Variety)); }
            set { WorkingItem.Variety = value; }
        }

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

        protected override void DeleteImp()
        {
            base.DeleteImp();
            _eventAggregator.NotifyOthers(WorkingItem.InnerObject, OperationType.Delete);
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
