using System.Windows;
using System.Windows.Input;
using CactusGuru.Presentation.View.Utils;
using CactusGuru.Presentation.ViewModel.Utils;
using System;

namespace CactusGuru.Presentation.View.Views
{
    public partial class SupplierEditor : IWindowController, IUserControlView
    {
        public SupplierEditor()
        {
            InitializeComponent();
            _tabController = new TabIndexController();
            _tabController.ReachedToLastField += (a, b) => Save(this, EventArgs.Empty);
            _tabController.AddControl(txtTitle);
            _tabController.AddControl(txtAcronym);
            _tabController.AddControl(txtWebSite);
        }

        private readonly TabIndexController _tabController;

        public event EventHandler Save = delegate { };

        private void SupplierEditor_OnLoaded(object sender, RoutedEventArgs e)
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
