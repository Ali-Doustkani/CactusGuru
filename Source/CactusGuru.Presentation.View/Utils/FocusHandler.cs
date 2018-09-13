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

        public static readonly DependencyProperty TraverseOnEnterProperty = DependencyProperty.RegisterAttached("TraverseOnEnter",
            typeof(bool),
            typeof(FocusHandler),
            new PropertyMetadata(TraverseOnEnterChanged));

        public static readonly DependencyProperty AutoOpenDropDownProperty = DependencyProperty.RegisterAttached("AutoOpenDropDown",
            typeof(bool),
            typeof(FocusHandler),
            new PropertyMetadata(AutoOpenDropDownChanged));

        public static bool GetHasFocus(UIElement obj)
        {
            return (bool)obj.GetValue(HasFocusProperty);
        }

        public static void SetHasFocus(UIElement obj, bool value)
        {
            obj.SetValue(HasFocusProperty, value);
        }

        public static bool GetTraverseOnEnter(UIElement obj)
        {
            return (bool)obj.GetValue(TraverseOnEnterProperty);
        }

        public static void SetTraverseOnEnter(UIElement obj, bool value)
        {
            obj.SetValue(TraverseOnEnterProperty, value);
        }

        public static bool GetAutoOpenDropDown(UIElement obj)
        {
            return (bool)obj.GetValue(AutoOpenDropDownProperty);
        }

        public static void SetAutoOpenDropDown(UIElement obj, bool value)
        {
            obj.SetValue(AutoOpenDropDownProperty, value);
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
            FocusManager.AddLostFocusHandler(obj, (sender, e) =>
            {
                ((DependencyObject)sender).SetValue(HasFocusProperty, false);
            });
            obj.SetValue(IsEventSetProperty, true);
        }

        private static void TraverseOnEnterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
                AddControl((FrameworkElement)obj);
            else
                RemoveControl((FrameworkElement)obj);
        }

        private static void AddControl(FrameworkElement ctrl)
        {
            if (ctrl is ComboBox)
                (ctrl as ComboBox).DropDownClosed += DropDownClosed;
            ctrl.PreviewKeyDown += ctrl_PreviewKeyDown;
            ctrl.Unloaded += Ctrl_Unloaded;
        }

        private static void RemoveControl(FrameworkElement element)
        {
            if (element is ComboBox)
                (element as ComboBox).DropDownClosed -= DropDownClosed;
            element.PreviewKeyDown -= ctrl_PreviewKeyDown;
            element.Unloaded -= Ctrl_Unloaded;
        }

        private static void DropDownClosed(object sender, System.EventArgs e)
        {
            var combo = (ComboBox)sender;
            if (combo.SelectedItem != null)
                GotoNextControl(sender);
        }

        private static void Ctrl_Unloaded(object sender, RoutedEventArgs e)
        {
            RemoveControl((FrameworkElement)sender);
        }

        private static void ctrl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                GotoNextControl(sender);
        }

        private static void GotoNextControl(object ctrl)
        {
            var form = Window.GetWindow((DependencyObject)ctrl);
            var element = FocusManager.GetFocusedElement(form) as FrameworkElement;
            if (element != null)
                element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        private static void AutoOpenDropDownChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false)) return;
            var combo = obj as ComboBox;
            if (combo == null) return;
            combo.GotFocus += Combo_GotFocus;
            combo.Unloaded += Combo_Unloaded;
        }

        private static void Combo_GotFocus(object sender, RoutedEventArgs e)
        {
            ((ComboBox)sender).IsDropDownOpen = true;
        }

        private static void Combo_Unloaded(object sender, RoutedEventArgs e)
        {
            var combo = (ComboBox)sender;
            combo.GotFocus -= Combo_GotFocus;
            combo.Unloaded -= Combo_Unloaded;
        }
    }
}