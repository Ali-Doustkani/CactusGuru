using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Utils
{
    public static class FocusHandler
    {
        private static readonly DependencyProperty IsEventSetProperty = DependencyProperty.RegisterAttached("IsEventSet",
            typeof(bool),
            typeof(FocusHandler),
            new PropertyMetadata(false));

        public static readonly DependencyProperty HasFocusProperty = DependencyProperty.RegisterAttached("HasFocus",
            typeof(bool),
            typeof(FocusHandler),
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

        public static void GotoNextControl(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;
            var keyboardFocus = sender as UIElement;
            if (keyboardFocus != null)
            {
                var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                keyboardFocus.MoveFocus(tRequest);
            }
        }

        public static void NavigateListboxItems(ListBox listBox, KeyEventArgs e)
        {
            if (e.Key == Key.Down && listBox.SelectedIndex < listBox.Items.Count)
                listBox.SelectedIndex++;
            else if (e.Key == Key.Up && listBox.SelectedIndex > 0)
                listBox.SelectedIndex--;
        }
    }
}
