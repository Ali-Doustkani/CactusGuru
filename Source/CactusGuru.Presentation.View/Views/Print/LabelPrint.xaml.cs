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

        private void MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewModel().AddToPrintCommand.Execute(null);
        }

        private void GridControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && ViewModel() != null)
                ViewModel().DeleteCurrentPrintItemCommand.Execute(null);
        }

        private LabelPrintEditorViewModel ViewModel()
        {
            return DataContext as LabelPrintEditorViewModel;
        }

        private void FocusOnSearchBox(object sender, System.EventArgs e)
        {
            txtSearch.Focus();
        }
    }
}