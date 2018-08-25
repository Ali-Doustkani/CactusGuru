using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Utils
{
    internal static class FocusUtil
    {
        public static void GotoNextControl(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var keyboardFocus = sender as UIElement;
                if (keyboardFocus != null)
                {
                    var tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                    keyboardFocus.MoveFocus(tRequest);
                }
            }
        }

        public static void GotoNextItem(ListBox listBox, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
                listBox.SelectedIndex++;
            else if (e.Key == Key.Up)
                listBox.SelectedIndex--;
        }
    }
}
