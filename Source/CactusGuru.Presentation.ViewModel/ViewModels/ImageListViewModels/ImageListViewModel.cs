using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels
{
    public class ImageListViewModel : BaseViewModel, INavigationViewModel
    {
        public ImageListViewModel(IImageListViewProvider viewProvider,
            ImageViewModelFactory imageViewModelFactory,
            IDialogService dialogService)
        {
            _viewProvider = viewProvider;
            _imageViewModelFactory = imageViewModelFactory;
            _dialogService = dialogService;
            _bgWorker = new BackgroundWorker();
            _bgWorker.WorkerReportsProgress = true;
            _bgWorker.DoWork += _bgWorker_DoWork;
            _bgWorker.ProgressChanged += _bgWorker_ProgressChanged;
            _bgWorker.RunWorkerCompleted += _bgWorker_RunWorkerCompleted;
            DeSelectAllCommand = new RelayCommand(DeSelectAll);
            SaveForInstagramCommand = new RelayCommand(SaveForInstagram, CanSave);
            Images = new ObservableCollection<ImageViewModel>();
            IsFormBusy = true;
        }

        private readonly IImageListViewProvider _viewProvider;
        private readonly ImageViewModelFactory _imageViewModelFactory;
        private readonly IDialogService _dialogService;
        private readonly BackgroundWorker _bgWorker;

        public ICommand DeSelectAllCommand { get; }
        public ICommand SaveForInstagramCommand { get; }
        public ObservableCollection<ImageViewModel> Images { get; private set; }

        public bool IsFormBusy { get; private set; }

        public ImageViewModel SelectedImage { get; set; }

        public void Load()
        {
            _bgWorker.RunWorkerAsync();
        }

        private void _bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (_viewProvider.GetImagesAsync(LoadImages)) { };
        }

        private void LoadImages(IEnumerable<ImageDto> dtos)
        {
            var images = new List<ImageViewModel>();
            foreach (var dto in dtos)
                images.Add(_imageViewModelFactory.Create(dto));
            _bgWorker.ReportProgress(0, images);
        }

        private void _bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ((List<ImageViewModel>)e.UserState).ForEach(Images.Add);
        }

        private void _bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsFormBusy = false;
            OnPropertyChanged(nameof(IsFormBusy));
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
            var dialogResult = _dialogService.OpenDirectorySelector();
            if (!dialogResult.Result) return;
            var selectedImages = Images.Where(x => x.IsSelected).Select(x => x.InnerObject);
            _viewProvider.SaveToFiles(selectedImages, dialogResult.Value);
            DeSelectAll();
            _dialogService.Say("ذخیره با موفقیت انجام شد.");
        }
    }
}
