using CactusGuru.Presentation.View.Utils;
using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Views
{
    public partial class CollectorEditor : IUserControlView
    {
        public CollectorEditor()
        {
            InitializeComponent();
            _controller = new TabIndexController();
            _controller.ReachedToLastField += (a, b) => Save(this, EventArgs.Empty);
            _controller.AddControl(txtTitle);
            _controller.AddControl(txtAcronym);
            _controller.AddControl(txtWebsite);
        }

        private readonly TabIndexController _controller;

        public event EventHandler Save = delegate { };

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            FocusHandler.NavigateListboxItems(listBox, e);
        }
    }
}
