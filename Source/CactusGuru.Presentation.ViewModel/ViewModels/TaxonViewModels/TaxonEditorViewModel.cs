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
        public TaxonEditorViewModel(ITaxonViewProvider dataProvider,
            INavigationService navigation,
            IDialogService dialogService,
            EventAggregator eventAggregator)
            : base(dataProvider, new TaxonViewModelFactory(), dialogService, "تاکسون ها")
        {
            _dataProvider = dataProvider;
            _navigation = navigation;
            _eventAggregator = eventAggregator;
            GotoGeneraCommand = new RelayCommand(GotoGenera);
        }

        private readonly ITaxonViewProvider _dataProvider;
        private readonly INavigationService _navigation;
        private readonly EventAggregator _eventAggregator;

        public ICommand GotoGeneraCommand { get; private set; }
        public ObservableCollection<GenusDto> Genera { get; private set; }

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
            Rules.MakeSure(nameof(Genus)).IsNotEmpty().ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Species)).IsNotEmpty().ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Variety)).ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(SubSpecies)).ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Forma)).ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Cultivar)).ValidatesForWhole(Similarity);
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

        public GenusDto Genus
        {
            get { return WorkingItem?.Genus; }
            set
            {
                WorkingItem.Genus = value;
                Rules.Check(nameof(Genus), value);
            }
        }

        public string Species
        {
            get { return WorkingItem?.Species; }
            set
            {
                WorkingItem.Species = value;
                Rules.Check(nameof(Species), value);
            }
        }

        public string Variety
        {
            get { return WorkingItem?.Variety; }
            set
            {
                WorkingItem.Variety = value;
                Rules.Check(nameof(Variety), value);
            }
        }

        public string SubSpecies
        {
            get { return WorkingItem?.SubSpecies; }
            set
            {
                WorkingItem.SubSpecies = value;
                Rules.Check(nameof(SubSpecies), value);
            }
        }

        public string Forma
        {
            get { return WorkingItem?.Forma; }
            set
            {
                WorkingItem.Forma = value;
                Rules.Check(nameof(Forma), value);
            }
        }

        public string Cultivar
        {
            get { return WorkingItem?.Cultivar; }
            set
            {
                WorkingItem.Cultivar = value;
                Rules.Check(nameof(Cultivar), value);
            }
        }

        private string Similarity()
        {
            if (_dataProvider.HasSimilar((TaxonDto)WorkingItem.InnerObject))
                return "تاکسونی با این مشخصات در سیستم موجود است";
            return null;
        }
    }
}