using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Presentation.ViewModel.Test.Framework;
using CactusGuru.Presentation.ViewModel.ViewModels.CollectorViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Test.ViewModels.CollectorViewModelsTest
{
    [TestClass]
    public class CollectorEditorViewModelTest : ViewModelTestBase<CollectorEditorViewModel, IDataEntryViewProvider>
    {
        [TestMethod]
        public void Load_LoadCollectors()
        {
            The(x => x.GetList()).ReturnsCollection<CollectorDto>();

            viewModel.LoadCommand.Execute(null);

            Assert.AreEqual(2, viewModel.ItemSource.Count());
        }

        [TestMethod]
        public void SelectedCollectorTitle()
        {
            viewModel.WorkingItem = null;
            Assert.AreEqual(string.Empty, viewModel.SelectedCollectorFullName);

            viewModel.WorkingItem = new CollectorViewModel(new CollectorDto { FullName = "title" });
            Assert.AreEqual("title", viewModel.SelectedCollectorFullName);
        }

        [TestMethod]
        public void SelectedCollectorAcronym()
        {
            viewModel.WorkingItem = null;
            Assert.AreEqual(string.Empty, viewModel.SelectedCollectorAcronym);

            viewModel.WorkingItem = new CollectorViewModel(new CollectorDto { Acronym = "pp" });
            Assert.AreEqual("pp", viewModel.SelectedCollectorAcronym);
        }

        [TestMethod]
        public void SelectedCollectorWebsite()
        {
            viewModel.WorkingItem = null;
            Assert.AreEqual(string.Empty, viewModel.SelectedCollectorWebsite);

            viewModel.WorkingItem = new CollectorViewModel(new CollectorDto { Website = "web" });
            Assert.AreEqual("web", viewModel.SelectedCollectorWebsite);
        }
    }
}
