using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.NavigationService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using CactusGuru.Presentation.ViewModel.Utils;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageGallaryEditorViewModel : BaseViewModel
    {
        public ImageGallaryEditorViewModel(IImageGalleryViewProvider dataProvider,
            IDialogService dialogService,
            ImageItemViewModelFactory imageItemFactory, INavigationService navigationService)
        {
            _dialogService = dialogService;
            _imageItemFactory = imageItemFactory;
            _navigationService = navigationService;
            _dataProvider = dataProvider;
            _imageLoaderWorker = new BackgroundWorker();
            _imageLoaderWorker.WorkerReportsProgress = true;
            _imageLoaderWorker.DoWork += _backgroundWorker_DoWork;
            _imageLoaderWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;
            _imageLoaderWorker.ProgressChanged += _backgroundWorker_ProgressChanged;

            _imageAdderWorker = new BackgroundWorker();
            _imageAdderWorker.WorkerReportsProgress = true;
            _imageAdderWorker.DoWork += _imageAdderWorker_DoWork;
            _imageAdderWorker.ProgressChanged += _imageAdderWorker_ProgressChanged;
            _imageAdderWorker.RunWorkerCompleted += _imageAdderWorker_RunWorkerCompleted;

            _imageSaveWorker = new BackgroundWorker();
            _imageSaveWorker.DoWork += _imageSaveWorker_DoWork;
            _imageSaveWorker.RunWorkerCompleted += _imageSaveWorker_RunWorkerCompleted1;

            SaveCommand = new RelayCommand(Save, CanUndo);
            CancelCommand = new RelayCommand(_navigationService.CloseCurrentView, () => !IsFormBusy);
            AddImageCommand = new RelayCommand(AddImage, () => !IsFormBusy);
            DeleteImageCommand = new RelayCommand(DeleteSelectedImages, () => IsAnythingSelected);
            UndoCommand = new RelayCommand(Undo, CanUndo);
            SelectAllCommand = new RelayCommand(SelectAll);
            DeSelectAllCommand = new RelayCommand(DeSelectAll);
            SaveToFilesCommand = new RelayCommand(() => SaveToFiles("imageFile"), () => IsAnythingSelected);
            SaveForInstagramCommand = new RelayCommand(() => SaveToFiles("zipFile"), () => IsAnythingSelected);
            Images = new ObservableCollection<ImageItemViewModel>();
        }

        private readonly IImageGalleryViewProvider _dataProvider;
        private readonly IDialogService _dialogService;
        private readonly INavigationService _navigationService;
        private readonly ImageItemViewModelFactory _imageItemFactory;
        private readonly BackgroundWorker _imageLoaderWorker;
        private readonly BackgroundWorker _imageAdderWorker;
        private readonly BackgroundWorker _imageSaveWorker;
        private Guid _collectionItemId;
        private bool _isFormBusy;
        private GalleryMemento _memento;
        private string _code;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand DeleteImageCommand { get; }
        public ICommand SelectAllCommand { get; }
        public ICommand DeSelectAllCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand SaveToFilesCommand { get; }
        public ICommand SaveForInstagramCommand { get; }
        public ObservableCollection<ImageItemViewModel> Images { get; private set; }
        public ImageItemViewModel SelectedImage { get; set; }

        public string Code
        {
            get { return _code; }
            set
            {
                if (value == _code) return;
                if (!_dataProvider.CollectionItemCodeExists(value)) return;
                LoadByCode(value);
                _code = value;
            }
        }

        public string Title { get; private set; }
        public string Locality { get; private set; }

        public bool IsFormBusy
        {
            get { return _isFormBusy; }
            set
            {
                _isFormBusy = value;
                OnPropertyChanged(nameof(IsFormBusy));
            }
        }

        public bool IsAnythingSelected
        {
            get { return Images.Any(x => x.IsSelected); }
        }

        public void SelectImage()
        {
            if (SelectedImage == null) return;
            SelectedImage.IsSelected = !SelectedImage.IsSelected;
        }

        private void DeleteSelectedImages()
        {
            Images.RemoveAll(x => x.IsSelected);
        }

        private void SelectAll()
        {
            foreach (var image in Images)
                image.IsSelected = true;
        }

        private void DeSelectAll()
        {
            foreach (var image in Images)
                image.IsSelected = false;
        }

        private void SaveToFiles(string param)
        {
            var dialogResult = _dialogService.OpenDirectorySelector();
            if (!dialogResult.Result) return;
            var selectedImages = Images.Where(x => x.IsSelected).Select(x => x.InnerObject);
            if (param == "imageFile")
                _dataProvider.SaveToFiles(selectedImages, dialogResult.Value);
            else if (param == "zipFile")
                _dataProvider.SaveToZip(selectedImages, dialogResult.Value);
            DeSelectAll();
            _dialogService.Say("ذخیره ی تصاویر با موفقیت انجام شد.");
        }

        #region UNDO

        private void Undo()
        {
            UndoImages();
            UndoImageContents();
        }

        private void UndoImages()
        {
            Images.Clear();
            Images.AddRange(_memento.Images);
        }

        private void UndoImageContents()
        {
            foreach (var image in Images)
                image.Undo();
        }

        private bool CanUndo()
        {
            var anyImagesAdded = (_memento != null) && _memento.AnyDifference(Images);
            var imageContentsChanged = Images.Any(x => x.IsDirty);
            return anyImagesAdded || imageContentsChanged;
        }

        #endregion

        #region LOADING

        private void LoadByCode(string code)
        {
            Load(_dataProvider.GetCollectionItemIdByCode(code));
        }

        public void Load(Guid collectionItemId)
        {
            IsFormBusy = true;
            Images.Clear();
            _collectionItemId = collectionItemId;
            var collectionItem = _dataProvider.GetCollectionItem(collectionItemId);
            _code = collectionItem.Code;
            Title = collectionItem.Title;
            Locality = collectionItem.Locality;
            _imageLoaderWorker.RunWorkerAsync();
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _dataProvider.GetThumbnailsOf(_collectionItemId,
                dto =>
                {
                    var vm = _imageItemFactory.Create(dto);
                    _imageLoaderWorker.ReportProgress(0, vm);
                });
        }

        private void _backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var vm = (ImageItemViewModel)e.UserState;
            Images.Add(vm);
        }

        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _memento = new GalleryMemento(Images);
            IsFormBusy = false;
        }

        #endregion

        #region ADD IMAGE

        private void AddImage()
        {
            var dialogResult = _dialogService.OpenImageFileDialog();
            if (!dialogResult.Result) return;
            IsFormBusy = true;
            _imageAdderWorker.RunWorkerAsync(dialogResult.Value);
        }

        private void _imageAdderWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var files = (IEnumerable<string>)e.Argument;
            foreach (var path in files)
            {
                var dto = _dataProvider.Build(path, _collectionItemId);
                dto.ImagePath = path;
                _imageAdderWorker.ReportProgress(0, dto);
            }
        }

        private void _imageAdderWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var dto = (ImageDto)e.UserState;
            var vm = _imageItemFactory.Create(dto);
            Images.Add(vm);
        }

        private void _imageAdderWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            System.Threading.Thread.Sleep(500);
            IsFormBusy = false;
        }

        #endregion

        #region SAVE IMAGE

        private void Save()
        {
            IsFormBusy = true;
            _imageSaveWorker.RunWorkerAsync();
        }

        private void _imageSaveWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var dto = new ImageGalleryDto();
            dto.CollectionItemId = _collectionItemId;
            foreach (var vm in Images.Where(x => !x.IsSelected))
                dto.Images.Add(vm.InnerObject);
            _dataProvider.SaveImageGallery(dto);
        }

        private void _imageSaveWorker_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            IsFormBusy = false;
            _navigationService.CloseCurrentView();
        }

        #endregion

        public void ChangeDate()
        {
            var result = _navigationService.GetDateFromUser();
            if (result.Result)
            {
                SelectedImage.DateAdded = result.Value;
            }
        }
    }
}