using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Utils
{
    public static class InputBindingsManager
    {
        public static readonly DependencyProperty UpdatePropertySourceWhenEnterPressedProperty = DependencyProperty.RegisterAttached(
                "UpdatePropertySourceWhenEnterPressed", typeof(DependencyProperty), typeof(InputBindingsManager), new PropertyMetadata(null, OnUpdatePropertySourceWhenEnterPressedPropertyChanged));

        static InputBindingsManager() { }

        public static void SetUpdatePropertySourceWhenEnterPressed(DependencyObject dp, DependencyProperty value)
        {
            dp.SetValue(UpdatePropertySourceWhenEnterPressedProperty, value);
        }

        public static DependencyProperty GetUpdatePropertySourceWhenEnterPressed(DependencyObject dp)
        {
            return (DependencyProperty)dp.GetValue(UpdatePropertySourceWhenEnterPressedProperty);
        }

        private static void OnUpdatePropertySourceWhenEnterPressedPropertyChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            var element = dp as UIElement;

            if (element == null)
                return;

            if (e.OldValue != null)
                element.PreviewKeyDown -= Element_PreviewKeyDown;

            if (e.NewValue != null)
                element.PreviewKeyDown += Element_PreviewKeyDown;
        }

        private static void Element_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                DoUpdateSource(e.Source);
        }

        static void DoUpdateSource(object source)
        {
            DependencyProperty property =
                GetUpdatePropertySourceWhenEnterPressed(source as DependencyObject);

            if (property == null)
                return;

            var elt = source as UIElement;

            if (elt == null)
                return;

            var binding = BindingOperations.GetBindingExpression(elt, property);

            if (binding != null)
                binding.UpdateSource();
        }
    }
}
