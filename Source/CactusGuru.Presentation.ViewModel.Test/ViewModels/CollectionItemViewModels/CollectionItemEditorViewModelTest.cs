using CactusGuru.Application.Common;
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
        public void LoadInfo()
        {
            var info = new LoadInfoDto
            {
                Collectors = Enumerable.Repeat(new CollectorDto(), 2),
                IncomeTypes = Enumerable.Repeat(new IncomeTypeDto(), 2),
                Suppliers = Enumerable.Repeat(new SupplierDto(), 3),
                Taxa = Enumerable.Repeat(new TaxonDto(), 4)
            };
            The(x => x.LoadInfoAsync()).Returns(info);

            Load();

            MakeSure(x => x.Collectors.Count).Is(2);
            MakeSure(x => x.IncomeTypes.Count).Is(2);
            MakeSure(x => x.Suppliers.Count).Is(3);
            MakeSure(x => x.Taxa.Count).Is(4);
        }
    }
}