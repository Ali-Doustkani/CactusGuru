using CactusGuru.Presentation.ViewModel.ViewModels.ImageGallery;
using System.Windows;

namespace CactusGuru.Presentation.View.Views
{
    public partial class ImageGallary
    {
        public ImageGallary()
        {
            InitializeComponent();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel().SelectImage();
        }

        private void ChangeDateButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel().ChangeDate();
            grid.RefreshData();
        }

        private ImageGallaryEditorViewModel ViewModel()
        {
            return (ImageGallaryEditorViewModel)DataContext;
        }
    }
}
