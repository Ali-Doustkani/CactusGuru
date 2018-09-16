using System.Collections.Generic;

namespace CactusGuru.Presentation.ViewModel.Services.Printings
{
    public interface IPrintService
    {
        void PrintLabel(IEnumerable<LabelPrintItemDto> labels);
    }
}
