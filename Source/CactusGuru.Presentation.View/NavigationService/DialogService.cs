using CactusGuru.Presentation.ViewModel.NavigationService;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace CactusGuru.Presentation.View.NavigationService
{
    public class DialogService : IDialogService
    {
        public void Say(string message)
        {
            System.Windows.MessageBox.Show(message, "کاکتوس گورو", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        public bool Ask(string message)
        {
            var result = System.Windows.MessageBox.Show(message,
                                           "کاکتوس گورو",
                                           MessageBoxButton.YesNo,
                                           MessageBoxImage.Question,
                                           MessageBoxResult.No);
            return result == MessageBoxResult.Yes;
        }

        public void Error(string error)
        {
            System.Windows.MessageBox.Show(error,
                              "کاکتوس گورو",
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
