using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure.EventAggregation;
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
            return new CollectionItemEditorViewModel(dataProvider.Object, dialogService.Object, navigationService.Object, new EventAggregator());
        }

        [TestMethod]
        public void Load_Taxa()
        {
            The(x => x.GetTaxa()).ReturnsCollection();

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

        [TestMethod]
        public void GotoTaxaCommand()
        {
            The(x => x.GetTaxa()).ReturnsEmptyCollection();
            Load();
            Assert.AreEqual(0, viewModel.Taxa.Count);

            The(x => x.GetTaxa()).ReturnsCollection();

            viewModel.GotoTaxaCommand.Execute(null);

            MustNavigatedTo(x => x.GotoTaxa());
            Assert.AreEqual(2, viewModel.Taxa.Count);
        }

        [TestMethod]
        public void GotoCollectorsCommand()
        {
            The(x => x.GetCollectors()).ReturnsEmptyCollection();
            Load();
            Assert.AreEqual(0, viewModel.Collectors.Count);

            The(x => x.GetCollectors()).ReturnsCollection();

            viewModel.GotoCollectorsCommand.Execute(null);

            MustNavigatedTo(x => x.GotoCollectors());
            Assert.AreEqual(2, viewModel.Collectors.Count);
        }

        [TestMethod]
        public void GotoSuppliersCommand()
        {
            The(x => x.GetSuppliers()).ReturnsEmptyCollection();
            Load();
            Assert.AreEqual(0, viewModel.Suppliers.Count);

            The(x => x.GetSuppliers()).ReturnsCollection();

            viewModel.GotoSuppliersCommand.Execute(null);

            MustNavigatedTo(x => x.GotoSuppliers());
            Assert.AreEqual(2, viewModel.Suppliers.Count);
        }
    }
}