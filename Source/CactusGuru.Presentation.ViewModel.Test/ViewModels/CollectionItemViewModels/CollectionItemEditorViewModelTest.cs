using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Test.Framework;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CactusGuru.Presentation.ViewModel.Test.ViewModels.CollectionItemViewModels
{
    [TestClass]
    public class CollectionItemEditorViewModelTest : ViewModelTestBase<CollectionItemEditorViewModel, ICollectionItemViewProvider>
    {
        protected override CollectionItemEditorViewModel Make()
        {
            return new CollectionItemEditorViewModel(dataProvider.Object);
        }

        [TestMethod]
        public void Load_Taxa()
        {
            The(x => x.GetTaxaAsync()).ReturnsCollection();

            Load();

            MakeSure(x => x.Taxa.Count).Is(2);
        }

        [TestMethod]
        public void Load_Collectors()
        {
            The(x => x.GetCollectors()).ReturnsCollection();

            Load();

            MakeSure(vm => vm.Collectors.Count).Is(2);
        }

        [TestMethod]
        public void Load_Suppliers()
        {
            The(x => x.GetSuppliers()).ReturnsCollection();

            Load();

            MakeSure(vm => vm.Suppliers.Count).Is(2);
        }

        [TestMethod]
        public void Load_IncomeTypes()
        {
            The(x => x.GetIncomeTypes()).ReturnsCollection();

            Load();

            MakeSure(vm => vm.IncomeTypes.Count).Is(2);
        }
    }
}