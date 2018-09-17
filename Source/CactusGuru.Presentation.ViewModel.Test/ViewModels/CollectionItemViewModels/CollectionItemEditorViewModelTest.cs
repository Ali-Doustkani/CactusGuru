using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Test.Framework;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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

            Assert.AreEqual(2, viewModel.Taxa.Count);
        }

        [TestMethod]
        public void Load_Collectors()
        {
            The(x => x.GetCollectors()).ReturnsCollection();

            Load();

            Assert.AreEqual(2, viewModel.Collectors.Count);
        }

        [TestMethod]
        public void Load_Suppliers()
        {
            The(x => x.GetSuppliers()).ReturnsCollection();

            Load();

            Assert.AreEqual(2, viewModel.Suppliers.Count);
        }

        [TestMethod]
        public void Load_IncomeTypes()
        {
            The(x => x.GetIncomeTypes()).ReturnsCollection();

            Load();

            Assert.AreEqual(2, viewModel.IncomeTypes.Count());
        }
    }
}