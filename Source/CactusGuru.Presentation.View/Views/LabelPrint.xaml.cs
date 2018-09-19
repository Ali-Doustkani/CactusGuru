using CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Views
{
    public partial class LabelPrint
    {
        public LabelPrint()
        {
            InitializeComponent();
        }

        private void ItemMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ((LabelPrintEditorViewModel)DataContext).AddToPrintCommand.Execute(null);
        }

        private void FocusOnSearchBox(object sender, System.EventArgs e)
        {
            txtSearch.Focus();
        }
    }
}