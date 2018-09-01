using System.Windows.Input;
using CactusGuru.Presentation.ViewModel.ViewModels.LabelPrint;
using DevExpress.Xpf.Grid;

namespace CactusGuru.Presentation.View.Views
{
    public partial class LabelPrint
    {
        public LabelPrint()
        {
            InitializeComponent();
            txtSourceSearch.Focus();
        }

        private const int COLLECTION_ITEMS = 0;
        private const int TAXA = 1;

        private void txtSourceSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (tab.SelectedIndex == COLLECTION_ITEMS)
                ApplyKey(listCollectionItems, e.Key);
            else if (tab.SelectedIndex == TAXA)
                ApplyKey(listTaxa, e.Key);
        }

        private void ApplyKey(GridControl grid, Key key)
        {
            if (key == Key.Up)
                grid.View.MovePrevRow();
            else if (key == Key.Down)
                grid.View.MoveNextRow();
            else if (key == Key.Enter)
                btnAddToPrint.Command.Execute(null);
        }

        private void listCollectionItems_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtSourceSearch.Focus();
        }

        private void listCollectionItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            btnAddToPrint.Command.Execute(null);
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

        private void tab_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            txtSourceSearch.Focus();
        }
    }
}
