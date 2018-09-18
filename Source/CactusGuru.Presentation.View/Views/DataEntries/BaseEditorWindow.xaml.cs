using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Views
{
    public partial class BaseEditorWindow
    {
        public BaseEditorWindow()
        {
            Resources.MergedDictionaries.Add(ResourceLocator.General);
            InitializeComponent();
        }

        public void SetUserControl(Grid view)
        {
            SetValue(TitleProperty, view.Tag);
            view.SetValue(Grid.RowProperty, 1);
            grid.Children.Add(view);
        }
    }
}
