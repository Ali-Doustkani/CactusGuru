using System.Collections.Generic;

namespace CactusGuru.Presentation.ViewModel.Services.Navigations
{
    public interface IDialogService
    {
        void Say(string message);
        bool Ask(string message);
        bool AskForDelete();
        void Error(string error);
        DialogResult<IEnumerable<string>> OpenImageFileDialog();
        DialogResult<string> OpenDirectorySelector();
    }
}
