namespace CactusGuru.Presentation.View.UserControls
{
    public partial class SourceBox  
    {
        public SourceBox()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            listbox.ScrollIntoView(listbox.SelectedItem);
        }
    }
}
