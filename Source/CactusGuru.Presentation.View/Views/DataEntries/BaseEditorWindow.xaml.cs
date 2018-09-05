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
            view.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(view);
        }
    }
}
