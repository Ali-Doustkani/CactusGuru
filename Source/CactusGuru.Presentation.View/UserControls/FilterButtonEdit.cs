using DevExpress.Xpf.Editors;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.UserControls
{
    public class FilterButtonEdit : ButtonEdit
    {
        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            if (e.Key != Key.Up && e.Key != Key.Down) return;

            e.Handled = true;
            foreach (InputBinding binding in InputBindings)
            {
                var keyBinding = binding as KeyBinding;
                if (keyBinding == null) continue;
                if (keyBinding.Key == e.Key)
                    keyBinding.Command.Execute(null);
            }
            base.OnPreviewKeyDown(e);
        }
    }
}
