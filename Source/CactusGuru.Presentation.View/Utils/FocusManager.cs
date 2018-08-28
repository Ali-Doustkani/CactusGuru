using System.Windows;

namespace CactusGuru.Presentation.View.Utils
{
    public static class FocusManager
    {
        private static readonly DependencyProperty IsEventSetProperty = DependencyProperty.RegisterAttached("IsEventSet",
            typeof(bool),
            typeof(FocusManager),
            new PropertyMetadata(false));

        public static readonly DependencyProperty HasFocusProperty = DependencyProperty.RegisterAttached("HasFocus",
            typeof(bool),
            typeof(FocusManager),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, HasFocusChanged));

        public static bool GetHasFocus(UIElement obj)
        {
            return (bool)obj.GetValue(HasFocusProperty);
        }

        public static void SetHasFocus(UIElement obj, bool value)
        {
            obj.SetValue(HasFocusProperty, value);
        }

        private static void HasFocusChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            SetEventIfItsNotSet(obj);
            if ((bool)e.NewValue)
                ((UIElement)obj).Focus();
        }

        private static void SetEventIfItsNotSet(DependencyObject obj)
        {
            if ((bool)obj.GetValue(IsEventSetProperty))
                return;
            System.Windows.Input.FocusManager.AddLostFocusHandler(obj, (sender, e) =>
            {
                ((DependencyObject)sender).SetValue(HasFocusProperty, false);
            });
            obj.SetValue(IsEventSetProperty, true);
        }
    }
}
