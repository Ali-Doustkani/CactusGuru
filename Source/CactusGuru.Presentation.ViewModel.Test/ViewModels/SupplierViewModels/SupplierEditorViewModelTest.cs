﻿using CactusGuru.Application.Common;
using CactusGuru.Application.ViewProviders;
using CactusGuru.Infrastructure;
using CactusGuru.Presentation.ViewModel.Test.Framework;
using CactusGuru.Presentation.ViewModel.ViewModels.SupplierViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;

namespace CactusGuru.Presentation.ViewModel.Test.ViewModels.SupplierViewModels
{
    [TestClass]
    public class SupplierEditorViewModelTest : ViewModelTestBase<SupplierEditorViewModel, IDataEntryViewProvider>
    {
        [TestMethod]
        public void Load_LoadSuppliers()
        {
            The(x => x.GetList()).ReturnsCollection<SupplierDto>();

            Load();

            Assert.AreEqual(2, viewModel.ItemSource.Count());
        }

        [TestMethod]
        public void OnLoad_IfThereIsAnySupplier_EditIsEnabled()
        {
            Assert.IsFalse(viewModel.PrepareForEditCommand.CanExecute(null));

            viewModel.WorkingItem = new SupplierViewModel(new SupplierDto());
            Assert.IsTrue(viewModel.PrepareForEditCommand.CanExecute(null));
        }

        [TestMethod]
        public void OnLoad_IsEditorOn_IsFalse()
        {
            Assert.IsFalse(viewModel.IsEditorOn);
        }

        [TestMethod]
        public void IsEditorOn_WhenAdding_True()
        {
            dataProvider.Setup(x => x.Build()).Returns(new SupplierDto());

            PrepareForAdd();

            Assert.IsTrue(viewModel.IsEditorOn);
        }

        [TestMethod]
        public void IsEditorOn_WhenEditing_True()
        {
            SetWorking();

            PrepareForEdit();

            Assert.IsTrue(viewModel.IsEditorOn);
        }

        [TestMethod]
        public void IsEditorOn_AfterAdding_False()
        {
            PrepareForAdd();
            Cancel(User.Accepted);

            Assert.IsFalse(viewModel.IsEditorOn);
        }

        [TestMethod]
        public void OnLoad_SelectFirstItem()
        {
            The(x => x.GetList()).ReturnsCollection<SupplierDto>();

            Load();

            Assert.AreEqual(viewModel.ItemSource.First(), viewModel.WorkingItem);
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
            var suppViewModel = SetWorking();
            viewModel.WorkingItem = suppViewModel;
            viewModel.ItemSource.Add(suppViewModel);

            Delete(User.Accepted);

            Assert.AreEqual(0, viewModel.ItemSource.Count());
        }

        private SupplierViewModel SetWorking()
        {
            var ret = new SupplierViewModel(new SupplierDto());
            viewModel.WorkingItem = ret;
            return ret;
        }
    }
}