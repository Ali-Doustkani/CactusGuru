using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Tools;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageGallaryEditorViewModel : FormViewModel
    {
        public ImageGallaryEditorViewModel(IImageGalleryViewProvider dataProvider, ImageItemViewModelFactory imageItemFactory)
        {
            _imageItemFactory = imageItemFactory;
            _dataProvider = dataProvider;
            SaveCommand = new RelayCommand(Save, CanUndo);
            CancelCommand = new RelayCommand(() => Navigations.CloseCurrentView(), () => LoaderState.IsIdle);
            AddImageCommand = new RelayCommand(AddImage, () => LoaderState.IsIdle);
            DeleteImageCommand = new RelayCommand(DeleteSelectedImages, () => IsAnythingSelected);
            UndoCommand = new RelayCommand(Undo, CanUndo);
            SelectAllCommand = new RelayCommand(SelectAll);
            DeSelectAllCommand = new RelayCommand(DeSelectAll);
            SaveToFilesCommand = new RelayCommand(() => SaveToFiles("imageFile"), () => IsAnythingSelected);
            SaveForInstagramCommand = new RelayCommand(() => SaveToFiles("zipFile"), () => IsAnythingSelected);

            _progress = new Progress<ImageDto>(dto =>
           {
               var vm = _imageItemFactory.Create(dto);
               Images.Add(vm);
           });

            Images = new ObservableCollection<ImageItemViewModel>();
        }

        private readonly IImageGalleryViewProvider _dataProvider;
        private readonly ImageItemViewModelFactory _imageItemFactory;
        private readonly IProgress<ImageDto> _progress;
        private Guid _collectionItemId;

        private GalleryMemento _memento;
        private string _code;
        private string _title;
        private string _locality;

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }
        public ICommand AddImageCommand { get; private set; }
        public ICommand DeleteImageCommand { get; private set; }
        public ICommand SelectAllCommand { get; private set; }
        public ICommand DeSelectAllCommand { get; private set; }
        public ICommand UndoCommand { get; private set; }
        public ICommand SaveToFilesCommand { get; private set; }
        public ICommand SaveForInstagramCommand { get; private set; }
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
                OnPropertyChanged(nameof(Code));
            }
        }

        public string Title
        {
            get { return _title; }
            private set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Locality
        {
            get { return _locality; }
            private set
            {
                _locality = value;
                OnPropertyChanged(nameof(Locality));
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

        public void ChangeDate()
        {
            var result = Navigations.GetDateFromUser();
            if (result.Result)
            {
                SelectedImage.DateAdded = result.Value;
            }
        }

        protected async override void OnLoad()
        {
            Images.Clear();
            var collectionItem = _dataProvider.GetCollectionItem(_collectionItemId);
            _code = collectionItem.Code;
            OnPropertyChanged(nameof(Code));
            Title = collectionItem.Title;
            Locality = collectionItem.Locality;
            await _dataProvider.GetThumbnailsOfAsync(_collectionItemId, _progress);
            _memento = new GalleryMemento(Images);
            LoaderState.ToIdle();
        }

        private void LoadByCode(string code) => Load(_dataProvider.GetCollectionItemIdByCode(code));

        public void Load(Guid collectionItemId) => _collectionItemId = collectionItemId;

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
            var dialogResult = Dialog.OpenDirectorySelector();
            if (!dialogResult.Result) return;
            var selectedImages = Images.Where(x => x.IsSelected).Select(x => x.InnerObject);
            if (param == "imageFile")
                _dataProvider.SaveToFiles(selectedImages, dialogResult.Value);
            else if (param == "zipFile")
                _dataProvider.SaveToZip(selectedImages, dialogResult.Value);
            DeSelectAll();
            Dialog.Say("ذخیره ی تصاویر با موفقیت انجام شد.");
        }

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

        private async void AddImage()
        {
            var dialogResult = Dialog.OpenImageFileDialog();
            if (!dialogResult.Result) return;
            LoaderState.ToBusy();
            await _dataProvider.BuildAsync(dialogResult.Value, _collectionItemId, _progress);
            LoaderState.ToIdle();
        }

        private async void Save()
        {
            LoaderState.ToBusy();
            var dto = new ImageGalleryDto
            {
                CollectionItemId = _collectionItemId,
                Images = Images.Where(x => !x.IsSelected).Select(x => x.InnerObject).ToList()
            };
            await _dataProvider.SaveImageGalleryAsync(dto);
            LoaderState.ToIdle();
            Navigations.CloseCurrentView();
        }
    }
}