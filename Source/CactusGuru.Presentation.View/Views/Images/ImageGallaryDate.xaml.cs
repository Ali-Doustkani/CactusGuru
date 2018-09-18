using System;
using System.Windows.Controls;

namespace CactusGuru.Presentation.View.Views
{
    public partial class ImageGallaryDate
    {
        public ImageGallaryDate()
        {
            InitializeComponent();
            FillYears();
        }

        private void FillYears()
        {
            for (int i = DateTime.Now.Year; i > DateTime.Now.Year - 30; i--)
            {
                var item = new ComboBoxItem
                {
                    Content = i
                };
                cmbYear.Items.Add(item);
            }
        }

        public DateTime GetDate()
        {
            var month = cmbMonth.SelectedIndex != -1 ? cmbMonth.SelectedIndex + 1 : DateTime.Now.Month;
            var year = cmbYear.SelectedIndex != -1 ? (int)((ComboBoxItem)cmbYear.SelectedItem).Content : DateTime.Now.Year;
            return new DateTime(year, month, 1);
        }

        public void SetDate(DateTime date)
        {
            cmbMonth.SelectedIndex = date.Month - 1;
            foreach (ComboBoxItem item in cmbYear.Items)
            {
                if (item.Content.Equals(date.Year))
                {
                    cmbYear.SelectedItem = item;
                    return;
                }
            }
            cmbYear.SelectedIndex = 0;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
