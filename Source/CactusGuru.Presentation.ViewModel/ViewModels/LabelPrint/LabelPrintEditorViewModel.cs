using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using CactusGuru.Presentation.ViewModel.PrintService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint
{
    public class LabelPrintEditorViewModel : FormViewModel
    {
        public LabelPrintEditorViewModel(ILabelPrintViewProvider viewProvider, IPrintService printService)
        {
            _viewProvider = viewProvider;
            _printService = printService;

            PrintItems = Bag.Of<LabelPrintViewModel>()
                .FilterBy((vm, text) => vm.Name.Has(text) || vm.Species.Has(text))
                .Build();

            AddToPrintCommand = new RelayCommand(AddToPrint);
            ClearCollectionItemsFilterCommand = new RelayCommand(ClearSourceFilterText);
            ClearPrintItemsFilterCommand = new RelayCommand(PrintItems.ClearFilterText);
            DeleteCurrentPrintItemCommand = new RelayCommand(DeleteSelectedPrintItem);
            PrintCommand = new RelayCommand(Print, CanPrint);
            PaperTypes = new[] { new PaperRowItem(PaperType.A4, "A4"), new PaperRowItem(PaperType.TenCm, "10cm") };
            SelectedPaperType = PaperTypes.First();
        }

        private readonly ILabelPrintViewProvider _viewProvider;
        private readonly IPrintService _printService;

        public ICommand AddToPrintCommand { get; }
        public ICommand ClearCollectionItemsFilterCommand { get; }
        public ICommand ClearPrintItemsFilterCommand { get; }
        public ICommand DeleteCurrentPrintItemCommand { get; }
        public ICommand PrintCommand { get; }
        public ObservableBag<CollectionItemViewModel> CollectionItems { get; private set; }
        public ObservableBag<TaxonViewModel> Taxa { get; private set; }
        public ObservableBag<LabelPrintViewModel> PrintItems { get; }
        public IEnumerable<PaperRowItem> PaperTypes { get; }
        public CollectionItemViewModel SelectedCollectionItem { get; set; }
        public TaxonViewModel SelectedTaxon { get; set; }
        public LabelPrintViewModel SelectedPrintItem { get; set; }
        public PaperRowItem SelectedPaperType { get; set; }

        private SelectedTabPage _selectedPage;
        public SelectedTabPage SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                if (value == _selectedPage) return;
                _selectedPage = value;
                CollectionItems.ClearFilterText();
                Taxa.ClearFilterText();
                OnPropertyChanged(nameof(SourceFilterNullText));
                OnPropertyChanged(nameof(SourceFilterText));
            }
        }

        public string LabelCount
        {
            get { return $"تعداد برچسب: {PrintItems.Sum(x => x.Count)}"; }
        }

        public string SourceFilterNullText
        {
            get
            {
                if (SelectedPage == SelectedTabPage.CollectionItem)
                    return "جست و جو در اقلام مجموعه";
                return "جست و جو در تاکسون ها";
            }
        }

        public string SourceFilterText
        {
            get
            {
                if (CollectionItems == null) return string.Empty;
                if (SelectedPage == SelectedTabPage.CollectionItem)
                    return CollectionItems.FilterText;
                return Taxa.FilterText;
            }
            set
            {
                if (SelectedPage == SelectedTabPage.CollectionItem)
                    CollectionItems.FilterText = value;
                else if (SelectedPage == SelectedTabPage.Taxon)
                    Taxa.FilterText = value;
            }
        }

        protected override void OnLoad()
        {
            CollectionItems = Bag.Of<CollectionItemViewModel>()
              .FilterBy((vm, text) => vm.Name.Has(text) || vm.Code == text)
              .WithConvertor((CollectionItemDto dto) => new CollectionItemViewModel(dto))
              .WithId(x => x.InnerObject.Id)
              .LoadFrom(_viewProvider.GetCollectionItems)
              .Build();
            Taxa = Bag.Of<TaxonViewModel>()
                .FilterBy((vm, text) => vm.Name.Has(text))
                .LoadFrom(_viewProvider.GetTaxa)
                .WithConvertor((TaxonDto dto) => new TaxonViewModel(dto))
                .WithId(x => x.InnerObject.Id)
                .Build();
            OnPropertyChanged(nameof(CollectionItems));
            OnPropertyChanged(nameof(Taxa));
        }

        private void AddToPrint()
        {
            if (NothingSelected()) return;
            var count = Navigations.GetNumberFromUser();
            if (!count.Result) return;
            if (PrintItemExists())
                GetPrintItem().Count += count.Value;
            else
                AddPrintItem(count.Value);
            OnPropertyChanged(nameof(LabelCount));
        }

        private bool NothingSelected()
        {
            return (SelectedPage == SelectedTabPage.CollectionItem && SelectedCollectionItem == null) ||
            (SelectedPage == SelectedTabPage.Taxon && SelectedTaxon == null);
        }

        private bool PrintItemExists()
        {
            return PrintItems.Any(x => x.Id.Equals(SelectedId()));
        }

        private LabelPrintViewModel GetPrintItem()
        {
            return PrintItems.Single(x => x.Id.Equals(SelectedId()));
        }

        private Guid SelectedId()
        {
            return SelectedPage == SelectedTabPage.CollectionItem ? SelectedCollectionItem.InnerObject.Id : SelectedTaxon.InnerObject.Id;
        }

        private void AddPrintItem(int count)
        {
            var dto = new LabelPrintViewModel();
            dto.Count = count;
            if (SelectedPage == SelectedTabPage.CollectionItem)
                dto.Set((CollectionItemDto)SelectedCollectionItem.InnerObject);
            else if (SelectedPage == SelectedTabPage.Taxon)
                dto.Set(SelectedTaxon.InnerObject);
            PrintItems.Add(dto);
        }

        private void DeleteSelectedPrintItem()
        {
            if (Dialog.Ask("آیا از حذف آیتم اطمینان دارید؟"))
                PrintItems.Remove(SelectedPrintItem);
        }

        private void Print()
        {
            var printDtos = new List<LabelPrintItemDto>();
            foreach (var printVm in PrintItems)
                AddPrintDto(printDtos, printVm);
            _printService.PrintLabel(printDtos, SelectedPaperType.Id);
        }

        private void AddPrintDto(List<LabelPrintItemDto> printDtos, LabelPrintViewModel labelPrintItemViewModel)
        {
            for (int i = 0; i < labelPrintItemViewModel.Count; i++)
            {
                var dto = new LabelPrintItemDto();
                dto.Code = labelPrintItemViewModel.Code;
                dto.Genera = labelPrintItemViewModel.Genus;
                dto.ReferenceInfo = labelPrintItemViewModel.ReferenceInfo;
                dto.Species = labelPrintItemViewModel.Species;
                printDtos.Add(dto);
            }
        }

        private bool CanPrint()
        {
            return PrintItems.Any();
        }

        private void ClearSourceFilterText()
        {
            if (SelectedPage == SelectedTabPage.CollectionItem)
                CollectionItems.ClearFilterText();
            else if (SelectedPage == SelectedTabPage.Taxon)
                Taxa.ClearFilterText();
        }
    }
}
