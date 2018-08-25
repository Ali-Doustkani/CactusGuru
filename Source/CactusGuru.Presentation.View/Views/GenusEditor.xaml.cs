using System;
using System.Windows;
using System.Windows.Input;
using CactusGuru.Presentation.View.Utils;
using CactusGuru.Presentation.ViewModel.Utils;

namespace CactusGuru.Presentation.View.Views
{
    public partial class GenusEditor : IWindowController, IUserControlView
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

        private void SupplierEditor_OnLoaded(object sender, RoutedEventArgs e)
        {
            listBox.Focus();
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            FocusUtil.GotoNextItem(listBox, e);
        }

        public void FocusFirstControl()
        {
            txtTitle.Focus();
        }

        public void FocusOnSearch()
        {
            txtSearch.Focus();
        }
    }
}
