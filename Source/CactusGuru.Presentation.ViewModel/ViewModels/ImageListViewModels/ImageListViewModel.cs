using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Presentation.ViewModel.Framework;
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
            SaveForInstagramCommand = new RelayCommand(SaveForInstagram, CanSave);
            Images = new ObservableCollection<ImageViewModel>();
        }

        private readonly IImageListViewProvider _viewProvider;

        public ICommand DeSelectAllCommand { get; }
        public ICommand SaveForInstagramCommand { get; }
        public ObservableCollection<ImageViewModel> Images { get; }
        public ImageViewModel SelectedImage { get; set; }

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

        public void Select()
        {
            if (SelectedImage == null) return;
            SelectedImage.IsSelected = !SelectedImage.IsSelected;
        }

        private void DeSelectAll()
        {
            foreach (var image in Images)
                image.IsSelected = false;
        }

        private bool CanSave()
        {
            return Images.Any(x => x.IsSelected);
        }

        private void SaveForInstagram()
        {
            var dialogResult = Dialog.OpenDirectorySelector();
            if (!dialogResult.Result) return;
            var selectedImages = Images.Where(x => x.IsSelected).Select(x => x.InnerObject);
            _viewProvider.SaveToFiles(selectedImages, dialogResult.Value);
            DeSelectAll();
            Dialog.Say("ذخیره با موفقیت انجام شد.");
        }
    }
}