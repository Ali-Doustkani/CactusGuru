using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Domain.Greenhouse.Formatting;
using CactusGuru.Presentation.ViewModel.Framework;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        public ImageViewModel(ImageDto imageDto, IFormatter<DateTime> dateFormatter)
        {
            InnerObject = imageDto;
            _dateFormatter = dateFormatter;
        }

        private readonly IFormatter<DateTime> _dateFormatter;

        internal ImageDto InnerObject { get; }

        public string Title
        {
            get { return InnerObject.Title; }
            set { }
        }

        public byte[] Picture
        {
            get { return InnerObject.Thumbnail; }
            set { }
        }

        public DateTime DateAdded
        {
            get { return InnerObject.DateAdded; }
        }

        public string DateAddedTitle => _dateFormatter.Format(DateAdded);

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

       
    }
}
