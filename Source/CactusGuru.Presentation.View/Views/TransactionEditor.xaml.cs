using CactusGuru.Presentation.ViewModel.ViewModels.TransactionViewModels;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.LookUp;
using System;
using System.Windows;
using DevExpress.Xpf.Editors.Helpers;
using System.Collections.Generic;

namespace CactusGuru.Presentation.View.Views
{
    public partial class TransactionEditor : IUserControlView
    {
        public TransactionEditor()
        {
            InitializeComponent();
            DataContext = new TransactionEditorViewModel();
        }

        public event EventHandler Save;

        private TransactionEditorViewModel ViewModel()
        {
            return (TransactionEditorViewModel)DataContext;
        }

        private void TableView_InitNewRow(object sender, InitNewRowEventArgs e)
        {
            grid.SetCellValue(e.RowHandle, "RowNumber", ViewModel().InitRowNumber());
            grid.SetCellValue(e.RowHandle, "PartType", ViewModel().InitPartType());
        }

        private void TableView_ValidateRow(object sender, GridRowValidationEventArgs e)
        {
            if (e.Row == null) return;
            if (e.RowHandle != DataControlBase.NewItemRowHandle) return;
            e.IsValid = true;
            e.Handled = true;
        }

        private void TableView_InvalidRowException(object sender, InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction; 
        }

    }
}
