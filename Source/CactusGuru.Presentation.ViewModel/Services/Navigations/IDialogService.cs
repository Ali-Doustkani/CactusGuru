using System.Collections.Generic;

namespace CactusGuru.Presentation.ViewModel.Services.Navigations
{
    public interface IDialogService
    {
        void Say(string message);
        bool Ask(string message);
        void Error(string error);
        DialogResult<IEnumerable<string>> OpenImageFileDialog();
        DialogResult<string> OpenDirectorySelector();
    }
}
