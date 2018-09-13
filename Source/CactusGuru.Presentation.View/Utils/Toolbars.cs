using System.Windows;
using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Utils
{
    public static class Toolbars
    {
        public static readonly DependencyProperty HideGripProperty = DependencyProperty.RegisterAttached("HideGrip",
            typeof(bool),
            typeof(Toolbars),
            new PropertyMetadata(HideGripChanged));

        public static bool GetHideGrip(DependencyObject obj)
        {
            return (bool)obj.GetValue(HideGripProperty);
        }

        public static void SetHideGrip(DependencyObject obj, bool value)
        {
            obj.SetValue(HideGripProperty, value);
        }

        private static void HideGripChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false)) return;
            var toolbar = (ToolBar)d;
            toolbar.Loaded += Loaded;
            toolbar.Unloaded += Unloaded;
        }

        private static void Loaded(object sender, RoutedEventArgs e)
        {
            var toolBar = (ToolBar)sender;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
                overflowGrid.Visibility = Visibility.Collapsed;
            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
                mainPanelBorder.Margin = new Thickness();
        }

        private static void Unloaded(object sender, RoutedEventArgs e)
        {
            var toolBar = (ToolBar)sender;
            toolBar.Loaded -= Loaded;
            toolBar.Unloaded -= Unloaded;
        }
    }
}
