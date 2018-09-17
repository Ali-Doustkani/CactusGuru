using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Test.Framework;
using CactusGuru.Presentation.ViewModel.ViewModels.TaxonViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Test.ViewModels.TaxonViewModels
{
    [TestClass]
    public class TaxonEditorViewModelTest : ViewModelTestBase<TaxonEditorViewModel, ITaxonViewProvider>
    {
        protected override TaxonEditorViewModel Make()
        {
            return new TaxonEditorViewModel(dataProvider.Object);
        }

        [TestMethod]
        public void Load_LoadTaxa()
        {
            The(x => x.GetListAsync()).ReturnsCollection();

            Load();

            MakeSure(vm => vm.ItemSource.Count).Is(2);
        }

        [TestMethod]
        public void Load_LoadGenera()
        {
            The(x => x.GetGenera()).ReturnsCollection();

            Load();

            Assert.AreEqual(2, viewModel.Genera.Count);
        }

        [TestMethod]
        public void Load_SetFirstItemAsSelectedItem()
        {
            The(x => x.GetListAsync()).ReturnsCollection();

            Load();

            MakeSure(vm => vm.ItemSource.First()).Is(viewModel.WorkingItem);
        }
    }
}
