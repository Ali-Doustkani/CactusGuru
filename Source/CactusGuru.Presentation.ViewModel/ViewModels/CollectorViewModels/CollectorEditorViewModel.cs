using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Framework;

namespace CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels
{
    public class CollectorEditorViewModel : SimpleEditorViewModel<CollectorViewModel>
    {
        public CollectorEditorViewModel(ICollectorViewProvider dataProvider)
            : base(dataProvider, new CollectorViewModelFactory(), "کلکتورها")
        {
            _dataProvider = dataProvider;
        }

        private readonly ICollectorViewProvider _dataProvider;

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
            Rules.MakeSure(nameof(Acronym)).IsNotEmpty().ValidatesForWhole(Similarity);
        }

        private string Similarity()
        {
            if (_dataProvider.HasSimilar((CollectorDto)WorkingItem.InnerObject))
                return "مشخصات کلکتور تکراری است";
            return null;
        }
    }
}
