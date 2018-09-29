using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Framework.Collections;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageGallaryEditorViewModel : FormViewModel
    {
        public ImageGallaryEditorViewModel(IImageGalleryViewProvider dataProvider)
        {
            _dataProvider = dataProvider;
            SaveCommand = new RelayCommand(Save, CanUndo);
            CancelCommand = new RelayCommand(() => Navigations.CloseCurrentView(), () => LoaderState.IsIdle);
            AddImageCommand = new RelayCommand(AddImage, () => LoaderState.IsIdle);
            DeleteImageCommand = new RelayCommand(DeleteSelectedImages, IsAnythingSelected);
            UndoCommand = new RelayCommand(Undo, CanUndo);
            SelectAllCommand = new RelayCommand(SelectAll);
            DeSelectAllCommand = new RelayCommand(DeSelectAll);
            SaveToFilesCommand = new RelayCommand(() => SaveToFiles("imageFile"), IsAnythingSelected);
            SaveForInstagramCommand = new RelayCommand(() => SaveToFiles("zipFile"), IsAnythingSelected);

            _progress = new Progress<ImageDto>(dto =>
           {
               var vm = new ImageItemViewModel(dto, Navigations);
               Images.Add(vm);
           });

            Images = new ObservableCollection<ImageItemViewModel>();
        }

        private readonly IImageGalleryViewProvider _dataProvider;
        private readonly IProgress<ImageDto> _progress;
        private Guid _collectionItemId;
        private GalleryMemento _memento;

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand AddImageCommand { get; }
        public ICommand DeleteImageCommand { get; }
        public ICommand SelectAllCommand { get; }
        public ICommand DeSelectAllCommand { get; }
        public ICommand UndoCommand { get; }
        public ICommand SaveToFilesCommand { get; }
        public ICommand SaveForInstagramCommand { get; }
        public ObservableCollection<ImageItemViewModel> Images { get; }
        public string Code { get; private set; }
        public string Title { get; private set; }
        public string Locality { get; private set; }

        public void Load(Guid collectionItemId) => _collectionItemId = collectionItemId;

        protected async override void OnLoad()
        {
            var collectionItem = _dataProvider.GetCollectionItem(_collectionItemId);
            Code = collectionItem.Code;
            Title = collectionItem.Title;
            Locality = collectionItem.Locality;
            await _dataProvider.GetThumbnailsOfAsync(_collectionItemId, _progress);
            _memento = new GalleryMemento(Images);
            OnPropertyChanged(nameof(Code));
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Locality));
            LoaderState.ToIdle();
        }

        private void LoadByCode(string code) => Load(_dataProvider.GetCollectionItemIdByCode(code));

        private bool IsAnythingSelected()
        {
            return Images.Any(x => x.IsSelected);
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
            var dialogResult = Dialog.OpenDirectorySelector();
            if (!dialogResult.Result) return;
            var selectedImages = Images.Where(x => x.IsSelected).Select(x => x.InnerObject);
            if (param == "imageFile")
                _dataProvider.SaveToFiles(selectedImages, dialogResult.Value);
            else if (param == "zipFile")
                _dataProvider.SaveToZip(selectedImages, dialogResult.Value);
            DeSelectAll();
            Dialog.Say("Saving images completed successfully!");
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