using CactusGuru.Presentation.ViewModel.PrintService;
using System.Collections.Generic;
using System.Windows.Controls;

namespace CactusGuru.Presentation.View.PrintService
{
    public class PrintService : IPrintService
    {
        public void PrintLabel(IEnumerable<LabelPrintItemDto> labels)
        {
            var dialog = new PrintDialog();
            if (dialog.ShowDialog() != true) return;

            var document = new LabelPrintDocument(labels);
            dialog.PrintDocument(document.GenerateDocument(), "Labels");
        }
    }
}