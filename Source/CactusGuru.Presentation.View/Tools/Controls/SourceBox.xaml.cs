namespace CactusGuru.Presentation.View.Tools.Controls
{
    public partial class SourceBox
    {
        public SourceBox()
        {
            Resources.MergedDictionaries.Add(ResourceLocator.Lists);
            InitializeComponent();
        }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listbox.ScrollIntoView(listbox.SelectedItem);
        }

        private void Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            txtSearch.Focus();
        }
    }
}
