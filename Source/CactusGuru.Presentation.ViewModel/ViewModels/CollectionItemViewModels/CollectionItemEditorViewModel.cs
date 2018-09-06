using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels
{
    public class CollectionItemEditorViewModel : EditorViewModel<CollectionItemViewModel>
    {
        public CollectionItemEditorViewModel(
            ICollectionItemViewProvider dataProvider,
            IDialogService dialogService,
            INavigationService navigationService)
              : base(dataProvider, new CollectionItemViewModelFactory(), dialogService)
        {
            _dataProvider = dataProvider;
            _navigationService = navigationService;
            GotoTaxaCommand = new RelayCommand(_navigationService.GotoTaxa);
            GotoCollectorsCommand = new RelayCommand(_navigationService.GotoCollectors);
            GotoSuppliersCommand = new RelayCommand(_navigationService.GotoSuppliers);
        }

        private readonly ICollectionItemViewProvider _dataProvider;
        private readonly INavigationService _navigationService;
        private CollectionItemViewModel _itemToEdit;

        public ICommand GotoTaxaCommand { get; private set; }
        public ICommand GotoCollectorsCommand { get; private set; }
        public ICommand GotoSuppliersCommand { get; private set; }
        public ChangeableObservableCollection<TaxonDto> Taxa { get; private set; }
        public ChangeableObservableCollection<SupplierDto> Suppliers { get; private set; }
        public ChangeableObservableCollection<CollectorDto> Collectors { get; set; }
        public ObservableCollection<IncomeTypeRowItem> IncomeTypes { get; private set; }
        public override string Title => "اقلام";

        public string Code
        {
            get { return WorkingItem?.Code; }
            set
            {
                WorkingItem.Code = value;
                Rules.Check(nameof(Code), value);
            }
        }

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

        public SupplierDto Supplier
        {
            get
            {
                if (WorkingItem?.Supplier == null)
                    return null;
                return Suppliers.Single(x => x.Id.Equals(WorkingItem.Supplier.Value));
            }
            set
            {
                WorkingItem.Supplier = value?.Id;
                Rules.Check(nameof(Supplier), value);
            }
        }

        public string SupplierCode
        {
            get { return WorkingItem?.SupplierCode; }
            set
            {
                WorkingItem.SupplierCode = value;
                Rules.Check(nameof(SupplierCode), value);
            }
        }

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

        public void PrepareForEdit(Guid id)
        {
            _itemToEdit = _viewModelFactory.Create(_dataProvider.GetCollectionItem(id));
            if (_itemToEdit.IncomeDate.HasValue)
                IncomeDate = DateUtil.ToPersianDate(_itemToEdit.IncomeDate.Value);
        }

        protected override void PrepareForLoad()
        {
            Taxa = new ChangeableObservableCollection<TaxonDto>(_dataProvider.GetTaxa());
            Collectors = new ChangeableObservableCollection<CollectorDto>(_dataProvider.GetCollectors());
            Suppliers = new ChangeableObservableCollection<SupplierDto>(_dataProvider.GetSuppliers());
            LoadIncomeTypes();

            AddListener(Taxa);
            AddListener(Collectors);
            AddListener(Suppliers);

            Rules.MakeSure(nameof(Code)).IsNotEmpty().ValidatesForItself(CodeSimilarity);
            Rules.MakeSure(nameof(Supplier)).ValidatesFor(nameof(SupplierCode), SupplierExistance);
            Rules.MakeSure(nameof(SupplierCode)).ValidatesForItself(SupplierExistance);

            if (_itemToEdit == null)
                PrepareForAdd();
            else
            {
                WorkingItem = _itemToEdit;
                PrepareForEdit();
            }
        }

        protected override void AddImp()
        {
            FillDate();
            base.AddImp();
            NotifyOthers(WorkingItem.InnerObject, OperationType.Add);
        }

        protected override void EditImp()
        {
            FillDate();
            base.EditImp();
            NotifyOthers(WorkingItem.InnerObject, OperationType.Update);
        }

        protected override CollectionItemViewModel DeleteImp()
        {
            var deletedItem = base.DeleteImp();
            if (deletedItem != null)
            {
                NotifyOthers(deletedItem.InnerObject, OperationType.Delete);
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

        private void LoadIncomeTypes()
        {
            var list = new List<IncomeTypeRowItem>();
            foreach (var dto in _dataProvider.GetIncomeTypes())
                list.Add(new IncomeTypeRowItem(dto.Value));
            IncomeTypes = new ObservableCollection<IncomeTypeRowItem>(list);
        }

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

        private string CodeSimilarity()
        {
            if (_dataProvider.HasSimilarCode(WorkingItem.Code))
                return "کد تکراری است";
            return null;
        }

        private string SupplierExistance()
        {
            if (WorkingItem.Supplier.HasValue && string.IsNullOrEmpty(WorkingItem.SupplierCode))
                return "کد تامین کننده نیز باید مشخص شود";
            else if (!WorkingItem.Supplier.HasValue && !string.IsNullOrEmpty(WorkingItem.SupplierCode))
                return "تامین کننده نیز باید مشخص شود";
            return null;
        }
    }
}
