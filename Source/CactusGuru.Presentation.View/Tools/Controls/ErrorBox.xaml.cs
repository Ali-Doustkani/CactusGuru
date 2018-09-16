using System.Windows;
using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Tools.Controls
{
    public partial class ErrorBox
    {
        public ErrorBox()
        {
            InitializeComponent();
        }

        private void UserControl_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
            {
                Visibility = Visibility.Visible;
                message.Text = (string)e.Error.ErrorContent;
            }
            else
            {
                Visibility = Visibility.Collapsed;
                message.Text = string.Empty;
            }
        }
    }
}
