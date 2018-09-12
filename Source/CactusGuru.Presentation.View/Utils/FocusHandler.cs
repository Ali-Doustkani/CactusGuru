using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.LookUp;
using System.Windows;
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

        public static void AddControl(FrameworkElement ctrl)
        {
            if (ctrl is LookUpEdit)
                (ctrl as LookUpEdit).PopupClosed += TabIndexController_PopupClosed;
            ctrl.PreviewKeyUp += ctrl_PreviewKeyUp;
            ctrl.Unloaded += Ctrl_Unloaded;
        }

        public static void RemoveControl(FrameworkElement element)
        {
            if (element is LookUpEdit)
                (element as LookUpEdit).PopupClosed -= TabIndexController_PopupClosed;
            element.PreviewKeyUp -= ctrl_PreviewKeyUp;
            element.Unloaded -= Ctrl_Unloaded;
        }

        private static void Ctrl_Unloaded(object sender, RoutedEventArgs e)
        {
            RemoveControl((FrameworkElement)sender);
        }

        private static void TabIndexController_PopupClosed(object sender, ClosePopupEventArgs e)
        {
            if (e.CloseMode == PopupCloseMode.Normal)
                GotoNextControl(sender);
        }

        private static void ctrl_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                GotoNextControl(sender);
        }

        private static void GotoNextControl(object ctrl)
        {
            ((UIElement)ctrl).MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
