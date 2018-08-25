namespace CactusGuru.Presentation.ViewModel.Framework
{
    public interface INavigationViewModel
    {
        bool IsFormBusy { get; }
        void Load();
    }
}
