using CactusGuru.Presentation.View.Utils;
using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Views
{
    public partial class GenusEditor : IUserControlView
    {
        public GenusEditor()
        {
            InitializeComponent();
            _controller = new TabIndexController();
            _controller.AddControl(txtTitle);
            _controller.ReachedToLastField += (a, b) => Save(this, EventArgs.Empty);
        }

        private readonly TabIndexController _controller;

        public event EventHandler Save = delegate { };

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            FocusUtil.GotoNextItem(listBox, e);
        }
    }
}
