using System.Windows;
using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Tools.Controls
{
    public class Page : ContentControl
    {
        static Page()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Page), new FrameworkPropertyMetadata(typeof(Page)));
        }
    }
}