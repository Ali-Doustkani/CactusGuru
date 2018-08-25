using System.Windows;
using System.Windows.Input;
using CactusGuru.Presentation.View.Utils;
using CactusGuru.Presentation.ViewModel.Utils;
using System;

namespace CactusGuru.Presentation.View.Views
{
    public partial class CollectorEditor : IWindowController, IUserControlView
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

        private void CollectorEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            listBox.Focus();
        }

        public void FocusFirstControl()
        {
            txtTitle.Focus();
        }

        public void FocusOnSearch()
        {
            txtSearch.Focus();
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            FocusUtil.GotoNextItem(listBox, e);
        }
    }
}
