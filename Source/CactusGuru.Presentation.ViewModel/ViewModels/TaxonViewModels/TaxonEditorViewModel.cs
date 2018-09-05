using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels
{
    public class TaxonEditorViewModel : SimpleEditorViewModel<TaxonViewModel>, INotifyDataErrorInfo
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

            _rules = new Rules(RaiseErrorsChanged);
            _rules.MakeSure(nameof(Genus)).IsNotEmpty().ValidatesForWhole(Similarity);
            _rules.MakeSure(nameof(Species)).IsNotEmpty().ValidatesForWhole(Similarity);
            _rules.MakeSure(nameof(Variety)).ValidatesForWhole(Similarity);
            _rules.MakeSure(nameof(SubSpecies)).ValidatesForWhole(Similarity);
            _rules.MakeSure(nameof(Forma)).ValidatesForWhole(Similarity);
            _rules.MakeSure(nameof(Cultivar)).ValidatesForWhole(Similarity);

            GotoGeneraCommand = new RelayCommand(GotoGenera);
        }

        private readonly ITaxonViewProvider _dataProvider;
        private readonly INavigationService _navigation;
        private readonly EventAggregator _eventAggregator;
        private readonly Rules _rules;

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
                _rules.Check(nameof(Genus), value);
            }
        }

        public string Species
        {
            get { return WorkingItem?.Species; }
            set
            {
                WorkingItem.Species = value;
                _rules.Check(nameof(Species), value);
            }
        }

        public string Variety
        {
            get { return WorkingItem?.Variety; }
            set
            {
                WorkingItem.Variety = value;
                _rules.Check(nameof(Variety), value);
            }
        }

        public string SubSpecies
        {
            get { return WorkingItem?.SubSpecies; }
            set
            {
                WorkingItem.SubSpecies = value;
                _rules.Check(nameof(SubSpecies), value);
            }
        }

        public string Forma
        {
            get { return WorkingItem?.Forma; }
            set
            {
                WorkingItem.Forma = value;
                _rules.Check(nameof(Forma), value);
            }
        }

        public string Cultivar
        {
            get { return WorkingItem?.Cultivar; }
            set
            {
                WorkingItem.Cultivar = value;
                _rules.Check(nameof(Cultivar), value);
            }
        }

        private string Similarity()
        {
            if (_dataProvider.HasSimilar((TaxonDto)WorkingItem.InnerObject))
                return "تاکسونی با این مشخصات در سیستم موجود است";
            return null;
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void RaiseErrorsChanged(string propname)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propname));
        }

        public bool HasErrors => _rules.AnyError();

        public IEnumerable GetErrors(string propertyName) => _rules.GetErrors(propertyName);

        protected override bool CanSave()
        {
            return base.CanSave() && !HasErrors;
        }

        protected override bool CanSaveNew()
        {
            return base.CanSaveNew() && !HasErrors;
        }

        protected override void Cancel()
        {
            base.Cancel();
            _rules.ClearErrors();
        }
    }
}
