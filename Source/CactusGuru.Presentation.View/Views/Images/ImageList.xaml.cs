using CactusGuru.Presentation.ViewModel.ViewModels.ImageListViewModels;
using System.Windows;

namespace CactusGuru.Presentation.View.Views
{
    public partial class ImageList  
    {
        public ImageList()
        {
            Resources.MergedDictionaries.Add(ResourceLocator.General);
            Resources.MergedDictionaries.Add(ResourceLocator.Lists);
            InitializeComponent();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel().Select();
        }

        private ImageListViewModel ViewModel()
        {
            return (ImageListViewModel)DataContext;
        }
    }
}
