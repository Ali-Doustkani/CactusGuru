using System;

namespace CactusGuru.Presentation.View.Views
{
    public interface IUserControlView
    {
        event EventHandler Save;

        object DataContext { get; set; }
    }
}
