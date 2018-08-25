using DevExpress.Xpf.Core;

namespace CactusGuru.Presentation.View.Views
{
    public class MyDXMessageBoxLocalizer : DXMessageBoxLocalizer
    {
        public override string GetLocalizedString(DXMessageBoxStringId id)
        {
            if (id == DXMessageBoxStringId.Ok)
                return "تایید";

            if (id == DXMessageBoxStringId.Cancel)
                return "لغو";

            return base.GetLocalizedString(id);
        }
    }
}
