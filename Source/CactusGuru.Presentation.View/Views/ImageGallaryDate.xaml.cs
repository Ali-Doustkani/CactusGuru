﻿using DevExpress.Xpf.Editors;
using System;
using System.Globalization;

namespace CactusGuru.Presentation.View.Views
{
    public partial class ImageGallaryDate
    {
        public ImageGallaryDate()
        {
            InitializeComponent();
        }

        public DateTime GetDate()
        {
            var cal = new PersianCalendar();
            var month = cal.GetMonth(DateTime.Now);
            var year = cal.GetYear(DateTime.Now);
            if (cmbMonth.SelectedIndex != -1)
                month = cmbMonth.SelectedIndex + 1;
            if (cmbYear.SelectedIndex != -1)
                year = Convert.ToInt32(((ComboBoxEditItem)cmbYear.SelectedItem).Content);
            return cal.ToDateTime(year, month, 1, 1, 1, 1, 1);
        }

        private void DXDialog_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            cmbMonth.SelectedIndex = 0;
            cmbMonth.Focus();
            cmbMonth.IsPopupOpen = true;
        }

        private void cmbMonth_PopupClosed(object sender, ClosePopupEventArgs e)
        {
            cmbYear.Focus();
            cmbYear.IsPopupOpen = true;
        }
    }
}