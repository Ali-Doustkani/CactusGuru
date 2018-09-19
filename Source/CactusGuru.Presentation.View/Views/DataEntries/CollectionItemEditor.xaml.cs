using CactusGuru.Presentation.ViewModel.ViewModels.CollectionItemViewModels;
using System;
using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Views.DataEntries
{
    public partial class CollectionItemEditor
    {
        public CollectionItemEditor()
        {
            InitializeComponent();
        }

        public void Set(Guid? id)
        {
            if (id == null)
            {
                AutoOpen(cmbCollector);
                AutoOpen(cmbIncomeType);
                AutoOpen(cmbSupplier);
                AutoOpen(cmbTaxon);
            }
            else
            {
                 ((CollectionItemEditorViewModel)DataContext).PrepareForEdit(id.Value);
            }
        }

        private void AutoOpen(ComboBox comboBox)
        {
            comboBox.SetValue(Tools.AttachedProps.FocusHandler.AutoOpenDropDownProperty, true);
        }
    }
}
