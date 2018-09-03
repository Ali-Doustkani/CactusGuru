using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Views
{
    public partial class BaseEditorWindow
    {
        public BaseEditorWindow()
        {
            InitializeComponent();
        }

        public void SetUserControl(Grid view)
        {
            ContentControl.Content = view;
        }
    }
}
