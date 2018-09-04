using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels
{
    public class CollectionItemEditorViewModel : EditorViewModel<CollectionItemViewModel>, IDataErrorInfo
    {
        public CollectionItemEditorViewModel(
            ICollectionItemViewProvider dataProvider,
            IDialogService dialogService,
            INavigationService navigationService,
            EventAggregator eventAggregator)
              : base(dataProvider, new CollectionItemViewModelFactory(), dialogService)
        {
            _dataProvider = dataProvider;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            GotoTaxaCommand = new RelayCommand(GotoTaxa);
            GotoCollectorsCommand = new RelayCommand(GotoCollectors);
            GotoSuppliersCommand = new RelayCommand(GotoSuppliers);
        }

        private readonly ICollectionItemViewProvider _dataProvider;
        private readonly INavigationService _navigationService;
        private readonly EventAggregator _eventAggregator;
        private CollectionItemViewModel _itemToEdit;

        public ICommand GotoTaxaCommand { get; private set; }
        public ICommand GotoCollectorsCommand { get; private set; }
        public ICommand GotoSuppliersCommand { get; private set; }

        public override string Title => "اقلام";

        #region LOADING 

        protected override void PrepareForLoad()
        {
            LoadCollectors();
            LoadTaxa();
            LoadSuppliers();
            LoadIncomeTypes();

            if (_itemToEdit == null)
                PrepareForAdd();
            else
            {
                WorkingItem = _itemToEdit;
                PrepareForEdit();
            }
        }

        private void LoadSuppliers()
        {
            Suppliers = new ObservableCollection<SupplierDto>(_dataProvider.GetSuppliers());
        }

        private void LoadTaxa()
        {
            Taxa = new ObservableCollection<TaxonDto>(_dataProvider.GetTaxa());
        }

        private void LoadCollectors()
        {
            Collectors = new ObservableCollection<CollectorDto>(_dataProvider.GetCollectors());
        }

        private void LoadIncomeTypes()
        {
            var list = new List<IncomeTypeRowItem>();
            foreach (var dto in _dataProvider.GetIncomeTypes())
                list.Add(new IncomeTypeRowItem(dto.Value));
            IncomeTypes = new ObservableCollection<IncomeTypeRowItem>(list);
        }

        #endregion

        #region VIEW MODEL

        public ObservableCollection<TaxonDto> Taxa { get; set; }

        public TaxonDto Taxon
        {
            get
            {
                if (WorkingItem?.Taxon == null)
                    return null;
                return Taxa.Single(x => x.Id.Equals(WorkingItem.Taxon.Value));
            }
            set { WorkingItem.Taxon = value?.Id; }
        }

        public ObservableCollection<SupplierDto> Suppliers { get; set; }

        public SupplierDto Supplier
        {
            get
            {
                if (WorkingItem?.Supplier == null)
                    return null;
                return Suppliers.Single(x => x.Id.Equals(WorkingItem.Supplier.Value));
            }
            set { WorkingItem.Supplier = value?.Id; }
        }

        public ObservableCollection<CollectorDto> Collectors { get; set; }

        public CollectorDto Collector
        {
            get
            {
                if (WorkingItem?.Collector == null)
                    return null;
                return Collectors.Single(x => x.Id.Equals(WorkingItem.Collector.Value));
            }
            set { WorkingItem.Collector = value?.Id; }
        }

        public string IncomeDate { get; set; }

        public ObservableCollection<IncomeTypeRowItem> IncomeTypes { get; set; }

        public IncomeTypeRowItem IncomeType
        {
            get
            {
                if (WorkingItem?.IncomeType == null)
                    return null;
                return new IncomeTypeRowItem(WorkingItem.IncomeType.Value);
            }
            set
            {
                if (value.Value == IncomeTypeRowItem.PLANT)
                    WorkingItem.IncomeType = IncomeTypeDto.Plant;
                else
                    WorkingItem.IncomeType = IncomeTypeDto.Seed;
            }
        }

        #endregion

        #region NAVIGATIONS

        private void GotoTaxa()
        {
            _navigationService.GotoTaxa();
            LoadTaxa();
            OnPropertyChanged("Taxa");
        }

        private void GotoCollectors()
        {
            _navigationService.GotoCollectors();
            LoadCollectors();
            OnPropertyChanged(nameof(Collectors));
        }

        private void GotoSuppliers()
        {
            _navigationService.GotoSuppliers();
            LoadSuppliers();
            OnPropertyChanged("Suppliers");
        }

        #endregion

        #region IDataErrorInfo

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName != nameof(IncomeDate)) return null;
                if (!DateIsValid())
                    return "تاریخ معتبر نیست.";
                return null;
            }
        } 

        #endregion

        private bool DateIsValid()
        {
            if (string.IsNullOrEmpty(IncomeDate)) return true;
            return DateUtil.IsValid(IncomeDate);
        }

        private void FillDate()
        {
            if (DateUtil.IsValid(IncomeDate))
                WorkingItem.IncomeDate = DateUtil.FromPersianDate(IncomeDate);
            else if (string.IsNullOrEmpty(IncomeDate))
                WorkingItem.IncomeDate = null;
        }

        protected override void AddImp()
        {
            FillDate();
            base.AddImp();
            _eventAggregator.NotifyOthers(WorkingItem.InnerObject, OperationType.Add);
        }

        protected override void EditImp()
        {
            FillDate();
            base.EditImp();
            _eventAggregator.NotifyOthers(WorkingItem.InnerObject, OperationType.Update);
        }

        protected override CollectionItemViewModel DeleteImp()
        {
            var deletedItem = base.DeleteImp();
            if (deletedItem != null)
            {
                _eventAggregator.NotifyOthers(deletedItem.InnerObject, OperationType.Delete);
                _navigationService.CloseCurrentView();
            }
            return deletedItem;
        }

        protected override bool CanSave()
        {
            return base.CanSave() && DateIsValid();
        }

        protected override bool CanSaveNew()
        {
            return base.CanSaveNew() && DateIsValid();
        }

        public void PrepareForEdit(Guid id)
        {
            _itemToEdit = _viewModelFactory.Create(_dataProvider.GetCollectionItem(id));
            if (_itemToEdit.IncomeDate.HasValue)
                IncomeDate = DateUtil.ToPersianDate(_itemToEdit.IncomeDate.Value);
        }
    }
}
