using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;

namespace CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels
{
    public class SupplierEditorViewModel : SimpleEditorViewModel<SupplierViewModel>
    {
        public SupplierEditorViewModel(ISupplierViewProvider dataProvider)
            : base(dataProvider)
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

        protected override bool Filter(SupplierViewModel vm, string text) => vm.FullName.Has(text) || vm.Acronym.Has(text) || vm.Website.Has(text);

        private string Similarity()
        {
            if (_dataProvider.HasSimilar((SupplierDto)WorkingItem.InnerObject))
                return "This supplier already exists";
            return null;
        }
    }
}
