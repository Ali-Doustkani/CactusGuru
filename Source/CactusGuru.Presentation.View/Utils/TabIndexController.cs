using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.LookUp;

namespace CactusGuru.Presentation.View.Utils
{
    public class TabIndexController
    {
        public TabIndexController()
        {
            _controls = new List<Control>();
        }

        private readonly List<Control> _controls;

        public event EventHandler ReachedToLastField = delegate { }; 

        public void AddControl(Control ctrl)
        {
            if (ctrl is LookUpEdit)
                (ctrl as LookUpEdit).PopupClosed += TabIndexController_PopupClosed;
            ctrl.PreviewKeyDown += ctrl_PreviewKeyDown;
            _controls.Add(ctrl);
        }

        public void ClearControls()
        {
            _controls.Clear();
        }

        private void TabIndexController_PopupClosed(object sender, ClosePopupEventArgs e)
        {
            if (e.CloseMode == PopupCloseMode.Normal)
                GotoNextControl(sender);
        }

        private void ctrl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                GotoNextControl(sender);
        }

        private void GotoNextControl(object ctrl)
        {
            var currentIndex = _controls.IndexOf(ctrl as Control);
            if (_controls.IsValidIndex(++currentIndex))
            {
                _controls[currentIndex].Focus();
                if (!_controls[currentIndex].IsEnabled)
                    GotoNextControl(_controls[currentIndex]);
            }
            else
                ReachedToLastField(this, EventArgs.Empty);

        }
    }
}
