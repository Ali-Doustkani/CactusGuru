using CactusGuru.Application.ViewProviders.LabelPrinting;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using CactusGuru.Presentation.ViewModel.Services.Printings;
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
            PrintItems = Bag.Empty<LabelPrintViewModel>();
            AddToPrintCommand = new RelayCommand(AddToPrint);
            ClearCollectionItemsFilterCommand = new RelayCommand(ClearSourceFilterText);
            ClearPrintItemsFilterCommand = new RelayCommand(() => PrintItems.ClearFilterText());
            DeleteCurrentPrintItemCommand = new RelayCommand(DeleteSelectedPrintItem);
            PrintCommand = new RelayCommand(Print, CanPrint);
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
        public ObservableBag<LabelPrintViewModel> PrintItems { get; private set; }
        public CollectionItemViewModel SelectedCollectionItem { get; set; }
        public TaxonViewModel SelectedTaxon { get; set; }
        public LabelPrintViewModel SelectedPrintItem { get; set; }

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
                OnPropertyChanged(nameof(SourceFilterText));
            }
        }

        public string LabelCount
        {
            get { return $"Count: {PrintItems.Sum(x => x.Count)}"; }
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

        protected async override void OnLoad()
        {
            PrintItems = Bag.Of<LabelPrintViewModel>()
               .FilterBy((vm, text) => vm.Name.Has(text) || vm.Species.Has(text))
               .Build();
            var info = await _viewProvider.LoadInfoAsync();
            CollectionItems = Bag.Of<CollectionItemViewModel>()
              .FilterBy((vm, text) => vm.Name.Has(text) || vm.Code == text)
              .WithConvertor((CollectionItemDto dto) => new CollectionItemViewModel(dto))
              .WithId(x => x.InnerObject.Id)
              .WithSource(info.CollectionItems)
              .Build();
            Taxa = Bag.Of<TaxonViewModel>()
                .FilterBy((vm, text) => vm.Name.Has(text))
                .WithConvertor((TaxonDto dto) => new TaxonViewModel(dto))
                .WithSource(info.Taxa)
                .WithId(x => x.InnerObject.Id)
                .Build();
            OnPropertyChanged(nameof(PrintItems));
            OnPropertyChanged(nameof(CollectionItems));
            OnPropertyChanged(nameof(Taxa));
            LoaderState.ToIdle();
        }

        private void AddToPrint()
        {
            if (NothingSelected()) return;
            if (PrintItemExists())
                GetPrintItem().Count += 1;
            else
                AddPrintItem();
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

        private void AddPrintItem()
        {
            var dto = new LabelPrintViewModel();
            dto.Count = 1;
            if (SelectedPage == SelectedTabPage.CollectionItem)
                dto.Set((CollectionItemDto)SelectedCollectionItem.InnerObject);
            else if (SelectedPage == SelectedTabPage.Taxon)
                dto.Set(SelectedTaxon.InnerObject);

            dto.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == nameof(LabelPrintViewModel.Count))
                    OnPropertyChanged(nameof(LabelCount));
            };

            PrintItems.Add(dto);
        }

        private void DeleteSelectedPrintItem()
        {
            if (Dialog.AskForDelete())
                PrintItems.Remove(SelectedPrintItem);
        }

        private void Print()
        {
            var printDtos = new List<LabelPrintItemDto>();
            foreach (var printVm in PrintItems)
                AddPrintDto(printDtos, printVm);
            _printService.PrintLabel(printDtos);
        }

        private void AddPrintDto(List<LabelPrintItemDto> printDtos, LabelPrintViewModel labelPrintItemViewModel)
        {
            for (int i = 0; i < labelPrintItemViewModel.Count; i++)
            {
                var dto = new LabelPrintItemDto();
                dto.Code = labelPrintItemViewModel.Code;
                dto.Genus = labelPrintItemViewModel.Genus;
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
