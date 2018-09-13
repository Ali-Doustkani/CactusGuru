using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace CactusGuru.Presentation.View.Utils
{
    public static class Events
    {
        public static readonly DependencyProperty BindLoadEventsProperty = DependencyProperty.RegisterAttached("BindLoadEvents",
            typeof(bool),
            typeof(Events),
            new PropertyMetadata(BindLoadEventsChanged));

        public static bool GetBindLoadEvents(DependencyObject obj)
        {
            return (bool)obj.GetValue(BindLoadEventsProperty);
        }

        public static void SetBindLoadEvents(DependencyObject obj, bool value)
        {
            obj.SetValue(BindLoadEventsProperty, value);
        }

        private static void BindLoadEventsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(true))
            {
                var element = (FrameworkElement)obj;
                element.DataContextChanged += DataContextChanged;
                element.Unloaded += Unloaded;
            }
        }

        private static void DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            BindCommandToEvent(element, "LoadCommand", "Loaded");
            BindCommandToEvent(element, "UnloadCommand", "Unloaded");
        }

        private static void Unloaded(object sender, RoutedEventArgs e)
        {
            var element = (FrameworkElement)sender;
            element.DataContextChanged -= DataContextChanged;
            element.Unloaded -= Unloaded;
        }

        private static void BindCommandToEvent(FrameworkElement element, string commandName, string eventName)
        {
            var binding = new Binding(commandName);
            binding.Source = element.DataContext;
            var cmdAction = new InvokeCommandAction();
            BindingOperations.SetBinding(cmdAction, InvokeCommandAction.CommandProperty, binding);
            var trigger = new System.Windows.Interactivity.EventTrigger(eventName);
            trigger.Actions.Add(cmdAction);
            Interaction.GetTriggers(element).Add(trigger);
        }
    }
}
