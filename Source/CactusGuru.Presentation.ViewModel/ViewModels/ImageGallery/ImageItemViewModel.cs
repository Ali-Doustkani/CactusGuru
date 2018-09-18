using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Services.Navigations;
using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageItemViewModel : BaseViewModel
    {
        public ImageItemViewModel(ImageDto dto, INavigationService navigations)
        {
            InnerObject = dto;
            _navigations = navigations;
            _memento = new ImageItemMemento(dto);
            ChangeDateCommand = new RelayCommand(ChangeDate);
        }

        private readonly ImageItemMemento _memento;
        private readonly INavigationService _navigations;

        public ICommand ChangeDateCommand { get; }

        internal ImageDto InnerObject { get; }

        public string Description
        {
            get { return InnerObject.Description; }
            set
            {
                InnerObject.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public byte[] Picture
        {
            get { return InnerObject.Thumbnail; }
        }

        public bool SharedOnInstagram
        {
            get { return InnerObject.SharedOnInstagram; }
            set
            {
                InnerObject.SharedOnInstagram = value;
                OnPropertyChanged(nameof(SharedOnInstagram));
            }
        }

        public DateTime DateAdded
        {
            get { return InnerObject.DateAdded; }
            private set
            {
                InnerObject.DateAdded = value;
                OnPropertyChanged(nameof(DateAdded));
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public bool IsDirty => _memento.IsDirty(InnerObject);

        public void Undo()
        {
            Description = _memento.Description;
            IsSelected = _memento.IsSelected;
            DateAdded = _memento.DateAdded;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ImageItemViewModel;
            if (other == null) return false;
            return other.InnerObject.Id.Equals(InnerObject.Id);
        }

        public override int GetHashCode()
        {
            return InnerObject.Id.GetHashCode();
        }

        private void ChangeDate()
        {
            var result = _navigations.GetDateFromUser(DateAdded);
            if (result.Result)
                DateAdded = result.Value;
        }
    }
}