using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Test.Framework;
using CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Test.ViewModels.SupplierViewModels
{
    [TestClass]
    public class SupplierEditorViewModelTest : ViewModelTestBase<SimpleEditorViewModel<SupplierViewModel>, ISupplierViewProvider>
    {
        protected override SimpleEditorViewModel<SupplierViewModel> Make()
        {
            return new SupplierEditorViewModel(dataProvider.Object);
        }

        [TestMethod]
        public void Load_LoadSuppliers()
        {
            The(x => x.GetListAsync()).ReturnsCollection();

            Load();

            MakeSure(x => x.ItemSource.Count).Is(2);
        }

        [TestMethod]
        public void OnLoad_IfThereIsAnySupplier_EditIsEnabled()
        {
            Assert.IsFalse(viewModel.PrepareForEditCommand.CanExecute(null));

            viewModel.WorkingItem = new SupplierViewModel();
            Assert.IsTrue(viewModel.PrepareForEditCommand.CanExecute(null));
        }

        [TestMethod]
        public void OnLoad_IsNotView_IsFalse()
        {
            Assert.IsFalse(viewModel.State.IsNotView);
        }

        [TestMethod]
        public void IsNotView_WhenAdding_True()
        {
            dataProvider.Setup(x => x.Build()).Returns(new SupplierDto());

            PrepareForAdd();

            Assert.IsTrue(viewModel.State.IsNotView);
        }

        [TestMethod]
        public void IsNotView_WhenEditing_True()
        {
            SetWorking();

            PrepareForEdit();

            Assert.IsTrue(viewModel.State.IsNotView);
        }

        [TestMethod]
        public void IsNotView_AfterAdding_False()
        {
            PrepareForAdd();
            Cancel(User.Accepted);

            Assert.IsFalse(viewModel.State.IsNotView);
        }

        [TestMethod]
        public void OnLoad_SelectFirstItem()
        {
            The(x => x.GetListAsync()).ReturnsCollection();

            Load();

            MakeSure(x => x.ItemSource.First()).Is(viewModel.WorkingItem);
        }

        [TestMethod]
        public void IsSaveEnabled_WhenStateIsView_False()
        {
            Assert.IsFalse(viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void IsSaveEnabled_WhenAddingOrEditing_True()
        {
            PrepareForAdd();
            Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));
            Cancel(User.Accepted);

            PrepareForEdit();
            Assert.IsTrue(viewModel.SaveCommand.CanExecute(null));
        }

        [TestMethod]
        public void IsCancelEnabled_WhenStateIsView_False()
        {
            Assert.IsFalse(viewModel.CancelCommand.CanExecute(null));
        }

        [TestMethod]
        public void IsCancelEnabled_WhenAddingOrEditing_True()
        {
            PrepareForAdd();
            Assert.IsTrue(viewModel.CancelCommand.CanExecute(null));
            Cancel(User.Accepted);

            PrepareForEdit();
            Assert.IsTrue(viewModel.CancelCommand.CanExecute(null));
        }

        [TestMethod]
        public void PrepareForEdit_CopySelectedItemForUndo()
        {
            SetWorking();

            PrepareForEdit();

            dataProvider.Verify(x => x.Copy(It.IsAny<SupplierDto>()), Times.Once());
        }

        [TestMethod]
        public void Cancel_CopyOriginalToCurrent()
        {
            var original = new SupplierDto { FullName = "mesagarden" };
            dataProvider.Setup(x => x.Copy(It.IsAny<SupplierDto>())).Returns(original);
            SetWorking();

            PrepareForEdit();
            Cancel(User.Accepted);

            dataProvider.Verify(x => x.CopyTo(original, It.IsAny<SupplierDto>()), Times.Once());
        }

        [TestMethod]
        public void Cancel_AskBeforeDoingSo()
        {
            Cancel(User.NotAccepted);

            dataProvider.Verify(x => x.CopyTo(It.IsAny<SupplierDto>(), It.IsAny<SupplierDto>()), Times.Never);
        }

        [TestMethod]
        public void DeleteSelectedItem()
        {
            Load();
            SetWorking();

            Delete(User.Accepted);

            dataProvider.Verify(x => x.Delete(viewModel.WorkingItem.InnerObject), Times.Once);
        }

        [TestMethod]
        public void Delete_AskBeforeDeleting()
        {
            Delete(User.NotAccepted);

            dataProvider.Verify(x => x.Delete(It.IsAny<SupplierDto>()), Times.Never);
        }

        [TestMethod]
        public void Delete_IfInquiryHasError_DoNotDelete()
        {
            SetWorking();
            dataProvider.Setup(x => x.Delete(It.IsAny<TransferObjectBase>())).Throws(new ErrorHappenedException("ERR"));

            Delete(User.Accepted);

            dialogService.Verify(x => x.Error(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void Delete_RemoveFromSuppliersToo()
        {
            Load();
            var suppViewModel = SetWorking();
            viewModel.WorkingItem = suppViewModel;
            viewModel.ItemSource.Add(suppViewModel);

            Delete(User.Accepted);

            Assert.AreEqual(0, viewModel.ItemSource.Count());
        }

        private SupplierViewModel SetWorking()
        {
            var ret = new SupplierViewModel();
            ret.InnerObject = new SupplierDto();
            viewModel.WorkingItem = ret;
            return ret;
        }
    }
}
