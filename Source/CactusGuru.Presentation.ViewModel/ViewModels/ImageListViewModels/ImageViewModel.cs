using CactusGuru.Application.ViewProviders.ImageList;
using CactusGuru.Presentation.ViewModel.Framework;
using CactusGuru.Presentation.ViewModel.Utils;
using System;

namespace CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels
{
    public class ImageViewModel : BaseViewModel
    {
        public ImageViewModel(ImageDto imageDto)
        {
            InnerObject = imageDto;
            _dateFormatter = new MonthNameDateFormatter();
        }

        private readonly MonthNameDateFormatter _dateFormatter;

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
