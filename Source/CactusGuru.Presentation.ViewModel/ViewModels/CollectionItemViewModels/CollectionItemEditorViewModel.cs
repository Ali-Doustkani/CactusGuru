using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using System;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels
{
    public class CollectionItemEditorViewModel : EditorViewModel<CollectionItemViewModel>
    {
        public CollectionItemEditorViewModel(ICollectionItemViewProvider dataProvider)
              : base(dataProvider)
        {
            _dataProvider = dataProvider;
        }

        private readonly ICollectionItemViewProvider _dataProvider;
        private CollectionItemViewModel _itemToEdit;

        public ICommand GotoTaxaCommand { get; private set; }
        public ICommand GotoCollectorsCommand { get; private set; }
        public ICommand GotoSuppliersCommand { get; private set; }
        public ObservableBag<TaxonDto> Taxa { get; private set; }
        public ObservableBag<SupplierDto> Suppliers { get; private set; }
        public ObservableBag<CollectorDto> Collectors { get; set; }
        public ObservableBag<IncomeTypeRowItem> IncomeTypes { get; private set; }

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

        public DateTime? IncomeDate
        {
            get { return WorkingItem?.IncomeDate; }
            set { WorkingItem.IncomeDate = value; }
        }

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
            _itemToEdit = CreateWorkingObject(_dataProvider.GetCollectionItem(id));
        }

        protected async override void OnLoad()
        {
            GotoTaxaCommand = new RelayCommand(Navigations.GotoTaxa);
            GotoCollectorsCommand = new RelayCommand(Navigations.GotoCollectors);
            GotoSuppliersCommand = new RelayCommand(Navigations.GotoSuppliers);

            var loadInfo = await _dataProvider.LoadInfoAsync();
            Taxa = Bag.Of<TaxonDto>()
                .WithId(x => x.Id)
                .WithSource(loadInfo.Taxa)
                .Build();
            Collectors = Bag.Of<CollectorDto>()
                .WithId(x => x.Id)
                .WithSource(loadInfo.Collectors)
                .Build();
            Suppliers = Bag.Of<SupplierDto>()
                .WithId(x => x.Id)
                .WithSource(loadInfo.Suppliers)
                .Build();
            IncomeTypes = Bag.Of<IncomeTypeRowItem>()
                .WithConvertor<IncomeTypeDto>(x => new IncomeTypeRowItem(x.Value))
                .WithSource(loadInfo.IncomeTypes)
                .Build();

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

        protected override CollectionItemViewModel DeleteImp()
        {
            var deletedItem = base.DeleteImp();
            if (deletedItem != null)
            {
                NotifyOthers(deletedItem.InnerObject, OperationType.Delete);
                Navigations.CloseCurrentView();
            }
            return deletedItem;
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