using System.Windows;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Utils
{
    public static class EnterKeyAttach
    {
        public static readonly DependencyProperty EnterKeyProperty = DependencyProperty.RegisterAttached("EnterKey",
            typeof(InputBinding),
            typeof(EnterKeyAttach),
            new PropertyMetadata(EnterKeyChanged));

        public static InputBinding GetEnterKey(DependencyObject obj)
        {
            return (InputBinding)obj.GetValue(EnterKeyProperty);
        }

        public static void SetEnterKey(DependencyObject obj, InputBindingCollection value)
        {
            obj.SetValue(EnterKeyProperty, value);
        }

        private static void EnterKeyChanged(DependencyObject obj , DependencyPropertyChangedEventArgs e)
        {
            var element = (UIElement)obj;
            element.InputBindings.Clear();
            element.InputBindings.Add((InputBinding)e.NewValue);
        }
    }
}
