using CactusGuru.Presentation.View.Utils;
using System;
using System.Windows.Input;

namespace CactusGuru.Presentation.View.Views
{
    public partial class SupplierEditor : IUserControlView
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

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            FocusHandler.NavigateListboxItems(listBox, e);
        }
    }
}
