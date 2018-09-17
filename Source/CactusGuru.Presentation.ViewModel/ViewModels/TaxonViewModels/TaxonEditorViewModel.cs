using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels
{
    public class TaxonEditorViewModel : SimpleEditorViewModel<TaxonViewModel>
    {
        public TaxonEditorViewModel(ITaxonViewProvider dataProvider)
            : base(dataProvider)
        {
            _dataProvider = dataProvider;
            GotoGeneraCommand = new RelayCommand(() => Navigations.GotoGenera());
        }

        private readonly ITaxonViewProvider _dataProvider;

        public ICommand GotoGeneraCommand { get; private set; }
        public ObservableBag<GenusDto> Genera { get; private set; }

        protected async override void OnLoad()
        {
            base.OnLoad();
            Rules.MakeSure(nameof(Genus)).IsNotEmpty().ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Species)).IsNotEmpty().ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Variety)).ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(SubSpecies)).ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Forma)).ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Cultivar)).ValidatesForWhole(Similarity);
            Genera = await Bag.Of<GenusDto>()
                .WithId(x => x.Id)
                .Loads(_dataProvider.GetGenera)
                .Build();
            AddListener(Genera);
            OnPropertyChanged(nameof(Genera));
        }

        protected override bool Filter(TaxonViewModel vm, string text)
        {
            return vm.Species.Has(text) ||
                        vm.Genus.Name.Has(text) ||
                        vm.Variety.Has(text) ||
                        vm.Cultivar.Has(text) ||
                        vm.SubSpecies.Has(text);
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

        protected override void OnSomethingHappened(NotificationEventArgs info)
        {
            base.OnSomethingHappened(info);
            var genus = info.Object as GenusDto;
            if (genus == null) return;
            foreach (var item in ItemSource)
                if (item.Genus.Id == genus.Id)
                    item.InnerObject = _dataProvider.Get(item.InnerObject.Id);
        }
    }
}