using CactusGuru.Presentation.ViewModel.PrintService;
using Stimulsoft.Report;
using System.Collections.Generic;
using System.Drawing.Printing;

namespace CactusGuru.Entry.Presentation
{
    public class PrintService : IPrintService
    {
        private PaperType _paperType;

        public void PrintLabel(IEnumerable<LabelPrintItemDto> labels, PaperType paperType)
        {
            StiOptions.Engine.GlobalEvents.PrintingDocumentInViewer += GlobalEvents_PrintingDocumentInViewer;
            _paperType = paperType;
            var report = new StiReport();
            report.RegBusinessObject(nameof(LabelPrintItemDto), labels);
            if (paperType == PaperType.A4)
                report.Load("Reports\\A4Label.mrt");
            else if (paperType == PaperType.TenCm)
                report.Load("Reports\\10CmLabel.mrt");
            report.Show();
            StiOptions.Engine.GlobalEvents.PrintingDocumentInViewer -= GlobalEvents_PrintingDocumentInViewer;
        }

        private void GlobalEvents_PrintingDocumentInViewer(object sender, System.EventArgs e)
        {
            if (_paperType != PaperType.TenCm) return;
            var settings = new PrinterSettings();
            settings.DefaultPageSettings.PaperSize = new PaperSize("10Cm", 413, 2900);
            ((StiReport)sender).Print(settings);
        }
    }
}
