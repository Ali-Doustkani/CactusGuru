using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels
{
    public class SupplierEditorViewModel : SimpleEditorViewModel<SupplierViewModel>
    {
        public SupplierEditorViewModel(ISupplierViewProvider dataProvider)
            : base(dataProvider, new SupplierViewModelFactory())
        {
            _dataProvider = dataProvider;
        }

        private readonly ISupplierViewProvider _dataProvider;

        public string FullName
        {
            get { return WorkingItem?.FullName; }
            set
            {
                WorkingItem.FullName = value;
                Rules.Check(nameof(FullName), value);
            }
        }

        public string Acronym
        {
            get { return WorkingItem?.Acronym; }
            set
            {
                WorkingItem.Acronym = value;
                Rules.Check(nameof(Acronym), value);
            }
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            Rules.MakeSure(nameof(FullName)).IsNotEmpty().ValidatesForWhole(Similarity);
            Rules.MakeSure(nameof(Acronym)).ValidatesForWhole(Similarity);
        }

        private string Similarity()
        {
            if (_dataProvider.HasSimilar((SupplierDto)WorkingItem.InnerObject))
                return "مشخصات تامین کننده تکراری است";
            return null;
        }
    }
}
