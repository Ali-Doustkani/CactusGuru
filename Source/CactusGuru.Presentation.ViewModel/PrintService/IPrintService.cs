using System.Collections.Generic;

namespace CactusGuru.Presentation.ViewModel.PrintService
{
    public interface IPrintService
    {
        void PrintLabel(IEnumerable<LabelPrintItemDto> labels, PaperType paperType);
    }
}
