using CactusGuru.Application.ViewProviders.ImageGallery;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Presentation.ViewModel.Framework;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery
{
    public class ImageItemViewModel : BaseViewModel
    {
        public ImageItemViewModel(ImageDto dto, IFormatter<DateTime> dateFormatter)
        {
            InnerObject = dto;
            _dateFormatter = dateFormatter;
            _memento = new ImageItemMemento(dto);
        }

        private readonly IFormatter<DateTime> _dateFormatter;
        private readonly ImageItemMemento _memento;

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
            set { InnerObject.Thumbnail = value; }
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
            set
            {
                InnerObject.DateAdded = value;
                OnPropertyChanged(nameof(DateAddedTitle));
            }
        }

        public string DateAddedTitle => _dateFormatter.Format(InnerObject.DateAdded);

        private bool _isDeleted;

        public bool IsSelected
        {
            get { return _isDeleted; }
            set
            {
                _isDeleted = value;
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
    }
}