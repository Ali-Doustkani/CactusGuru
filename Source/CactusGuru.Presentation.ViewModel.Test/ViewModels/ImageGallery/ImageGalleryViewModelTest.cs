using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Presentation.ViewModel.NavigationService;
using CactusGuru.Presentation.ViewModel.Utils;
using CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;

namespace CactusGuru.Presentation.ViewModel.Test.ViewModels.ImageGallery
{
    [TestClass]
    public class ImageGalleryViewModelTest
    {
        private ImageGallaryEditorViewModel _viewModel;
        private Mock<IImageGalleryViewProvider> _dataProvider;
        private Mock<IDialogService> _dialogService;
        private Mock<IWindowController> _windowController;

        [TestInitialize]
        public void SetUp()
        {
            _dataProvider = new Mock<IImageGalleryViewProvider>();
            _dialogService = new Mock<IDialogService>();
            _windowController = new Mock<IWindowController>();
            _viewModel = new ImageGallaryEditorViewModel(_dataProvider.Object, _dialogService.Object, new ImageItemViewModelFactory(Mock.Of<IFormatter<DateTime>>()), Mock.Of<INavigationService>());
        }

        [TestMethod]
        public void Load_LoadThumbs()
        {
            var id = Guid.NewGuid();
            _dataProvider.Setup(x => x.GetCollectionItem(id)).Returns(new CollectionItemDto { Id = id });
            _dataProvider.Setup(x => x.GetThumbnailsOf(id, It.IsAny<Action<ImageDto>>())).Callback((Guid imageId, Action<ImageDto> callback) =>
            {
                callback(new ImageDto { Id = imageId });
                callback(new ImageDto { Id = imageId });
            });

            _viewModel.Load(id);
            Thread.Sleep(10);

            Assert.AreEqual(2, _viewModel.Images.Count);
        }
    }
}
