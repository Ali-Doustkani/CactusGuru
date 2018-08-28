using CactusGuru.Presentation.View.Utils;
using System;

namespace CactusGuru.Presentation.View.Views
{
    public partial class CollectionItemEditor : IUserControlView
    {
        public CollectionItemEditor()
        {
            InitializeComponent();
            var controller = new TabIndexController();
            controller.ReachedToLastField += (a, b) => Save(this, EventArgs.Empty);
            controller.AddControl(txtCode);
            controller.AddControl(cmbTaxa);
            controller.AddControl(cmbCollectors);
            controller.AddControl(txtFieldNumber);
            controller.AddControl(txtLocality);
            controller.AddControl(txtDate);
            controller.AddControl(cmbIncomeTypes);
            controller.AddControl(cmbSuppliers);
            controller.AddControl(txtSupplierCode);
            controller.AddControl(txtCount);
            controller.AddControl(txtDescription);
        }

        public void FocusFirstControl()
        {
            cmbTaxa.Focus();
        }

        public void FocusOnSearch()
        {

        }

        public event EventHandler Save = delegate { };
    }
}
