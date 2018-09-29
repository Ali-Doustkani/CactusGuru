using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels
{
    public class ImageListViewModel : FormViewModel
    {
        public ImageListViewModel(IImageListViewProvider viewProvider)
        {
            _viewProvider = viewProvider;
            DeSelectAllCommand = new RelayCommand(DeSelectAll);
            SaveToFileCommand = new RelayCommand(SaveToFile, AnySelected);
            SaveForInstagramCommand = new RelayCommand(SaveForInstagram, AnySelected);
            DeleteCommand = new RelayCommand(Delete, AnySelected);
            Images = new ObservableCollection<ImageViewModel>();
        }

        private readonly IImageListViewProvider _viewProvider;

        public ICommand DeSelectAllCommand { get; }
        public ICommand SaveToFileCommand { get; }
        public ICommand SaveForInstagramCommand { get; }
        public ICommand DeleteCommand { get; }
        public ObservableCollection<ImageViewModel> Images { get; }

        protected async override void OnLoad()
        {
            var progress = new Progress<ImageDto>(dto =>
            {
                if (LoaderState.IsBusy)
                    LoaderState.ToIdle();
                Images.Add(new ImageViewModel(dto));
            });
            await _viewProvider.GetImagesAsync(progress);
        }

        private void DeSelectAll()
        {
            foreach (var image in Images)
                image.IsSelected = false;
        }

        private bool AnySelected()
        {
            return Images.Any(x => x.IsSelected) && LoaderState.IsIdle;
        }

        private async void Delete()
        {
            if (!Dialog.AskForDelete()) return;
            LoaderState.ToBusy();
            var selectedImages = Images.Where(x => x.IsSelected);
            await _viewProvider.Delete(selectedImages.Select(x => x.InnerObject));
            Images.RemoveAll(selectedImages.ToArray());
            LoaderState.ToIdle();
        }

        private async void SaveForInstagram()
        {
            var dialogResult = Dialog.OpenDirectorySelector();
            if (!dialogResult.Result) return;
            LoaderState.ToBusy();
            var selectedImages = Images.Where(x => x.IsSelected).Select(x => x.InnerObject);
            await _viewProvider.SaveForInstagram(selectedImages, dialogResult.Value);
            DeSelectAll();
            LoaderState.ToIdle();
            Dialog.Say("Saving for Instagram completed successfully!");
        }

        private async void SaveToFile()
        {
            var dialogResult = Dialog.OpenDirectorySelector();
            if (!dialogResult.Result) return;
            LoaderState.ToBusy();
            var selectedImages = Images.Where(x => x.IsSelected).Select(x => x.InnerObject);
            await _viewProvider.SaveToFile(selectedImages, dialogResult.Value);
            DeSelectAll();
            LoaderState.ToIdle();
            Dialog.Say("Saving images completed successfully!");
        }
    }
}