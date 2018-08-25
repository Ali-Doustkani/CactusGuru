using DevExpress.Xpf.Editors;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Utils
{
    public static class PriceTextBox
    {
        public static void AddZeros(KeyEventArgs e, TextEdit sender)
        {
            if (e.Key != Key.Add) return;
            sender.Text += "000";
        }
    }
}
