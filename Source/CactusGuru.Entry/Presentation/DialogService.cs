using CactusGuru.Presentation.ViewModel.Services.Navigations;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace CactusGuru.Entry.Presentation
{
    public class DialogService : IDialogService
    {
        private const string APP_TITLE = "Cactus Guru";

        public void Say(string message)
        {
            System.Windows.MessageBox.Show(message, APP_TITLE, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        public bool Ask(string message)
        {
            var result = System.Windows.MessageBox.Show(message,
                                           APP_TITLE,
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question,
                                           MessageBoxResult.No);
            return result == MessageBoxResult.Yes;
        }

        public void Error(string error)
        {
            System.Windows.MessageBox.Show(error,
                              APP_TITLE,
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
        }

        public DialogResult<IEnumerable<string>> OpenImageFileDialog()
        {
            var dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            dialog.Filter = @"JPEF Files|*.jpg";
            if (dialog.ShowDialog() != DialogResult.OK)
                return new DialogResult<IEnumerable<string>>(false, Enumerable.Empty<string>());
            return new DialogResult<IEnumerable<string>>(true, dialog.FileNames);
        }

        public DialogResult<string> OpenDirectorySelector()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
                return new DialogResult<string>(true, dialog.SelectedPath);
            return new DialogResult<string>(false, string.Empty);
        }
    }
}
